using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_ImgConvert : CaseModel
    {
        public Test_ImgConvert() {
            this.NameSign = @"图片转化";
            this.SonCases = new CaseModel[] {
                new MD5Chane(),
            };
        }

        /// <summary>
        /// 清空指定的文件夹，但不删除文件夹
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFolder(string dir) {
            foreach (string d in Directory.GetFileSystemEntries(dir)) {
                if (System.IO.File.Exists(d)) {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    System.IO.File.Delete(d);
                } else {
                    DirectoryInfo d1 = new DirectoryInfo(d);
                    if (d1.GetFiles().Length != 0) {
                        DeleteFolder(d1.FullName);
                    }
                    Directory.Delete(d);
                }
            }
        }

        public class MD5Chane : CaseModel
        {
            public MD5Chane() {
                this.NameSign = @"MD5更改";
                this.ExeEvent = Method;
            }

            public bool Method() {
                DirectoryInfo olddir = new DirectoryInfo(@"D:\auto\circleoffriends\2018-12-18");
                DirectoryInfo newdir = new DirectoryInfo(@"D:\auto\circleoffriends\download");
                DeleteFolder(newdir.FullName);
                FileInfo[] fis = PathHelp.PatternFileInfo(olddir, @".*\.(jpg|png|gif)");
                foreach (FileInfo file in fis) {
                    Calc(newdir, file);
                }
                return false;
            }
            private void SaveImg(byte[] imgcontent, string filepath) {
                using (MemoryStream ms = new MemoryStream(imgcontent)) {
                    using (Bitmap bitmap = new Bitmap(ms)) {
                        bitmap.Save(filepath, GetImageFormat(bitmap));
                    }
                }
            }

            public void Calc(DirectoryInfo newdir, FileInfo file) {
                string newfilepath = newdir.FullName + @"\" + file.Name;
                FileInfo newfile = new FileInfo(newfilepath);
                int rate = 100;
                byte[] imgcontent = CanUseImg(file, out rate);
                SaveImg(imgcontent, newfilepath);
                string[] strs = new string[] {
                        string.Format("oldFile.size: {0}MB", KB_MB(file.Length)),
                        string.Format("newFile.size: {0}MB", KB_MB(newfile.Length)),
                        string.Format("imgcontent.Length: {0}MB", KB_MB(imgcontent.Length)),
                        string.Format("file.Name: {0}", file.Name),
                        string.Format("rate: {0}", rate),
                    };
                Console.WriteLine(ConvertTool.ToString(strs, @" "));
            }

            public double KB_MB(double len) {
                return Math.Round(len / 1024.0 / 1024.0, 2);
            }
            public byte[] CanUseImg(FileInfo file, out int rate) {
                byte[] imgcontent = new byte[] { };
                for (rate = 100; rate > 1; rate -= 5) {
                    imgcontent = CreateNewImg(file, rate);
                    if (imgcontent.Length <= file.Length || !IsBigImg(imgcontent.Length)) {
                        break;
                    }
                }
                return imgcontent;
            }
            public bool IsBigImg(int imglength) {
                return KB_MB(imglength) > 2;
            }
            public byte[] CreateNewImg(FileInfo oldfile, int rate) {
                using (Bitmap bitmap = new Bitmap(oldfile.FullName)) {
                    int x = RandomData.GetInt(0, bitmap.Width);
                    int y = RandomData.GetInt(0, bitmap.Height);
                    Color oc = bitmap.GetPixel(x, y);
                    Color nc = ChangeColor(oc);
                    bitmap.SetPixel(x, y, nc);

                    #region 重写计算尺寸大小
                    int max_width = 1200;
                    int max_height = 700;
                    bool isBigImg = Math.Round(oldfile.Length / 1024.0 / 1024.0, 2) > 2;
                    if (isBigImg && (bitmap.Width >= max_width || bitmap.Height >= max_height)) {
                        Size _newSize = ResizeImage(bitmap.Width, bitmap.Height, max_width, max_height);
                        using (Bitmap newbitmap = new Bitmap(bitmap, _newSize)) {
                            return ConvertByte(newbitmap, rate);
                        }
                    }
                    #endregion
                    return ConvertByte(bitmap, rate);
                }
            }
            public byte[] ConvertByte(Bitmap bitmap, int rate) {
                // C#图片无损压缩
                // https://www.cnblogs.com/felix-wang/p/6378394.html
                EncoderParameters ep = new EncoderParameters();
                long[] qy = new long[1];
                qy[0] = rate > 100 ? 100 : rate < 1 ? 1 : rate;
                //设置压缩的比例1-100
                EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                ep.Param[0] = eParam;
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int i = 0; i < arrayICI.Length; i++) {
                    if (arrayICI[i].FormatDescription.Equals("JPEG")) {
                        jpegICIinfo = arrayICI[i];
                        break;
                    }
                }
                using (MemoryStream ms = new MemoryStream()) {
                    if (jpegICIinfo != null) {
                        bitmap.Save(ms, jpegICIinfo, ep);
                    } else {
                        ImageFormat imgformat = GetImageFormat(bitmap);
                        bitmap.Save(ms, imgformat);
                    }
                    return ms.ToArray();
                }
            }


            /// <summary>
            /// 计算新尺寸
            /// </summary>
            /// <param name="width">原始宽度</param>
            /// <param name="height">原始高度</param>
            /// <param name="maxWidth">最大新宽度</param>
            /// <param name="maxHeight">最大新高度</param>
            /// <returns></returns>
            private static Size ResizeImage(int width, int height, int maxWidth, int maxHeight) {
                //此次2012-02-05修改过=================
                if (maxWidth <= 0)
                    maxWidth = width;
                if (maxHeight <= 0)
                    maxHeight = height;
                //以上2012-02-05修改过=================
                decimal MAX_WIDTH = (decimal)maxWidth;
                decimal MAX_HEIGHT = (decimal)maxHeight;
                decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

                int newWidth, newHeight;
                decimal originalWidth = (decimal)width;
                decimal originalHeight = (decimal)height;

                if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT) {
                    decimal factor;
                    // determine the largest factor 
                    if (originalWidth / originalHeight > ASPECT_RATIO) {
                        factor = originalWidth / MAX_WIDTH;
                        newWidth = Convert.ToInt32(originalWidth / factor);
                        newHeight = Convert.ToInt32(originalHeight / factor);
                    } else {
                        factor = originalHeight / MAX_HEIGHT;
                        newWidth = Convert.ToInt32(originalWidth / factor);
                        newHeight = Convert.ToInt32(originalHeight / factor);
                    }
                } else {
                    newWidth = width;
                    newHeight = height;
                }
                return new Size(newWidth, newHeight);
            }

            public ImageFormat GetImageFormat(Image img) {
                ImageFormat[] list = new ImageFormat[] {
                    ImageFormat.Bmp,
                    ImageFormat.Emf,
                    ImageFormat.Exif,
                    ImageFormat.Gif,
                    ImageFormat.Icon,
                    ImageFormat.Jpeg,
                    ImageFormat.MemoryBmp,
                    ImageFormat.Png,
                    ImageFormat.Tiff,
                    ImageFormat.Wmf,
                };
                foreach (ImageFormat item in list) {
                    if (img.RawFormat.Equals(item)) {
                        return item;
                    }
                }
                return ImageFormat.Png;
            }

            private Color ChangeColor(Color oc) {
                int red = RandomData.GetInt(0, 255 + 1);
                int green = RandomData.GetInt(0, 255 + 1);
                int blue = RandomData.GetInt(0, 255 + 1);
                Color nc = Color.FromArgb(red, green, blue);
                return nc;
            }
        }
    }
}

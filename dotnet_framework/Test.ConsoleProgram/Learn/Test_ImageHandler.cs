using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YTS.BLL;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_ImageHandler : CaseModel
    {
        public Test_ImageHandler() {
            this.NameSign = @"图像处理程序";
            this.SonCases = new CaseModel[] {
                //new MD5Chane(), // 本地开发测试, 需要固定路径以及图片文件
                Func_MB_KB_B(),
                Func_Compression(),
                base.BreakCase(),
            };
        }

        public CaseModel Func_MB_KB_B() {
            return new CaseModel() {
                NameSign = @"单位换算",
                ExeEvent = () => {
                    long b = 1751335;
                    Console.WriteLine("b : {0}", b);
                    double kb = ImageHandler.B_KB(b);
                    Console.WriteLine("kb : {0}KB", kb);
                    double mb = ImageHandler.B_MB(b);
                    Console.WriteLine("mb : {0}MB", mb);
                    return true;
                },
            };
        }

        public CaseModel Func_Compression() {
            return new CaseModel() {
                NameSign = @"压缩计算",
                ExeEvent = () => {
                    const string oldpath = @"D:\ZRQDownloads\imgs";
                    if (!Directory.Exists(oldpath)) {
                        Console.WriteLine("停止执行, 路径不存在: {0}", oldpath);
                        return true;
                    }

                    ImageHandler handler = new ImageHandler();
                    const int maxwidth = 1200;
                    const int maxheight = 1200;
                    handler.SetMaxSize(maxwidth, maxheight);
                    const long maxbytelength = 2 * 1024 * 1024;
                    handler.SetMaxByteLength(maxbytelength);

                    DirectoryInfo[] sondirs = PathHelp.AllSonDirectorys(new DirectoryInfo(oldpath));
                    foreach (DirectoryInfo dirinfo in sondirs) {
                        FileInfo[] fis = PathHelp.PatternFileInfo(dirinfo, @".*\.(jpg|png|gif)");
                        foreach (FileInfo file in fis) {
                            byte[] imgvalues = handler.Calc(file);
                            if (CheckData.IsSizeEmpty(imgvalues)) {
                                Console.WriteLine("压缩图片失败, 结果为空!");
                                Console.WriteLine("FileInfo.FullName: {0}", file.FullName);
                                return false;
                            }
                            string newdirpath = file.DirectoryName.Replace("imgs", "imgs_c");
                            string newfilepath = PathHelp.CreateUseFilePath(newdirpath, file.Name);
                            handler.SaveImg(imgvalues, newfilepath);
                        }
                    }
                    return true;
                },
            };
        }

        public class MD5Chane : CaseModel
        {
            public MD5Chane() {
                this.NameSign = @"MD5更改";
                this.ExeEvent = Method;
            }

            public bool Method() {
                const string oldpath = @"D:\ZRQDownloads\imgs";
                DirectoryInfo[] sondirs = PathHelp.AllSonDirectorys(new DirectoryInfo(oldpath));
                foreach (DirectoryInfo dirinfo in sondirs) {
                    FileInfo[] fis = PathHelp.PatternFileInfo(dirinfo, @".*\.(jpg|png|gif)");
                    foreach (FileInfo file in fis) {
                        Calc(dirinfo, file);
                    }
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

            public void Calc(DirectoryInfo olddir, FileInfo file) {
                string oldpath = PathHelp.CreateUseFilePath(olddir.FullName, file.Name);
                int rate = 60;
                byte[] imgcontent = CanUseImg(file, out rate);
                string newpath = PathHelp.CreateUseFilePath(olddir.FullName.Replace("imgs", "imgs_c"), file.Name);
                if (File.Exists(newpath)) {
                    File.Delete(newpath);
                }
                SaveImg(imgcontent, newpath);
                PrintInfo(new FileInfo(oldpath), new FileInfo(newpath), rate);
            }

            public void PrintInfo(FileInfo oldfile, FileInfo newfile, int rate) {
                Console.WriteLine("oldfile.FullName: {0} Size: {1} rate: {2}", oldfile, Size(oldfile), rate);
                Console.WriteLine("newfile.FullName: {0} Size: {1}\n", newfile, Size(newfile));
            }

            public string Size(FileInfo file) {
                const double rate = 1024.0;
                double b = file.Length;
                if (b <= rate) {
                    return string.Format("{0}Byte", Math.Round(b, 2));
                }
                double kb = b / rate;
                if (b <= rate) {
                    return string.Format("{0}KB", Math.Round(kb, 2));
                }
                double mb = kb / rate;
                return string.Format("{0}MB", Math.Round(mb, 2));
            }

            public double B_KB(double len) {
                return Math.Round(len / 1024.0, 2);
            }
            public double B_MB(double len) {
                return Math.Round(len / 1024.0 / 1024.0, 2);
            }
            public byte[] CanUseImg(FileInfo file, out int rate) {
                byte[] imgcontent = new byte[] { };
                for (rate = 100; rate > 1; rate -= 5) {
                    imgcontent = CreateNewImg(file, rate);
                    if (!IsBigImg(imgcontent.Length)) {
                        break;
                    }
                }
                return imgcontent;
            }
            public bool IsBigImg(double len) {
                return B_KB(len) > 250.0;
                //return B_MB(len) > 2;
            }
            public byte[] CreateNewImg(FileInfo oldfile, int rate) {
                using (Bitmap bitmap = new Bitmap(oldfile.FullName)) {
                    //int x = RandomData.GetInt(0, bitmap.Width);
                    //int y = RandomData.GetInt(0, bitmap.Height);
                    //Color oc = bitmap.GetPixel(x, y);
                    //Color nc = ChangeColor(oc);
                    //bitmap.SetPixel(x, y, nc);

                    #region 重写计算尺寸大小
                    int max_width = 1200;
                    int max_height = 7000;
                    if (bitmap.Width >= max_width || bitmap.Height >= max_height) {
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

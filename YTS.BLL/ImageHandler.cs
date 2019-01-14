using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using YTS.Tools;

namespace YTS.BLL
{
    public class ImageHandler
    {
        private int max_width = 0;
        private int max_height = 0;
        private long max_byte_length = 0;

        public ImageHandler() { }

        public void SetMaxSize(int maxwidth, int maxheight) {
            max_width = Math.Abs(maxwidth);
            max_height = Math.Abs(maxheight);
        }

        public void SetMaxByteLength(long maxlength) {
            max_byte_length = Math.Abs(maxlength);
        }

        public byte[] Calc(FileInfo file) {
            if (CheckData.IsObjectNull(file) || !file.Exists) {
                return new byte[] { };
            }

            using (Bitmap bitmap = new Bitmap(file.FullName)) {
                if ((this.max_width != 0 && bitmap.Width > this.max_width) ||
                (this.max_height != 0 && bitmap.Height > this.max_height)) {
                    Size new_size = ResizeImage(bitmap.Width, bitmap.Height, max_width, max_height);
                    using (Bitmap smallbitmap = new Bitmap(bitmap, new_size)) {
                        int x = RandomData.GetInt(0, bitmap.Width);
                        int y = RandomData.GetInt(0, bitmap.Height);
                        Color nc = RandomData.ColorRGB();
                        bitmap.SetPixel(x, y, nc);
                    }
                } else {
                    int x = RandomData.GetInt(0, bitmap.Width);
                    int y = RandomData.GetInt(0, bitmap.Height);
                    Color nc = RandomData.ColorRGB();
                    bitmap.SetPixel(x, y, nc);
                }

                byte[] bytes = new byte[] { };
                for (int rate = 100; rate > 1; rate -= 5) {
                    bytes = CreateNewImg(file, rate);
                    if (!IsBigImg(bytes.Length)) {
                        break;
                    }
                }
                return bytes;
            }
            
        }

        public void SaveImg(byte[] imgcontent, string filepath) {
            using (MemoryStream ms = new MemoryStream(imgcontent)) {
                using (Bitmap bitmap = new Bitmap(ms)) {
                    bitmap.Save(filepath, GetImageFormat(bitmap));
                }
            }
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
        public static double B_KB(double len) {
            return Math.Round(len / 1024.0, 2);
        }
        public static double B_MB(double len) {
            return Math.Round(len / 1024.0 / 1024.0, 2);
        }
        public static bool IsBigImg(double len) {
            return B_KB(len) > 250.0;
        }
        public byte[] CreateNewImg(FileInfo oldfile, int rate) {
            using (Bitmap bitmap = new Bitmap(oldfile.FullName)) {
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
        public static Size ResizeImage(int width, int height, int maxWidth, int maxHeight) {
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
    }
}

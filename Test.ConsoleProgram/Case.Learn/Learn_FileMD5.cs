using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Test.ConsoleProgram.Case.Learn
{
    public class Learn_FileMD5 : CaseModel
    {
        public Learn_FileMD5() {
            this.NameSign = @"获取文件的MD5值";
            this.ExeEvent = () => {
                //string directory = @"D:\ZRQWork\Work-History-File\test_file_md5";
                //string sorimgPath = string.Format(@"{0}\{1}", directory, @"text.png");
                //string newimgPath = string.Format(@"{0}\{1}", directory, @"new_text.png");
                //Print.WriteLine("sorimgPath: {0}\nmd5: {1}\n\n", sorimgPath, GetMD5HashFromFile(sorimgPath));

                //ChangeImageContent(sorimgPath, newimgPath);
                //Print.WriteLine("sorimgPath: {0}\nmd5: {1}\n\n", sorimgPath, GetMD5HashFromFile(sorimgPath));
                //Print.WriteLine("newimgPath: {0}\nmd5: {1}\n\n", newimgPath, GetMD5HashFromFile(newimgPath));

                //ImageFormat[] imgformat = new ImageFormat[] {
                //    ImageFormat.Bmp,
                //    ImageFormat.Emf,
                //    ImageFormat.Exif,
                //    ImageFormat.Gif,
                //    ImageFormat.Icon,
                //    ImageFormat.Jpeg,
                //    ImageFormat.MemoryBmp,
                //    ImageFormat.Png,
                //    ImageFormat.Tiff,
                //    ImageFormat.Wmf,
                //};
                ////Print.WriteLine(JsonHelper.SerializeObject(imgformat));
                ////Print.WriteLine(JsonHelper.SerializeObject(imgformat));
                //foreach(ImageFormat item in imgformat) {
                //    Print.WriteLine(item.ToString());
                //}



                string directory = @"D:\ZRQWork\JianGuoYunFolder\WorkFolder\YiMei\YiMei.Web\upload\201805\11";
                string sorimgPath = string.Format(@"{0}\{1}", directory, @"201805111417012978.png");
                string newimgPath = string.Format(@"{0}\{1}", directory, @"jiwjefa.png");
                Print.WriteLine("sorimgPath: {0}\nmd5: {1}\n\n", sorimgPath, GetMD5HashFromFile(sorimgPath));
                Print.WriteLine("newimgPath: {0}\nmd5: {1}\n\n", newimgPath, GetMD5HashFromFile(newimgPath));

                return true;
            };
        }
        private string GetMD5HashFromFile(string fileName) {
            try {
                byte[] retVal = new byte[] { };
                using(FileStream file = new FileStream(fileName, FileMode.Open)) {
                    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    retVal = md5.ComputeHash(file);
                    file.Dispose();
                    file.Close();
                }
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < retVal.Length; i++) {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            } catch(Exception ex) {
                throw new Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
            }
        }

        private void ChangeImageContent(string sourceimgPath, string newimgPath) {
            if(sourceimgPath == newimgPath) {
                return;
            }
            using(Bitmap bitmap = new Bitmap(sourceimgPath)) {
                int x = 0, y = 0;
                Color color = GetFirstNotAlphaColor(bitmap, ref x, ref y);
                Color newcolor = Color.FromArgb(
                    color.A,
                    color.R,
                    color.G,
                    color.B >= 255 ?
                        color.B - 1 :
                        color.B + 1);
                bitmap.SetPixel(x, y, color);
                using(Bitmap sonmap = new Bitmap(bitmap, bitmap.Width, bitmap.Height)) {
                    sonmap.Save(newimgPath, bitmap.RawFormat);
                    sonmap.Dispose();
                }
                bitmap.Dispose();
            }
        }

        private Color GetFirstNotAlphaColor(Bitmap bitmap, ref int x, ref int y) {
            for(int wx = 0; wx < bitmap.Width; wx++) {
                for(int hy = 0; hy < bitmap.Height; hy++) {
                    Color color = bitmap.GetPixel(wx, hy);
                    if(color.A > 0) {
                        x = wx;
                        y = hy;
                        return color;
                    }
                }
            }
            return bitmap.GetPixel(RandomData.GetInt(0, bitmap.Width), RandomData.GetInt(0, bitmap.Height));
        }
    }
}

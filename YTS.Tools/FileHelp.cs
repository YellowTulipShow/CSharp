using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace YTS.Tools
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FileHelp
    {
        /// <summary>
        /// 只读文件内容
        /// </summary>
        /// <param name="file_path">文件路径</param>
        /// <returns>文件内容</returns>
        public static string OnlyRead(string file_path) {
            return OnlyRead(file_path, Const.Format.FILE_ENCODING);
        }

        /// <summary>
        /// 只读文件内容
        /// </summary>
        /// <param name="file_path">文件路径</param>
        /// <param name="encoding">文件编码</param>
        /// <returns></returns>
        public static string OnlyRead(string file_path, Encoding encoding) {
            string abs_file_path = PathHelp.ToAbsolute(file_path);
            if (CheckData.IsStringNull(abs_file_path)) {
                return string.Empty;
            }
            PathHelp.CreateFileExists(abs_file_path);
            using (FileStream rfs = File.OpenRead(abs_file_path)) {
                using (StreamReader sr = new StreamReader(rfs, encoding)) {
                    string content = sr.ReadToEnd();
                    return content;
                }
            }
        }

        /// <summary>
        /// 只写文件内容
        /// </summary>
        /// <param name="file_path">文件路径</param>
        /// <param name="content">写入内容</param>
        public static void OnlyWrite(string file_path, string content) {
            OnlyWrite(file_path, content, Const.Format.FILE_ENCODING);
        }

        /// <summary>
        /// 只写文件内容
        /// </summary>
        /// <param name="file_path">文件路径</param>
        /// <param name="content">写入内容</param>
        /// <param name="encoding">文件编码</param>
        public static void OnlyWrite(string file_path, string content, Encoding encoding) {
            string abs_file_path = PathHelp.ToAbsolute(file_path);
            if (CheckData.IsStringNull(abs_file_path)) {
                return;
            }
            content = ConvertTool.ToString(content);
            File.Delete(abs_file_path);
            PathHelp.CreateFileExists(abs_file_path);
            using (FileStream wfs = File.OpenWrite(abs_file_path)) {
                using (StreamWriter sw = new StreamWriter(wfs, encoding)) {
                    sw.Write(content);
                    sw.Flush();
                }
            }
        }


        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="filename">文件物理路径含文件名</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBinaryFile(string fullName) {
            if (File.Exists(fullName)) {
                FileStream Fsm = null;
                try {
                    Fsm = File.OpenRead(fullName);
                    return ConvertStreamToByteBuffer(Fsm);
                } catch {
                    return new byte[0];
                } finally {
                    Fsm.Close();
                }
            } else {
                return new byte[0];
            }
        }

        /// <summary>
        /// 流转化为字节数组
        /// </summary>
        /// <param name="theStream">流</param>
        /// <returns>字节数组</returns>
        public static byte[] ConvertStreamToByteBuffer(System.IO.Stream theStream) {
            int bi;
            MemoryStream tempStream = new System.IO.MemoryStream();
            try {
                while ((bi = theStream.ReadByte()) != -1) {
                    tempStream.WriteByte(((byte)bi));
                }
                return tempStream.ToArray();
            } catch {
                return new byte[0];
            } finally {
                tempStream.Close();
            }
        }

        /// <summary>
        /// 文件流上传文件
        /// </summary>
        /// <param name="binData">字节数组</param>
        /// <param name="fileName">文件物理路径含文件名</param>
        public static bool SaveFile(byte[] binData, string fullName) {
            FileStream fileStream = null;
            MemoryStream m = new MemoryStream(binData);
            try {
                fileStream = new FileStream(fullName, FileMode.Create);
                m.WriteTo(fileStream);
                return true;
            } catch {
                return false;
            } finally {
                m.Close();
                fileStream.Close();
            }
        }

        /// <summary>
        /// 删除本地单个文件
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        public static bool DeleteFile(string _filepath) {
            if (string.IsNullOrEmpty(_filepath)) {
                return false;
            }
            string fullpath = Utils.GetMapPath(_filepath);
            if (File.Exists(fullpath)) {
                File.Delete(fullpath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除本地上传的文件(及缩略图)
        /// </summary>
        /// <param name="_filepath"></param>
        public static void DeleteUpFile(string _filepath) {
            if (string.IsNullOrEmpty(_filepath)) {
                return;
            }
            string fullpath = Utils.GetMapPath(_filepath); //原图
            if (File.Exists(fullpath)) {
                FileHelp.DeleteFile(fullpath);
            }
            if (_filepath.LastIndexOf("/") >= 0) {
                string thumbnailpath = _filepath.Substring(0, _filepath.LastIndexOf("/")) + "mall_" + _filepath.Substring(_filepath.LastIndexOf("/") + 1);
                string fullTPATH = Utils.GetMapPath(thumbnailpath); //宿略图
                if (File.Exists(fullTPATH)) {
                    FileHelp.DeleteFile(fullTPATH);
                }
            }
        }

        /// <summary>
        /// 删除内容图片
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="startstr">匹配开头字符串</param>
        public static void DeleteContentPic(string content, string startstr) {
            if (string.IsNullOrEmpty(content)) {
                return;
            }
            Regex reg = new Regex("IMG[^>]*?src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|'(?<1>[^\']*)')", RegexOptions.IgnoreCase);
            MatchCollection m = reg.Matches(content);
            foreach (Match math in m) {
                string imgUrl = math.Groups[1].Value;
                string fullPath = Utils.GetMapPath(imgUrl);
                try {
                    if (imgUrl.ToLower().StartsWith(startstr.ToLower()) && File.Exists(fullPath)) {
                        FileHelp.DeleteFile(fullPath);
                    }
                } catch { }
            }
        }

        /// <summary>
        /// 删除指定文件夹
        /// </summary>
        /// <param name="_dirpath">文件相对路径</param>
        public static bool DeleteDirectory(string _dirpath) {
            if (string.IsNullOrEmpty(_dirpath)) {
                return false;
            }
            string fullpath = Utils.GetMapPath(_dirpath);
            if (Directory.Exists(fullpath)) {
                Directory.Delete(fullpath, true);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改指定文件夹名称
        /// </summary>
        /// <param name="old_dirpath">旧相对路径</param>
        /// <param name="new_dirpath">新相对路径</param>
        /// <returns>bool</returns>
        public static bool MoveDirectory(string old_dirpath, string new_dirpath) {
            if (string.IsNullOrEmpty(old_dirpath)) {
                return false;
            }
            string fulloldpath = Utils.GetMapPath(old_dirpath);
            string fullnewpath = Utils.GetMapPath(new_dirpath);
            if (Directory.Exists(fulloldpath)) {
                Directory.Move(fulloldpath, fullnewpath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回文件大小KB
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>int</returns>
        public static int GetFileSize(string _filepath) {
            if (string.IsNullOrEmpty(_filepath)) {
                return 0;
            }
            string fullpath = Utils.GetMapPath(_filepath);
            if (File.Exists(fullpath)) {
                FileInfo fileInfo = new FileInfo(fullpath);
                return ((int)fileInfo.Length) / 1024;
            }
            return 0;
        }

        /// <summary>
        /// 返回文件扩展名，不含“.”
        /// </summary>
        /// <param name="_filepath">文件全名称</param>
        /// <returns>string</returns>
        public static string GetFileExt(string _filepath) {
            if (string.IsNullOrEmpty(_filepath)) {
                return string.Empty;
            }
            return Path.GetExtension(_filepath).Trim('.');
        }

        /// <summary>
        /// 返回文件名，不含路径
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>string</returns>
        public static string GetFileName(string _filepath) {
            return _filepath.Substring(_filepath.LastIndexOf(@"/") + 1);
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>bool</returns>
        public static bool FileExists(string _filepath) {
            string fullpath = Utils.GetMapPath(_filepath);
            if (File.Exists(fullpath)) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得远程字符串
        /// </summary>
        public static string GetDomainStr(string key, string uriPath) {
            string result = CacheHelper.Get(key) as string;
            if (result == null) {
                System.Net.WebClient client = new System.Net.WebClient();
                try {
                    client.Encoding = System.Text.Encoding.UTF8;
                    result = client.DownloadString(uriPath);
                } catch {
                    result = "暂时无法连接!";
                }
                CacheHelper.Insert(key, result, 60);
            }

            return result;
        }
    }
}

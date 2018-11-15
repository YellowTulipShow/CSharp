using System;
using System.IO;
using System.Text;

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
    }
}

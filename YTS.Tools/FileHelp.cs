using System;
using System.IO;
using System.Text;

namespace YTS.Tools
{
    public static class FileHelp
    {
        public static string OnlyRead(string abs_file_path) {
            return OnlyRead(abs_file_path, Const.Format.FILE_ENCODING);
        }
        public static string OnlyRead(string abs_file_path, Encoding encoding) {
            if (!PathHelp.IsAbsolute(abs_file_path)) {
                return string.Empty;
            }
            using (FileStream rfs = File.OpenRead(abs_file_path)) {
                using (StreamReader sr = new StreamReader(rfs, encoding)) {
                    string content = sr.ReadToEnd();
                    return content;
                }
            }
        }

        public static void OnlyWrite(string abs_file_path, string content) {
            OnlyWrite(abs_file_path, content, Const.Format.FILE_ENCODING);
        }
        public static void OnlyWrite(string abs_file_path, string content, Encoding encoding) {
            if (!PathHelp.IsAbsolute(abs_file_path)) {
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

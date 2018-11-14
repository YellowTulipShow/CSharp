using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace YTS.Tools
{
    /// <summary>
    /// 文件,文件夹路径操作类
    /// </summary>
    public static class PathHelp
    {
        /// <summary>
        /// 将相对路径转换为绝对路径
        /// </summary>
        /// <param name="relative">一个'相对路径'</param>
        /// <returns>肯定是绝对路径的路径</returns>
        public static string ToAbsolute(string relative) {
            if (CheckData.IsStringNull(relative)) {
                return string.Empty;
            }
            if (IsAbsolute(relative)) {
                return relative;
            }
            if (!CheckData.IsObjectNull(HttpContext.Current)) {
                return HttpContext.Current.Server.MapPath(relative);
            }
            relative = relative.TrimStart('/');
            relative = FilterDisablePathChar(relative);
            relative = Regex.Replace(relative, @"/{2,}", @"/");
            relative = relative.Replace(@"/", @"\");
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relative);
        }

        /// <summary>
        /// 过滤禁用路径错误字符
        /// </summary>
        public static string FilterDisablePathChar(string path) {
            return ConvertTool.FilterDisableChars(path, Path.GetInvalidPathChars());
        }
        /// <summary>
        /// 过滤禁用文件名称路径错误字符
        /// </summary>
        public static string FilterDisableFileNameChar(string filepath) {
            return ConvertTool.FilterDisableChars(filepath, Path.GetInvalidFileNameChars());
        }

        /// <summary>
        /// 是否是绝对路径
        /// </summary>
        /// <param name="path_string">路径字符串</param>
        /// <returns>True是, False否</returns>
        public static bool IsAbsolute(string path_string) {
            if (CheckData.IsStringNull(path_string)) {
                return false;
            }
            return Regex.IsMatch(path_string, @"^([a-zA-Z]:\\){1}[^\/\:\*\?\""\<\>\|\,]*$");
        }

        /// <summary>
        /// 创建使用文件路径
        /// </summary>
        /// <param name="directory">使用文件的文件夹路径</param>
        /// <param name="filename">文件名称</param>
        /// <returns>绝对路径的文件路径</returns>
        public static string CreateUseFilePath(string directory, string filename) {
            directory = ConvertTool.ToString(directory);
            if (CheckData.IsStringNull(directory)) {
                directory = @"/";
            }
            filename = ConvertTool.ToString(filename);
            if (CheckData.IsStringNull(filename)) {
                return string.Empty;
            }

            string abs_directory = ToAbsolute(directory).TrimEnd('\\');
            if (!Directory.Exists(abs_directory)) {
                Directory.CreateDirectory(abs_directory);
            }
            string abs_filename = FilterDisableFileNameChar(filename);
            return string.Format("{0}\\{1}", abs_directory, abs_filename);
        }

        /// <summary>
        /// 创建文件, 保证文件存在
        /// </summary>
        /// <param name="absfilepath">文件绝对路径</param>
        public static void CreateFileExists(string absfilepath) {
            if (!IsAbsolute(absfilepath)) {
                return;
            }
            if (File.Exists(absfilepath)) {
                return;
            }
            using (FileStream fs = File.Create(absfilepath)) {
                // 关闭连接
                fs.Dispose();
                fs.Close();
            }
        }

        /// <summary>
        /// 获得系统自动生成文件夹相对路径
        /// </summary>
        public static string SystemAutoGeneratesFolder() {
            return string.Format("/{0}", ConvertTool.ToPathSymbol(Const.Names.SYSTEM_AUTO_GENERATES_PATH));
        }
    }
}

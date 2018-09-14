using System;
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
            if (relative.ToLower().StartsWith("http://")) {
                return relative;
            }
            if (HttpContext.Current != null) {
                return HttpContext.Current.Server.MapPath(relative);
            } else {
                //非web程序引用
                relative = relative.Replace("/", "\\");
                if (relative.StartsWith("\\")) {
                    int startindex = relative.IndexOf('\\', 0);
                    string substr = relative.Substring(startindex);
                    relative = substr.TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relative);
            }
        }

        /// <summary>
        /// 是否是绝对路径
        /// </summary>
        /// <param name="path_string">路径字符串</param>
        /// <returns>True是, False否</returns>
        public static bool IsAbsolute(string path_string) {
            return Regex.IsMatch(path_string, @"^([a-zA-Z]:\\)?[^\/\:\*\?\""\<\>\|\,]*$");
        }
    }
}

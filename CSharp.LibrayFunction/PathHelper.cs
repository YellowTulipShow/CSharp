using System;
using System.Text.RegularExpressions;
using System.Web;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 文件,文件夹路径操作类
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// 将相对路径转换为绝对路径
        /// </summary>
        public static string ConvertToAbsolutePath(string RelativePath) {
            if (CheckData.IsStringNull(RelativePath)) {
                return string.Empty;
            }
            if (Regex.IsMatch(RelativePath, @"^([a-zA-Z]:\\)?[^\/\:\*\?\""\<\>\|\,]*$")) {
                return RelativePath;
            }
            if (RelativePath.ToLower().StartsWith("http://")) {
                return RelativePath;
            }
            if (HttpContext.Current != null) {
                return HttpContext.Current.Server.MapPath(RelativePath);
            } else //非web程序引用
            {
                RelativePath = RelativePath.Replace("/", "\\");
                if (RelativePath.StartsWith("\\")) {
                    int startindex = RelativePath.IndexOf('\\', 0);
                    string substr = RelativePath.Substring(startindex);
                    RelativePath = substr.TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RelativePath);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    public class JsonConfigHelper
    {
        // public static T GetAppSettings<T>(string fileName, string key) where T : class, new()
        // {
        //     // 获取bin目录路径
        //     var directory = AppContext.BaseDirectory;
        //     directory = directory.Replace("\\", "/");

        //     var filePath = $"{directory}/{fileName}";
        //     if (!File.Exists(filePath))
        //     {
        //         var length = directory.IndexOf("/bin");
        //         filePath = $"{directory.Substring(0, length)}/{fileName}";
        //     }

        //     //添加 json 文件路径
        //     var builder = new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("appsettings.json");
        //     //创建配置根对象
        //     var configurationRoot = builder.Build();
        //     configurationRoot

        //     return appconfig;
        // }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 解析动态页面
    /// </summary>
    public class AnalyticDynamicPage
    {
        public AnalyticDynamicPage() { }

        /// <summary>
        /// 获取页面文件内容
        /// </summary>
        public string GetPageFileContent()
        {
            //string templetFullPath = @"E:\JianGuoYunFolder\YellowTulipShowSystem\YTS.CSharp\CSharp.Web\templets\YTSTemp\Main_Arena.html";

            string templetFullPath = @"/CSharp.Web/templets/YTSTemp/Main_Arena.html";

            templetFullPath = Utils.GetMapPath(templetFullPath);

            StringBuilder strReturn = new StringBuilder(220000); //返回的字符
            using (StreamReader objReader = new StreamReader(templetFullPath, Encoding.UTF8))
            {
                StringBuilder extNamespace = new StringBuilder(); //命名空间标签转换容器
                StringBuilder textOutput = new StringBuilder(70000);
                textOutput.Append(objReader.ReadToEnd());
                objReader.Close();

                strReturn = textOutput;
            }
            return strReturn.ToString();
        }
    }
}

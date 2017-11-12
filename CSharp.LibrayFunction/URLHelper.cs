using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 操作URL处理类
    /// </summary>
    public class URLHelper
    {
        /// <summary>
        /// 获得URL 协议部分如: http://
        /// </summary>
        /// <param name="urllink"></param>
        /// <returns>返回带有'://'字符,使用时需Trim()掉</returns>
        public static string GetProtocol(string urllink)
        {
            if (urllink.IndexOf("://") <= 0)
            {
                return "";
            }
            return urllink.Substring(0, urllink.IndexOf("://") + 3);
        }

        /// <summary>
        /// 获得URL 主机部分(域名/IP)如: www.baidu.com/127.0.0.1
        /// </summary>
        /// <param name="urllink"></param>
        public static string GetHost(string urllink)
        {
            if (urllink.IndexOf(":") <= 0)
            {
                return "";
            }
            string[] sarr = GetHostAndPort(urllink).Split(':');
            return sarr.Length >= 1 ? sarr[0] : "";
        }

        /// <summary>
        /// 获得URL 端口部分如: 80 获取不到返回 "80"
        /// </summary>
        /// <param name="urllink"></param>
        /// <returns></returns>
        public static int GetPort(string urllink)
        {
            if (urllink.IndexOf(":") <= 0)
            {
                return 80;
            }
            string[] sarr = GetHostAndPort(urllink).Split(':');
            return sarr.Length >= 2 ? Convert.ToInt32(sarr[1]) : 80;
        }

        /// <summary>
        /// 获得URL 主机+端口内容
        /// </summary>
        /// <param name="urllink"></param>
        public static string GetHostAndPort(string urllink)
        {
            string protocol = GetProtocol(urllink);
            if (protocol == "")
            {
                return "";
            }

            urllink = urllink.Replace(protocol, "");
            return urllink.Split('/')[0];
        }

        /// <summary>
        /// 获得Url 页面地址部分 例: /admin/index.aspx
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetPageAddress(string url)
        {
            string hostport = GetHostAndPort(url);
            if (hostport != "")
            {
                int w1 = url.IndexOf(hostport) + hostport.Length;
                url = url.Substring(w1, url.Length - w1);
            }

            char duanChar = '?';
            if (url.IndexOf(duanChar) < 0)
                duanChar = '&';
            int w2 = url.IndexOf(duanChar);

            if (w2 > 0)
            {
                url = url.Substring(0, w2);
            }

            return url;
        }

        /// <summary>
        /// 获得URL参数部分之前的内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetParamsBeforeContent(string url)
        {
            int wen = url.IndexOf('?');
            int and = url.IndexOf('&');

            if (and >= 0)
                url = url.Substring(0, and);
            if (wen >= 0)
                url = url.Substring(0, wen);

            return url;
        }

        /// <summary>
        /// 获取URL 参数部分字符串 忽略#号后面内容 如: action=testaction&id=1&msg=testmsg&...
        /// </summary>
        /// <param name="url"></param>
        /// <returns>错误和没有返回 "" 空</returns>
        public static string GetParamsString(string url)
        {
            // string url1 = "www.baidu.com/i.html?id=1&action=testa&msg=testm#adas";
            // string url2 = "www.baidu.com/i.html&id=1&action=testa&msg=testm#asda";
            // string url3 = "id=1&action=testa&msg=testm#asda";

            // 首先剔除掉#号后面值
            int jing = url.IndexOf('#');
            if (jing >= 0)
                url = url.Substring(0, jing);

            // 标识 开始的位置
            int start = 0;

            int wen = url.IndexOf('?');
            int and = url.IndexOf('&');
            int deng = url.IndexOf('=');

            //if (deng < 0) // 如果地址里连 = 号都没有证明没有没有参数
            //    return "";

            if (wen >= 0) // 有 ? 号 直接从 ? 号开始走
            {
                start = ++wen;
            }
            else  // 没有 ? 号 那要从 & 号开始走
            {
                if (and < deng && and >= 0) // 在有 & 号的情况下判断是不是在 = 号前面
                {
                    start = ++and;
                }
                else // 既然 & 不在 = 号前面 证明本身url就是个参数列表
                {
                    start = 0;
                }
            }

            url = url.Substring(start, url.Length - start);
            return url;
        }

        /// <summary>
        /// 将URL 参数部分字符串转为 Dictionary键值对集合
        /// </summary>
        /// <param name="paramsString"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetParamsDictionary(string url)
        {
            string paramsString = "";
            return GetParamsDictionary(url, out paramsString);
        }

        /// <summary>
        /// 将URL 参数部分字符串转为 Dictionary键值对集合
        /// </summary>
        /// <param name="paramsString"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetParamsDictionary(string url, out string paramsString)
        {
            paramsString = GetParamsString(url);

            Dictionary<string, string> arrul = new Dictionary<string, string>();
            if (paramsString.IndexOf('=') < 0)
                return arrul;

            string[] array = paramsString.Split('&');
            for (int i = 0; i < array.Length; i++)
            {
                string[] hash = array[i].Split('=');
                if (!arrul.ContainsKey(hash[0]))
                {
                    arrul.Add(hash[0], hash[1]);
                }
                else
                {
                    arrul[hash[0]] = hash[1];
                }
            }
            return arrul;
        }

        /// <summary>
        /// 将Dictionary格式的集合转为url形式的参数字符串
        /// </summary>
        /// <param name="paramsDictionary"></param>
        /// <returns></returns>
        public static string ConvertDictionaryToString(Dictionary<string, string> paramsDictionary)
        {
            StringBuilder resuStr = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in paramsDictionary)
            {
                resuStr.Append(kv.Key + "=" + kv.Value + "&");
            }
            return resuStr.ToString().Trim('&');
        }

    }
}

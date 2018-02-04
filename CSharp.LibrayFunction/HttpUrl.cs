using System;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// Http Url 处理类
    /// </summary>
    public class HttpUrl
    {
        private readonly string sourceUrl;
        public HttpUrl(string sourceUrl) {
            this.sourceUrl = sourceUrl;
        }

        private static string AnalysisURLRegexString() {
            return @"(\w)+";
        }

        public string Host() {
            return string.Empty;
        }

    }
}

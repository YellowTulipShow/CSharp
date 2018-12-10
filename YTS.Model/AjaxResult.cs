using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Common;

namespace YTS.Model
{
    /// <summary>
    /// Ajax请求后返回的结果 数据模型 (不要更改属性的名称, 生产的js需要使用固定的名称)
    /// </summary>
    [Serializable]
    public class AjaxResult : AbsBasicDataModel
    {
        public AjaxResult() { }

        public enum StatusValue
        {
            /// <summary>
            /// 0:失败(默认)
            /// </summary>
            [Explain(@"0:失败(默认)")]
            Error = 0,
            /// <summary>
            /// 1:成功
            /// </summary>
            [Explain(@"1:成功")]
            Success = 1,
            /// <summary>
            /// 2:警告
            /// </summary>
            [Explain(@"2:警告")]
            Warning = 2,
        }

        /// <summary>
        /// 状态值
        /// </summary>
        public StatusValue Status { get { return _status; } set { _status = value; } }
        private StatusValue _status = StatusValue.Error;


        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get { return _msg; } set { _msg = value; } }
        private string _msg = string.Empty;


        /// <summary>
        /// 地址路径
        /// </summary>
        public string Url { get { return _url; } set { _url = value; } }
        private string _url = string.Empty;


        /// <summary>
        /// 跨域Jsonp回调函数返回方法
        /// </summary>
        public string JsonpCallback { get { return _jsonpCallback; } set { _jsonpCallback = value; } }
        private string _jsonpCallback = string.Empty;


        /// <summary>
        /// 结果Json字符串列表
        /// </summary>
        public object ResultContent { get { return _resultContent; } set { _resultContent = value; } }
        private object _resultContent = string.Empty;


        /// <summary>
        /// 二进制资源值
        /// </summary>
        public byte[] BinaryResource { get { return _binaryResource; } set { _binaryResource = value; } }
        private byte[] _binaryResource = new byte[] { };
    }
}

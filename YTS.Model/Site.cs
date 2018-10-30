using System;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.Model
{
    public class Site : AbsShineUpon, IFileInfo
    {
        public Site() { }

        public string GetPathFolder() {
            return string.Empty;
        }

        public string GetFileName() {
            return @"Site.config";
        }

        #region === Model Property ===
        /// <summary>
        /// 站点名称
        /// </summary>
        [Explain(@"站点名称")]
        [ShineUponProperty]
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = @"Main";


        /// <summary>
        /// 站点域名
        /// </summary>
        [Explain(@"站点域名")]
        [ShineUponProperty]
        public string Url { get { return _url; } set { _url = value; } }
        private string _url = @"yellowtulipshow.github.io";


        /// <summary>
        /// 公司名称
        /// </summary>
        [Explain(@"公司名称")]
        [ShineUponProperty]
        public string Company { get { return _company; } set { _company = value; } }
        private string _company = string.Empty;


        /// <summary>
        /// 通讯地址
        /// </summary>
        [Explain(@"通讯地址")]
        [ShineUponProperty]
        public string Address { get { return _address; } set { _address = value; } }
        private string _address = string.Empty;


        /// <summary>
        /// 联系电话
        /// </summary>
        [Explain(@"联系电话")]
        [ShineUponProperty]
        public string Tel { get { return _tel; } set { _tel = value; } }
        private string _tel = string.Empty;


        /// <summary>
        /// 传真号码
        /// </summary>
        [Explain(@"传真号码")]
        [ShineUponProperty]
        public string Fax { get { return _fax; } set { _fax = value; } }
        private string _fax = string.Empty;


        /// <summary>
        /// 邮箱
        /// </summary>
        [Explain(@"邮箱")]
        [ShineUponProperty]
        public string Mail { get { return _mail; } set { _mail = value; } }
        private string _mail = string.Empty;


        /// <summary>
        /// 站点备案号
        /// </summary>
        [Explain(@"站点备案号")]
        [ShineUponProperty]
        public string Crod { get { return _crod; } set { _crod = value; } }
        private string _crod = string.Empty;


        /// <summary>
        /// 是否关闭站点
        /// </summary>
        [Explain(@"是否关闭站点")]
        [ShineUponProperty]
        public bool Is_Close { get { return _is_close; } set { _is_close = value; } }
        private bool _is_close = false;


        /// <summary>
        /// 关闭原因
        /// </summary>
        [Explain(@"关闭原因")]
        [ShineUponProperty]
        public string Close_Reason { get { return _close_reason; } set { _close_reason = value; } }
        private string _close_reason = @"正在进行维护...... 请稍后重试";


        /// <summary>
        /// 站点统计代码
        /// </summary>
        [Explain(@"站点统计代码")]
        [ShineUponProperty]
        public string Statistical_Code { get { return _statistical_code; } set { _statistical_code = value; } }
        private string _statistical_code = string.Empty;
        #endregion
    }
}

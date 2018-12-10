using System;
using YTS.Common;

namespace YTS.Model
{
    /// <summary>
    /// 数据库模型: 访客留言
    /// </summary>
    public class visitor_message : AbsTableModelCommonlyUsed
    {
        public override string GetTableName() {
            return @"dt_visitor_message";
        }

        #region Model
        /// <summary>
        /// 访客姓名
        /// </summary>
        public string name { get { return _name; } set { _name = value; } }
        private string _name = string.Empty;

        /// <summary>
        /// 访客电话
        /// </summary>
        public string tel { get { return _tel; } set { _tel = value; } }
        private string _tel = string.Empty;

        /// <summary>
        /// 访客所留内容
        /// </summary>
        public string msg { get { return _msg; } set { _msg = value; } }
        private string _msg = string.Empty;

        /// <summary>
        /// 访客ip地址
        /// </summary>
        public string ipaddress { get { return _ipaddress; } set { _ipaddress = value; } }
        private string _ipaddress = string.Empty;
        #endregion
    }
}

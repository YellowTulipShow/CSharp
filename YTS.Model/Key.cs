using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Model
{
    /// <summary>
    /// 含有 '键' 属性数据模型
    /// </summary>
    public class KeyBasic : AbsBasicDataModel
    {
        public KeyBasic() : base() { }

        /// <summary>
        /// 键:
        /// </summary>
        public string Key { get { return _key; } set { _key = value; } }
        private string _key = string.Empty;
    }
}

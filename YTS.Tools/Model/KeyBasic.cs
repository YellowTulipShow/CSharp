using System;

namespace YTS.Tools.Model
{
    /// <summary>
    /// 含有 '键' 属性数据模型
    /// </summary>
    public class KeyBasic : AbsBasicDataModel
    {
        public KeyBasic() : base() { }
        public KeyBasic(string key)
            : base() {
            this.Key = key;
        }

        /// <summary>
        /// 键:
        /// </summary>
        public string Key { get { return _key; } set { _key = value; } }
        private string _key = string.Empty;
    }
}

using System;
using System.Reflection;
using YTS.Tools;

namespace YTS.Engine.ShineUpon
{
    /// <summary>
    /// 映射解析信息模型
    /// </summary>
    public class ShineUponInfo : Tools.AbsBasicDataModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get { return _Name; } set { _Name = value; } }
        private string _Name = string.Empty;

        /// <summary>
        /// 解释翻译信息
        /// </summary>
        public ExplainAttribute Explain { get { return _explain; } set { _explain = value; } }
        private ExplainAttribute _explain = null;

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo Property { get { return _property; } set { _property = value; } }
        private PropertyInfo _property = null;
    }
}

using System;
using System.Reflection;
using YTS.Model.Attribute;

namespace YTS.Engine
{
    /// <summary>
    /// 抽象信息模型-映射解析
    /// </summary>
    public abstract class AbsInfo_ShineUpon : Model.AbsBasicDataModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get { return _Name; } set { _Name = value; } }
        private string _Name = string.Empty;

        /// <summary>
        /// 解释翻译信息
        /// </summary>
        public Model.Attribute.ExplainAttribute Explain { get { return _explain; } set { _explain = value; } }
        private Model.Attribute.ExplainAttribute _explain = null;

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo Property { get { return _property; } set { _property = value; } }
        private PropertyInfo _property = null;
    }
}

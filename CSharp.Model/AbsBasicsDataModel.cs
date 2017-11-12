using CSharp.LibrayFunction;
using System;

namespace CSharp.Model
{
    /// <summary>
    /// 基础数据模型
    /// </summary>
    [Serializable]
    public abstract class AbsBasicsDataModel
    {
        public AbsBasicsDataModel() { }

        /// <summary>
        /// 深度克隆一个数据模型对象
        /// </summary>
        public AbsBasicsDataModel CloneModelData() {
            return ReflexHelper.CloneObjectAttribute<AbsBasicsDataModel>(this);
        }
    }
}

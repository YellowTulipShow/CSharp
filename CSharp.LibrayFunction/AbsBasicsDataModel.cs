using System;

namespace CSharp.LibrayFunction
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
            return ReflexHelper.CloneProperties(this);
        }

        /// <summary>
        /// 输出为 JSON 格式字符串数据
        /// </summary>
        public override string ToString() {
            return JsonHelper.SerializeObject(this);
        }
    }
}

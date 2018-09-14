using System;
using YTS.Tools;

namespace YTS.Model
{
    /// <summary>
    /// 基础数据模型
    /// </summary>
    [Serializable]
    public abstract class AbsBasicDataModel
    {
        public AbsBasicDataModel() { }

        /// <summary>
        /// 深度克隆一个数据模型对象
        /// </summary>
        public AbsBasicDataModel CloneModelData() {
            return ReflexHelp.CloneProperties(this);
        }

        /// <summary>
        /// 输出为 JSON 格式字符串数据
        /// </summary>
        public string ToJSONString() {
            return JSON.SerializeObject(this);
        }

        #region ====== Is Can Use Model ======
        /// <summary>
        /// 检查模型可以使用 单例
        /// </summary>
        /// <typeparam name="M">数据模型类型</typeparam>
        /// <param name="item">数据来源</param>
        /// <param name="errorMethod">不能使用的判断条件</param>
        /// <returns>True: 可以使用, 反之亦然</returns>
        public static bool IsCanUse<M>(M item, Func<M, bool> errorMethod) where M : AbsBasicDataModel {
            return !(CheckData.IsObjectNull(item) || errorMethod(item));
        }

        /// <summary>
        /// 检查模型可以使用 多例
        /// </summary>
        /// <typeparam name="M">数据模型类型</typeparam>
        /// <param name="array">数据来源</param>
        /// <param name="errorMethod">不能使用的判断条件</param>
        /// <returns>True: 可以使用, 反之亦然</returns>
        public static bool IsCanUse<M>(M[] array, Func<M, bool> errorMethod) where M : AbsBasicDataModel {
            if (!CheckData.IsSizeEmpty(array)) {
                foreach (M item in array) {
                    if (IsCanUse(item, errorMethod)) {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}

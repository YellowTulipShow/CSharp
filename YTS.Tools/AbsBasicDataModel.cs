using System;

namespace YTS.Tools
{
    /// <summary>
    /// 基础数据模型
    /// </summary>
    [Serializable]
    public abstract class AbsBasicDataModel
    {
        public AbsBasicDataModel() { }

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

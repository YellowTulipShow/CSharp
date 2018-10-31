using System;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.ShineUpon
{
    /// <summary>
    /// 映射分析器
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="I">信息结果类型</typeparam>
    public class ShineUponParser<M, I> :
        ShineUponTypeParser<I>
        where M : AbsShineUpon
        where I : ShineUponInfo
    {
        public ShineUponParser() : base(typeof(M)) { }

        #region === Data: Get/Set ===
        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="model">数据来源</param>
        /// <returns>键值数据</returns>
        public KeyObject GetValue_KeyObject(I info, M model) {
            return base.GetValue_KeyObject(info, model);
        }

        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="model">数据来源</param>
        /// <returns>键值数据</returns>
        public KeyString GetValue_KeyString(I info, M model) {
            return base.GetValue_KeyString(info, model);
        }

        /// <summary>
        /// 设置_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="target_model">目标模型</param>
        /// <param name="ov">数据</param>
        /// <returns>目标模型</returns>
        public void SetValue_Object(I info, M target_model, object ov) {
            base.SetValue_Object(info, target_model, ov);
        }
        #endregion
    }
}

using System;

namespace YTS.DAL
{
    /// <summary>
    /// 接口 - 默认数据映射模型
    /// </summary>
    public interface IDefaultRecord<M> where M : Model.AbsShineUpon
    {
        /// <summary>
        /// 是否需要默认记录
        /// </summary>
        /// <returns>是(True), 否(False)</returns>
        bool IsNeedDefaultRecord();

        /// <summary>
        /// 获取固定的默认记录集合
        /// </summary>
        /// <returns>映射数据模型列表</returns>
        M[] GetDefaultRecordGather();

        /// <summary>
        /// 填充默认记录集合
        /// </summary>
        void FillDefaultRecordGather();
    }
}

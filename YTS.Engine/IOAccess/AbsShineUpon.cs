using System;
using YTS.Engine.IOAccess;
using YTS.Model.Attribute;

namespace YTS.Model
{
    /// <summary>
    /// 可以进行映射处理的模型
    /// </summary>
    [Serializable]
    [ShineUponModel]
    public abstract class AbsShineUpon : Tools.AbsBasicDataModel, IModel
    {
    }
}

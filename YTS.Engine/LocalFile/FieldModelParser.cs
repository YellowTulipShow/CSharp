using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.LocalFile
{
    /// <summary>
    /// 映射分析器-文件
    /// </summary>
    /// <typeparam name="M">文件映射模型</typeparam>
    public class FieldModelParser<M> : ShineUponParser<M, FieldInfo> where M : AbsShineUpon
    {
        public FieldModelParser() : base() { }
    }
}

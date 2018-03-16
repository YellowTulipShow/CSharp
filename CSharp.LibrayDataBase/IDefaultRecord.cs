using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 接口 - 数据表默认数据模型
    /// </summary>
    public interface IDefaultRecord<M> where M : AbsModelNull
    {
        M DefaultDataModel();
    }
}

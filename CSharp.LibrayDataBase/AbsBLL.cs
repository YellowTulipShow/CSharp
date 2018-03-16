using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表-调用类
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">Model数据映射模型</typeparam>
    public abstract class AbsBLL<D, M>
        where D : AbsDAL<M>
        where M : AbsModelNull
    {
        public D SelfDAL { get { return _selfDAL_; } }
        private D _selfDAL_ = null;

        public AbsBLL(D dal) {
            this._selfDAL_ = dal;
        }
    }
}

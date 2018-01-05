using System;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表-调用类
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">Model数据映射模型</typeparam>
    public class AbsTableBLL<D, M>
        where D : AbsTableDAL<M>
        where M : AbsModel_Null
    {
        public D TableDAL;
        public AbsTableBLL(D dal) {
            this.TableDAL = dal;
        }
    }
}

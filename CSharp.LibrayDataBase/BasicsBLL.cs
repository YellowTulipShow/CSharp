using System;
using System.Data;
using System.Collections.Generic;
using CSharp.LibrayFunction;
using CSharp.Model.Table;

namespace CSharp.LibrayDataBase
{
    public class BasicsBLL<M> : ITableBasicFunction<M> where M : AbsTableModel
    {
        #region Init Function
        protected BasicsDAL<M> dal;
        public BasicsBLL(Object dal) {
            this.dal = (BasicsDAL<M>)dal;
        }
        #endregion

        #region ====== Tools Function ======
        /// <summary>
        /// 获得表名
        /// </summary>
        /// <returns></returns>
        public string GetTableName() {
            return dal.GetTableName();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">传入数据表中的自增ID值</param>
        /// <returns>true: 存在  false: 不在</returns>
        public bool Exists(int id) {
            return dal.Exists(id);
        }
        #endregion

        #region ====== SQL Language Execute ======
        /// <summary>
        /// 返回数据总数(仅单个数据表模型)
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>数据总数</returns>
        public virtual int GetCount(string strWhere) {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 增加一条数据(仅单个数据表模型)
        /// </summary>
        /// <param name="model">Model数据表模型</param>
        /// <returns>返回添加到的自增ID SQL 全局变量: @@IDENTITY</returns>
        public virtual int Add(M model) {
            return dal.Add(model);
        }

        /// <summary>
        /// 删除一条数据(仅单个数据表模型)
        /// </summary>
        /// <param name="id">传入数据表中的自增ID值</param>
        /// <returns>true: 成功 / false: 失败</returns>
        public virtual bool Delete(int id) {
            return dal.Delete(id);
        }

        /// <summary>
        /// 更新一条数据(仅单个数据表模型)
        /// </summary>
        /// <param name="model">Model数据表模型</param>
        /// <returns>true: 成功 / false: 失败</returns>
        public virtual bool Update(M model) {
            return dal.Update(model);
        }

        /// <summary>
        /// 修改一列数据(仅单个数据表模型)
        /// </summary>
        /// <param name="id">传入数据表中的自增ID值</param>
        /// <param name="strValue">更新的内容,例: 字段=值 (值记得加 '')</param>
        /// <returns>true: 成功 / false: 失败</returns>
        public virtual bool UpdateField(int id, string strValue) {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 多条SQL使用事务执行
        /// </summary>
        /// <param name="modellist"></param>
        /// <returns></returns>
        public bool Transaction(string[] strlist) {
            return dal.Transaction(strlist);
        }
        #endregion

        #region ====== SQL Select Data ======
        /// <summary>
        /// 获得一个对象实体(仅单个数据表模型)
        /// </summary>
        /// <param name="id">传入数据表中的自增ID值</param>
        /// <returns>Model数据表模型</returns>
        public M GetModel(int id) {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 将对象转换实体(仅单个数据表模型)
        /// </summary>
        /// <param name="row">单条数据行</param>
        /// <returns>Model数据表模型</returns>
        public M DataRowToModel(DataRow row) {
            return dal.DataRowToModel(row);
        }

        /// <summary>
        /// 获得前几行数据(仅单个数据表模型)
        /// </summary>
        /// <param name="Top">前几行 大于0有效</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="filedOrder">排序用的字段</param>
        /// <returns>DataSet数据集</returns>
        public DataSet GetList(int Top, string strWhere, string filedOrder) {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据(仅单个数据表模型)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="filedOrder">排序用的字段</param>
        /// <param name="recordCount">记录总数可以执行返回查看</param>
        /// <returns></returns>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount) {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得数据模型列表
        /// </summary>
        /// <param name="dt">用于转换的数据源表</param>
        /// <returns></returns>
        public virtual List<M> GetModelList(DataTable dt) {
            return dal.GetModelList(dt);
        }
        #endregion

        #region ====== SQL String Get ======
        /// <summary>
        /// 获得添加数据SQL字符串
        /// </summary>
        /// <param name="model">Model数据表模型</param>
        /// <returns></returns>
        public string SQLStringModelAdd(M model) {
            return dal.SQLStringModelAdd(model);
        }

        /// <summary>
        /// 获得更新数据SQL字符串
        /// </summary>
        /// <param name="id">更新条件数据ID号</param>
        /// <param name="Model">Model数据表模型</param>
        /// <returns></returns>
        public string SQLStringModelUpdate(int id, M Model) {
            return dal.SQLStringModelUpdate(id, Model);
        }
        #endregion
    }
}

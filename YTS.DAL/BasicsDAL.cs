using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using YTS.Tools;
using YTS.Model;
using YTS.Engine.DataBase.MSQLServer;

namespace YTS.DAL
{
    public class BasicsDAL<M> : ITableBasicFunction<M> where M : AbsTableModel
    {
        #region Init Function
        /// <summary>
        /// 规范 属性 数据模型
        /// </summary>
        private M defaultNullModel;

        /// <summary>
        /// 数据表名称
        /// </summary>
        public String TableName {
            get { return defaultNullModel.GetTableName(); }
        }

        /// <summary>
        /// 获得数据模型
        /// </summary>
        protected M ClonedefModel {
            get {
                return ReflexHelp.CreateNewObject<M>();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BasicsDAL(M model) {
            this.defaultNullModel = model;
        }
        #endregion

        #region interface ITableBasicFunction<M> Defined methods
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">传入数据表中的自增ID值</param>
        /// <returns>true: 存在  false: 不在</returns>
        public virtual bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from  " + TableName);
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据(仅单个数据表模型)
        /// </summary>
        /// <param name="model">Model数据表模型</param>
        /// <returns>返回添加到的自增ID SQL 全局变量: @@IDENTITY</returns>
        public virtual int Add(M model)
        {
            string strSql = SQLStringModelAdd(model) + " ;select @@IDENTITY; ";
            object obj = DbHelperSQL.GetSingle(strSql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据(仅单个数据表模型)
        /// </summary>
        /// <param name="model">Model数据表模型</param>
        /// <returns>true: 成功 / false: 失败</returns>
        public virtual bool Update(M model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();
            strSql.Append("update " + TableName + " set ");

            string strwhere = null;
            foreach (PropertyInfo pi in pros)
            {
                //如果不是主键则追加sql字符串
                if (!pi.Name.Equals("id") && !typeof(System.Collections.IList).IsAssignableFrom(pi.PropertyType))
                {
                    //判断属性值是否为空
                    if (pi.GetValue(model, null) != null)
                    {
                        str1.Append(pi.Name + "=@" + pi.Name + ",");//声明参数
                        paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(model, null)));//对参数赋值
                    }
                }
                else if (pi.Name.Equals("id") && (Int32)pi.GetValue(model, null) != 0)
                {
                    strwhere = " where id=@id ";
                    paras.Add(new SqlParameter("@id", pi.GetValue(model, null)));
                }
            }
            strSql.Append(str1.ToString().Trim(','));
            strSql.Append(strwhere);
            if (strwhere == null)
            {
                return false; // 没有唯一ID作为条件 需放弃执行更新语句条件
            }

            return DbHelperSQL.ExecuteSql(strSql.ToString(), paras.ToArray()) > 0;
        }

        /// <summary>
        /// 多条SQL使用事务执行
        /// </summary>
        /// <param name="modellist"></param>
        /// <returns></returns>
        public virtual bool Transaction(List<string> strlist)
        {
            if (strlist.Count <= 0)
            {
                return false;
            }
            bool resu = DbHelperSQL.ExecuteTransaction(strlist);
            return resu;
        }

        /// <summary>
        /// 获得添加数据SQL字符串
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string SQLStringModelAdd(M model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder sql_field = new StringBuilder();//数据字段
            StringBuilder sql_params = new StringBuilder();//数据参数

            // 利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();

            List<SqlParameter> paras = new List<SqlParameter>();
            strSql.Append("insert into  " + TableName + "(");
            foreach (PropertyInfo pi in pros)
            {
                //如果不是主键则追加sql字符串
                if (!pi.Name.Equals("id"))
                {
                    //判断属性值是否为空
                    if (pi.GetValue(model, null) != null)
                    {
                        sql_field.Append(pi.Name + ",");//拼接字段
                        sql_params.Append("'" + pi.GetValue(model, null) + "',");
                    }
                }
            }
            strSql.Append(sql_field.ToString().Trim(','));
            strSql.Append(") values (");
            strSql.Append(sql_params.ToString().Trim(','));
            strSql.Append(") ");
            return strSql.ToString();
        }

        /// <summary>
        /// 获得更新数据SQL字符串
        /// </summary>
        /// <param name="id">更新条件数据ID号</param>
        /// <param name="Model">Model数据表模型</param>
        /// <returns></returns>
        public string SQLStringModelUpdate(int id, M model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strset = new StringBuilder();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();
            strSql.Append("update " + TableName + " set ");

            string strwhere = null;
            foreach (PropertyInfo pi in pros)
            {
                //如果不是主键则追加sql字符串
                if (!pi.Name.Equals("id") && !typeof(System.Collections.IList).IsAssignableFrom(pi.PropertyType))
                {
                    //判断属性值是否为空
                    if (pi.GetValue(model, null) != null)
                    {
                        strset.AppendFormat(" {0} = '{1}', ", pi.Name, pi.GetValue(model, null));
                    }
                }
                else if (pi.Name.Equals("id") && (Int32)pi.GetValue(model, null) != 0)
                {
                    strwhere = String.Format(" where id = '{0}' ", id);
                }
            }
            strSql.Append(strset.ToString().Trim().Trim(','));
            strSql.Append(strwhere);
            if (strwhere == null)
            {
                return ""; // 没有唯一ID作为条件 需放弃执行更新语句条件
            }
            return strSql.ToString();
        }

        /// <summary>
        /// 删除一条数据(仅单个数据表模型)
        /// </summary>
        /// <param name="id">传入数据表中的自增ID值</param>
        /// <returns>true: 成功 / false: 失败</returns>
        public virtual bool Delete(int id)
        {
            return DbHelperSQL.ExecuteSql(SQLStringModelDelete(id)) > 0;
        }

        /// <summary>
        /// 获得 删除ID记录模型 SQL字符串
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <returns></returns>
        public string SQLStringModelDelete(int id) {
            return String.Format("delete from {0} where id = '{1}'", TableName, id.ToString());
        }

        /// <summary>
        /// 获得一个对象实体(仅单个数据表模型)
        /// </summary>
        /// <param name="id">传入数据表中的自增ID值</param>
        /// <returns>Model数据表模型</returns>
        public virtual M GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = ClonedefModel.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                //拼接字段，忽略List<T>
                if (!typeof(System.Collections.IList).IsAssignableFrom(p.PropertyType))
                {
                    str1.Append(p.Name + ",");//拼接字段
                }
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + TableName);
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];

            if (dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return default(M);
            }
        }

        /// <summary>
        /// 将对象转换实体(仅单个数据表模型)
        /// </summary>
        /// <param name="row">单条数据行</param>
        /// <returns>Model数据表模型</returns>
        public virtual M DataRowToModel(DataRow row)
        {
            M model = ClonedefModel;
            if (row != null)
            {
                //利用反射获得属性的所有公共属性
                Type modelType = model.GetType();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    //查找实体是否存在列表相同的公共属性
                    PropertyInfo proInfo = modelType.GetProperty(row.Table.Columns[i].ColumnName);
                    if (proInfo != null && row[i] != DBNull.Value)
                    {
                        object vl = row[i];
                        proInfo.SetValue(model, vl, null);//用索引值设置属性值
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获得前几行数据(仅单个数据表模型)
        /// </summary>
        /// <param name="Top">前几行 大于0有效</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="filedOrder">排序用的字段</param>
        /// <returns>DataSet数据集</returns>
        public virtual DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }

            strSql.Append(" * ");
            strSql.Append(" from  " + TableName);

            if (!string.IsNullOrEmpty(strWhere) && !string.IsNullOrWhiteSpace(strWhere))
            {
                strWhere = strWhere.Trim();
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrEmpty(filedOrder) && !string.IsNullOrWhiteSpace(filedOrder))
            {
                filedOrder = filedOrder.Trim();
                strSql.Append(" order by " + filedOrder);
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 获得数据模型列表
        /// </summary>
        /// <param name="dt">用于转换的数据源表</param>
        /// <returns></returns>
        public virtual List<M> GetModelList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<M>();
            }
            List<M> Mlist = new List<M>();
            foreach (DataRow dr in dt.Rows)
            {
                Mlist.Add(DataRowToModel(dr));
            }
            return Mlist;
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
        public virtual DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + TableName);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            DataSet ds = DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
            return ds;
        }

        /// <summary>
        /// 返回数据总数(仅单个数据表模型)
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>数据总数</returns>
        public virtual int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + TableName);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据(仅单个数据表模型)
        /// </summary>
        /// <param name="id">传入数据表中的自增ID值</param>
        /// <param name="strValue">更新的内容,例: 字段=值 (值记得加 '')</param>
        /// <returns>true: 成功 / false: 失败</returns>
        public virtual bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update " + TableName + " set " + strValue);
            strSql.AppendFormat(" where id = '{0}' ", id);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }
        #endregion
    }
}

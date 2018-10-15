using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using YTS.Engine.DataBase;
using YTS.Engine.DataBase.MSQLServer;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// Microsoft SQL Server 2008 数据库-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class DAL_MSSQLServer<M> :
        AbsDAL<M, string, ColumnModelParser<M>, ColumnInfo>,
        ITableName,
        IDataBaseResult<M>,
        ISupplementaryStructure
        where M : AbsShineUpon, ITableName
    {
        public DAL_MSSQLServer()
            : base() {
            // 执行补全
            if (IsNeedSupplementary()) {
                ExecutionSupplementary();
            }
        }

        #region ====== using:ITableName ======
        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public string GetTableName() {
            return this.DefaultModel.GetTableName();
        }
        #endregion

        #region ====== using:AbsDAL<Model, Where, Parser, ParserInfo> ======
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model">数据映射模型</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public override bool Insert(M model) {
            string sqlinsert = ConvertTool.StrToStrTrim(SQLInsert(model, false));
            return CheckData.IsStringNull(sqlinsert) ? false : DbHelperSQL.ExecuteSql(sqlinsert) > 0;
        }

        /// <summary>
        /// 数据映射模型 - 转 - SQL插入语句
        /// </summary>
        /// <param name="model">数据映射模型</param>
        /// <param name="isResultID">是否需要结果ID值</param>
        /// <returns>SQL插入语句</returns>
        public string SQLInsert(M model, bool isResultID) {
            List<string> fieldArr = new List<string>();
            List<string> valueArr = new List<string>();
            foreach (ColumnInfo item in this.Parser.GetColumn_CanWrite()) {
                KeyObject im = this.Parser.GetModelValue(item, model);
                if (CheckData.IsObjectNull(im) || CheckData.IsStringNull(im.Key)) {
                    continue;
                }
                fieldArr.Add(im.Key);
                string str_value = ModelValueToDataBaseValue(im.Value);
                valueArr.Add(string.Format("'{0}'", str_value));
            }
            if ((fieldArr.Count != valueArr.Count) || CheckData.IsSizeEmpty(fieldArr)) {
                return string.Empty;
            }
            return CreateSQL.Insert(this.GetTableName(), fieldArr.ToArray(), valueArr.ToArray(), isResultID);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="models">数据映射模型多条记录</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public override bool Insert(M[] models) {
            string[] sql_inserts = ConvertTool.ListConvertType(models, (model) => SQLInsert(model, false));
            return Transaction(sql_inserts);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public override bool Delete(string where) {
            string sqldelete;
            if (CheckData.IsStringNull(where)) {
                //sqldelete = CreateSQL.DropTable(this.GetTableName()); // ALL Clear
                sqldelete = CreateSQL.TruncateTable(this.GetTableName()); // only delete all data
            } else {
                sqldelete = CreateSQL.Delete(this.GetTableName(), where);
            }
            return CheckData.IsStringNull(sqldelete) ? false : DbHelperSQL.ExecuteSql(sqldelete) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="kos">需要更新的键值</param>
        /// <param name="where">查询条件</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public override bool Update(KeyObject[] kos, string where) {
            if (CheckData.IsSizeEmpty(kos) || CheckData.IsStringNull(where)) {
                return false;
            }
            string[] expressions = ConvertTool.ListConvertType(kos, item => {
                if (CheckData.IsStringNull(item.Key)) {
                    return null;
                }
                string str_value = ModelValueToDataBaseValue(item.Value);
                return string.Format("{0} = '{1}'", item.Key, item.Value);
            }, null);
            string set_str = ConvertTool.IListToString(expressions, ',');
            string sql_update = CreateSQL.Update(this.GetTableName(), set_str, where);
            return CheckData.IsStringNull(sql_update) ? false : DbHelperSQL.ExecuteSql(sql_update) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="top">查询记录数目</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public override M[] Select(int top, string where, KeyBoolean[] sorts) {
            if (CheckData.IsSizeEmpty(sorts)) {
                sorts = GetDefaultSortWhere();
            }
            string sort_order = CreateSQL.OrderBySimp(sorts);
            DataSet ds = QueryRecords(top, where, sort_order);
            return DataSetToModels(ds);
        }

        public KeyBoolean[] GetDefaultSortWhere() {
            return new KeyBoolean[] {
                new KeyBoolean() {
                    Key = this.Parser.GetSortResult()[0].Name,
                    Value = false,
                },
            };
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pageCount">每页展现记录数</param>
        /// <param name="pageIndex">浏览页面索引</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public override M[] Select(int pageCount, int pageIndex, out int recordCount, string where, KeyBoolean[] sorts) {
            if (CheckData.IsSizeEmpty(sorts)) {
                sorts = GetDefaultSortWhere();
            }
            string sort_order = CreateSQL.OrderBySimp(sorts);
            DataSet ds = QueryRecords(pageCount, pageIndex, out recordCount, where, sort_order);
            return DataSetToModels(ds);
        }

        /// <summary>
        /// 获取记录数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        public override int GetRecordCount(string where) {
            const int errorint = 0;
            string sql_select = CreateSQL.Select_Count(this.GetTableName(), where);
            if (CheckData.IsStringNull(sql_select)) {
                return errorint;
            }
            object value = DbHelperSQL.GetSingle(sql_select);
            return ConvertTool.ObjToInt(value, errorint);
        }
        #endregion

        #region ====== using:IDataBaseResult<M> ======
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="sql_where">查询条件</param>
        /// <param name="sql_order">排序条件</param>
        /// <returns>结果数据表</returns>
        public DataSet QueryRecords(int top, string sql_where, string sql_order) {
            string sql_select = CreateSQL.Select(this.GetTableName(), top, sql_where, sql_order);
            if (CheckData.IsStringNull(sql_select)) {
                return new DataSet();
            }
            DataSet ds = DbHelperSQL.Query(sql_select);
            return ds;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="sql_where">定义: 查询条件</param>
        /// <param name="sql_order">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>结果数据表</returns>
        public DataSet QueryRecords(int pageCount, int pageIndex, out int recordCount, string sql_where, string sql_order) {
            if (CheckData.IsStringNull(sql_order)) {
                throw new Exception(@"分页查询 排序条件必须存在!");
            }
            recordCount = GetRecordCount(sql_where);
            string sql_select = CreateSQL.Select(this.GetTableName(), 0, sql_where, string.Empty);
            string sql_paging = PagingHelper.CreatePagingSql(recordCount, pageCount, pageIndex, sql_select, sql_order);
            if (CheckData.IsStringNull(sql_paging)) {
                return new DataSet();
            }
            DataSet ds = DbHelperSQL.Query(sql_paging);
            return ds;
        }

        /// <summary>
        /// 数据集 转为 模型列表
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>模型列表</returns>
        public M[] DataSetToModels(DataSet ds) {
            if (CheckData.IsSizeEmpty(ds)) {
                return new M[] { };
            }
            List<M> list = new List<M>();
            foreach (DataTable dt in ds.Tables) {
                if (CheckData.IsSizeEmpty(dt)) {
                    continue;
                }
                foreach (DataRow dr in dt.Rows) {
                    M model = DataRowToModel(dr);
                    if (!CheckData.IsObjectNull(model)) {
                        list.Add(model);
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 数据行 转为 单个映射模型
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>单个映射模型</returns>
        public M DataRowToModel(DataRow row) {
            if (CheckData.IsSizeEmpty(row)) {
                return null;
            }
            M model = ReflexHelp.CreateNewObject<M>();
            ColumnInfo[] columninfos = this.Parser.GetSortResult();
            foreach (ColumnInfo item in columninfos) {
                if (CheckData.IsObjectNull(item)) {
                    continue;
                }
                object value = row[item.Property.Name];
                model = this.Parser.SetModelValue(item, model, value);
            }
            return model;
        }
        #endregion

        /// <summary>
        /// 执行-SQL字符串事务处理
        /// </summary>
        /// <param name="sqllist">SQL字符串列表</param>
        /// <returns>是否成功</returns>
        public bool Transaction(string[] sqllist) {
            if (CheckData.IsSizeEmpty(sqllist)) {
                return false;
            }
            bool resu = DbHelperSQL.ExecuteTransaction(sqllist);
            return resu;
        }

        # region Supplementary new Method:
        /// <summary>
        /// 是否补全结构
        /// </summary>
        /// <returns>MSQLServer需要补全结构</returns>
        public virtual bool IsNeedSupplementary() {
            return true;
        }

        /// <summary>
        /// 执行补全
        /// </summary>
        public virtual void ExecutionSupplementary() {
            // sql if 判断条件 判断表是否不存在
            string str_if_where = CreateSQL.NotExists(CreateSQL.MSSSysTable(GetTableName()));
            // sql 列的名称 和 数据类型格式 数据源集合
            Dictionary<string, string> columns = GetCreateColumns();

            string[] column_formats = ConvertTool.ListConvertType(columns, d => d.Value);
            // 不存在 需要创建表
            string SQL_CreateTable = CreateSQL.CreateTable(GetTableName(), column_formats);

            //存在 需要补全缺失的列
            string SQL_AlterColumns = SQLAlterColumns(columns);

            string sql = CreateSQL.If(str_if_where, SQL_CreateTable, SQL_AlterColumns);
            DbHelperSQL.GetSingle(sql);
        }

        /// <summary>
        /// 获得列信息
        /// </summary>
        private Dictionary<string, string> GetCreateColumns() {
            Dictionary<string, string> resuDic = new Dictionary<string, string>();
            foreach (ColumnInfo item in this.Parser.GetSortResult()) {
                string fieldName = item.Property.Name;
                if (resuDic.ContainsKey(fieldName)) {
                    continue;
                }
                string typeName = GetMemberInfoMSQLServerDataType(item);
                string[] vals = new string[] {
                    fieldName,
                    typeName,
                    item.Attribute.IsPrimaryKey ? @"primary key" : null,
                    !item.Attribute.IsCanBeNull ? @"not null" : null,
                    item.Attribute.IsIDentity ? @"identity(1,1)" : null,
                };
                resuDic[fieldName] = ConvertTool.IListToString(vals, @" ");
            }
            return resuDic;
        }

        /// <summary>
        /// 获取 数据映射模型元素(属性) 的 数据库对应数据类型
        /// </summary>
        /// <param name="info">数据映射模型元素(属性)</param>
        /// <returns>数据库对应数据类型</returns>
        private string GetMemberInfoMSQLServerDataType(ColumnInfo info) {
            Type detype = info.Property.PropertyType;
            if (CheckData.IsTypeEqual<int>(detype) || CheckData.IsTypeEqual<Enum>(detype, true)) {
                return @"int";
            }
            if (CheckData.IsTypeEqual<float>(detype)) {
                return @"money";
            }
            if (CheckData.IsTypeEqual<double>(detype)) {
                return @"float";
            }
            if (CheckData.IsTypeEqual<DateTime>(detype)) {
                return @"datetime";
            }
            string charlen = !info.Attribute.IsPrimaryKey ? "max" : info.Attribute.CharLength.ToString();
            string str = string.Format("nvarchar({0})", charlen);
            return str;
        }

        /// <summary>
        /// 获得补全表列信息
        /// </summary>
        private string SQLAlterColumns(Dictionary<string, string> columns) {
            List<string> ifExists = new List<string>();
            foreach (KeyValuePair<string, string> item in columns) {
                string if_where = CreateSQL.NotExists(CreateSQL.MSSSysColumns(GetTableName(), item.Key));
                string sql = CreateSQL.If(if_where, CreateSQL.AlterColumn(GetTableName(), item.Value));
                ifExists.Add(sql);
            }
            return ConvertTool.IListToString(ifExists, @" ");
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// SQL创造器
    /// </summary>
    public static class CreateSQL
    {
        #region ====== Const Field String Char ======
        /* ====== Select Part ====== */
        /// <summary>
        /// 查询标识列
        /// </summary>
        public const string SELECT_IDENTITY = @";select @@IDENTITY;";


        /* ====== Where Part ====== */
        /// <summary>
        /// 和并且 and 字段
        /// </summary>
        public const string WHERE_AND = @"and";
        /// <summary>
        /// 或者 or 字段
        /// </summary>
        public const string WHERE_OR = @"or";
        /// <summary>
        /// 取反 not 字段
        /// </summary>
        public const string WHERE_NOT = @"not";
        /// <summary>
        /// 左边 小括号 ( 
        /// </summary>
        public const string WHERE_LEFTPARENTHESES = @"(";
        /// <summary>
        /// 右边 小括号 )
        /// </summary>
        public const string WHERE_RIGHTPARENTHESES = @")";

        /* ====== Order By Part ====== */
        /// <summary>
        /// 排序: 正序
        /// </summary>
        public const string ORDERBY_ASC = @"asc";
        /// <summary>
        /// 排序: 倒序
        /// </summary>
        public const string ORDERBY_DESC = @"desc";
        /// <summary>
        /// 排序: 间隔
        /// </summary>
        public const string ORDERBY_INTERVALSYMBOL = @",";
        #endregion

        #region ====== Where Function ======
        /// <summary>
        /// 小括号包裹: (source)
        /// </summary>
        public static string WhereParenthesesPackage(string source) {
            return WHERE_LEFTPARENTHESES + source + WHERE_RIGHTPARENTHESES;
        }

        /* ====== Where Equal Part ====== */
        /// <summary>
        /// 条件: field = 'values'
        /// </summary>
        public static string WhereEqual(string field, string values) {
            return String.Format("{0} = '{1}'", field, values);
        }
        /// <summary>
        /// 条件: field != 'values'
        /// </summary>
        public static string WhereEqualNot(string field, string values) {
            return String.Format("{0} != '{1}'", field, values);
        }

        /* ====== Where Range Part ====== */
        /// <summary>
        /// 条件: field 小于 'values'
        /// </summary>
        public static string WhereSmallThan(string field, string values) {
            return String.Format("{0} < '{1}'", field, values);
        }
        /// <summary>
        /// 条件: field 小于= 'values'
        /// </summary>
        public static string WhereSmallThanEqual(string field, string values) {
            return String.Format("{0} <= '{1}'", field, values);
        }
        /// <summary>
        /// 条件: field > 'values'
        /// </summary>
        public static string WhereBigThan(string field, string values) {
            return String.Format("{0} > '{1}'", field, values);
        }
        /// <summary>
        /// 条件: field >= 'values'
        /// </summary>
        public static string WhereBigThanEqual(string field, string values) {
            return String.Format("{0} >= '{1}'", field, values);
        }

        /* ====== Where Like Part ====== */
        /// <summary>
        /// 条件: field like '%values%'
        /// </summary>
        public static string WhereLike(string field, string values) {
            return String.Format("{0} like '%{1}%'", field, values);
        }
        /// <summary>
        /// 条件: field not like '%values%'
        /// </summary>
        public static string WhereLikeNot(string field, string values) {
            return String.Format("{0} not like '%{1}%'", field, values);
        }
        /// <summary>
        /// 条件: Convert(varchar,{0},120) like '%{1}%'
        /// </summary>
        public static string WhereLikeTime(string field, string timeFormatValue) {
            string timefield = String.Format("Convert(varchar,{0},120)", field);
            return WhereLike(timefield, timeFormatValue);
        }
        /// <summary>
        /// 条件: ('values' like '%'+field+'%' or field like '%values%')
        /// </summary>
        public static string WhereLikeTwoWay(string field, string values) {
            return String.Format("('{1}' like '%'+{0}+'%' or {0} like '%{1}%')", field, values);
        }
        /// <summary>
        /// 条件查询 根据所有的公共属性遍历
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <param name="values">用于查询的值数组</param>
        public static string WhereLikeAllPropertyInfo(AbsBasicDataModel model, string[] values) {
            PropertyInfo[] ProInfoArrs = new PropertyInfo[] { };
            try { ProInfoArrs = model.GetType().GetProperties(); } catch (Exception) { return String.Empty; }

            // 如果 所有的属性 和 要查询的内容 都小于等于0的话 没必要执行了
            if (ProInfoArrs.Length <= 0 || values.Length <= 0)
                return String.Empty;

            StringBuilder SQLString = new StringBuilder();
            SQLString.Append(WHERE_LEFTPARENTHESES); // 左边 小括号
            for (int proCount = 0; proCount < ProInfoArrs.Length; proCount++) {
                for (int valCount = 0; valCount < values.Length; valCount++) {
                    if (valCount != 0 || proCount != 0)
                        SQLString.Append(WHERE_OR);
                    SQLString.Append(WhereLike(ProInfoArrs[proCount].Name, values[valCount]));
                }
            }
            SQLString.Append(WHERE_RIGHTPARENTHESES); // 右边 小括号
            return SQLString.ToString();
        }
        /// <summary>
        /// 组合内容字段查询 条件: field like '%,values%' or field like '%values,%' or field ='values'
        /// </summary>
        /// <returns></returns>
        public static string WhereLikeSymbolOrEqual(string field, string values, char symbol) {
            return String.Format("({0} like '%{2}{1}%' or {0} like '%{1}{2}%' or {0} ='{1}')", field, values, symbol);
        }

        /* ====== Where In Part ====== */
        /// <summary>
        /// 条件: field in ('v1','v2','v3','v4')
        /// </summary>
        public static string WhereIn(string field, string[] values) {
            return WhereInNot(field, values, false);
        }
        /// <summary>
        /// 条件: field not in ('v1','v2','v3','v4')
        /// </summary>
        public static string WhereInNot(string field, string[] values) {
            return WhereInNot(field, values, true);
        }
        private static string WhereInNot(string field, string[] values, bool isNot) {
            StringBuilder instr = new StringBuilder();
            for (int i = 0; i < values.Length; i++) {
                if (i != 0) {
                    instr.Append(",");
                }
                instr.AppendFormat("'{0}'", values[i]);
            }
            if (CheckData.IsStringNull(instr.ToString())) {
                instr.Append("''");
            }
            string notstr = String.Empty;
            if (isNot) {
                notstr = WHERE_NOT;
            }
            return String.Format("{0} {1} in ({2})", field, notstr, instr.ToString());
        }
        #endregion

        #region ====== Order By Function ======
        /// <summary>
        /// 排序语句: order by field asc
        /// </summary>
        public static string OrderBy(string field, bool isasc) {
            return "order by" + OrderBySimp(field, isasc);
        }
        /// <summary>
        /// 排序语句: order by field asc
        /// </summary>
        public static string OrderBy(Dictionary<string, bool> fields) {
            return "order by" + OrderBySimp(fields);
        }
        /// <summary>
        /// 排序语句(简洁版:适用于GetList): field asc
        /// </summary>
        public static string OrderBySimp(string field, bool isasc) {
            string symbol = String.Empty;
            if (isasc) {
                symbol = ORDERBY_ASC;
            } else {
                symbol = ORDERBY_DESC;
            }
            return field + "" + symbol;
        }
        /// <summary>
        /// 排序语句(简洁版:适用于GetList): field asc
        /// </summary>
        public static string OrderBySimp(Dictionary<string, bool> fields) {
            StringBuilder order = new StringBuilder();
            foreach (KeyValuePair<string, bool> item in fields) {
                order.Append(OrderBySimp(item.Key, item.Value));
                order.Append(ORDERBY_INTERVALSYMBOL);
            }
            return order.ToString().Trim(ORDERBY_INTERVALSYMBOL.ToCharArray()).ToString();
        }
        #endregion

        #region ====== Update Sentence Function ======
        /// <summary>
        /// 获得更新语句
        /// </summary>
        public static string Update(string tableName, string field, string values, string where) {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add(field, values);
            string updatestr = Update(tableName, paramDic, where);
            return updatestr;
        }
        /// <summary>
        /// 获得更新语句 (多个更新内容)
        /// </summary>
        public static string Update(string tableName, Dictionary<string, string> paramDic, string where) {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update {0} set", tableName);
            StringBuilder setstr = new StringBuilder();
            foreach (KeyValuePair<string, string> item in paramDic) {
                setstr.AppendFormat("{0} = '{1}',", item.Key, item.Value);
            }
            sql.Append(setstr.ToString().Trim(','));
            if (!CheckData.IsStringNull(where)) {
                sql.AppendFormat("where {0}", where);
            }
            return sql.ToString();
        }
        #endregion

        #region ====== Parser SQL WhereModel ======
        /// <summary>
        /// 条件模型解析
        /// </summary>
        /// <typeparam name="M">解析的'表'模型类型</typeparam>
        /// <param name="whereMSour">条件模型源</param>
        /// <param name="modelParser">模型解析器</param>
        /// <returns></returns>
        public static string ParserWhereModel<M>(WhereModel whereMSour, ColumnModelParser<M> modelParser) where M : AbsModelNull {
            if (CheckData.IsObjectNull(whereMSour) || CheckData.IsObjectNull(modelParser)) {
                return string.Empty;
            }
            List<string> resultArray = new List<string>();
            string[] fielVals = ParserFieldValueModel<M>(whereMSour.FielVals, modelParser, isAllowFieldRepeat: whereMSour.IsAllowFieldRepeat);
            resultArray.AddRange(fielVals);
            foreach (WhereModel wm in whereMSour.Wheres) {
                string son_where_str = ParserWhereModel(wm, modelParser);
                if (!CheckData.IsStringNull(son_where_str)) {
                    resultArray.Add(WhereParenthesesPackage(son_where_str));
                }
            }
            return ConvertTool.IListToString(resultArray, DataChar.MSQLServer_LogicChar_Parser(whereMSour.KeyChar));
        }
        /// <summary>
        /// 解析字段值模型列表
        /// </summary>
        /// <typeparam name="M">解析的'表'模型类型</typeparam>
        /// <param name="fielvals">字段值模型来源</param>
        /// <param name="modelParser">模型解析器</param>
        /// <param name="isFixedOperChar">是否固定操作符, 默认不固定</param>
        /// <param name="fixedOperChar">固定的操作符</param>
        /// <param name="isAllowFieldRepeat">是否允许字段重复, 默认允许</param>
        /// <returns></returns>
        public static string[] ParserFieldValueModel<M>(FieldValueModel[] fielvals, ColumnModelParser<M> modelParser,
            bool isFixedOperChar = false, DataChar.OperChar fixedOperChar = DataChar.OperChar.EQUAL,
            bool isAllowFieldRepeat = WhereModel.DEFAULT_ISALLOWFIELDREPEAT) where M : AbsModelNull {
            if (CheckData.IsSizeEmpty(fielvals) || CheckData.IsObjectNull(modelParser)) {
                return new string[] { };
            }
            List<string> Contains_names = new List<string>();
            List<string> sql_expressions = new List<string>();
            foreach (FieldValueModel FVm in fielvals) {
                ColumnItemModel columnItem = IsColumnField(FVm, modelParser.ColumnInfoArray);
                if (CheckData.IsObjectNull(columnItem) || (Contains_names.Contains(FVm.Name) && !isAllowFieldRepeat)) {
                    continue;
                }
                Contains_names.Add(FVm.Name);
                KeyValueModel kvM = modelParser.GetModelValue(columnItem, FVm.Value);
                if (CheckData.IsObjectNull(kvM)) {
                    continue;
                }
                FVm.Value = kvM.Value;
                FVm.SetKeyChar(isFixedOperChar ? fixedOperChar : FVm.KeyChar);
                sql_expressions.Add(DataChar.MSQLServer_OperChar_Parser(FVm));
            }
            return sql_expressions.ToArray();
        }
        private static ColumnItemModel IsColumnField(FieldValueModel FVm, ColumnItemModel[] columnItemModel) {
            if (CheckData.IsObjectNull(FVm) || CheckData.IsSizeEmpty(columnItemModel)) {
                return null;
            }
            foreach (ColumnItemModel item in columnItemModel) {
                if (FVm.Name == item.Property.Name) {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 字段排序模型解析
        /// </summary>
        public static string ParserFieldOrderModel(FieldOrderModel[] fieldOrders) {
            if (CheckData.IsSizeEmpty(fieldOrders)) {
                return string.Empty;
            }
            List<string> existed_names = new List<string>();
            string[] item_strs = ConvertTool.ListConvertType(fieldOrders, FOm => {
                if (existed_names.Contains(FOm.Name)) {
                    return string.Empty;
                } else {
                    existed_names.Add(FOm.Name);
                }
                return string.Format("{0} {1}", FOm.Name, FOm.IsAsc ? ORDERBY_ASC : ORDERBY_DESC);
            }, string.Empty);
            return ConvertTool.IListToString(item_strs, ',');
        }
        #endregion

        #region ====== Basic SQL Grammar ======
        public static string Insert(string table_name, string[] column_names, string[] column_values, bool isResultID = false) {
            table_name = ConvertTool.StrToStrTrim(table_name);
            if (CheckData.IsStringNull(table_name) || CheckData.IsSizeEmpty(column_names) ||
                CheckData.IsSizeEmpty(column_values) || column_names.Length != column_values.Length) {
                return string.Empty;
            }
            string fieldStr = ConvertTool.IListToString(column_names, ',');
            string valueStr = ConvertTool.IListToString(column_values, ',');
            string resultIDval = isResultID ? SELECT_IDENTITY : string.Empty;
            return string.Format("insert into {0}({1}) values({2}) {3}", table_name, fieldStr, valueStr, resultIDval).Trim();
        }
        public static string Delete(string table_name, string where) {
            table_name = ConvertTool.StrToStrTrim(table_name);
            where = ConvertTool.StrToStrTrim(where);
            if (CheckData.IsStringNull(table_name)) {
                return string.Empty;
            }
            where = !CheckData.IsStringNull(where) ? string.Format(" where {0} ", where) : string.Empty;
            return string.Format("delete {0} {1}", table_name, where).Trim();
        }
        public static string Update(string table_name, string setcontent, string where) {
            table_name = ConvertTool.StrToStrTrim(table_name);
            setcontent = ConvertTool.StrToStrTrim(setcontent);
            where = ConvertTool.StrToStrTrim(where);
            if (CheckData.IsStringNull(table_name) || CheckData.IsStringNull(setcontent)) {
                return string.Empty;
            }
            where = !CheckData.IsStringNull(where) ? string.Format(" where {0} ", where) : string.Empty;
            return string.Format("update {0} set {1} {2}", table_name, setcontent, where).Trim();
        }
        public static string Select(string table_name, int top, string where, string order) {
            table_name = ConvertTool.StrToStrTrim(table_name);
            where = ConvertTool.StrToStrTrim(where);
            order = ConvertTool.StrToStrTrim(order);
            if (CheckData.IsStringNull(table_name)) {
                return string.Empty;
            }
            string showcolumn = top <= 0 ? @"*" : string.Format("top {0} *", top);
            where = !CheckData.IsStringNull(where) ? string.Format(" where {0} ", where) : string.Empty;
            order = CheckData.IsStringNull(order) ? string.Empty : string.Format(" order by {0} ", order);
            return string.Format("select {1} from {0} {2} {3}", table_name, showcolumn, where, order).Trim();
        }
        public static string Select_Count(string table_name, string where) {
            if (CheckData.IsStringNull(table_name)) {
                return string.Empty;
            }
            where = !CheckData.IsStringNull(where) ? string.Format(" where {0} ", where) : string.Empty;
            return string.Format("select count(*) as H from {0} {1}", table_name, where);
        }
        #endregion

        #region ====== Tools Method ======
        /// <summary>
        /// 创建 SQL if 语句
        /// </summary>
        /// <param name="whereExpression">条件表达式, 必填</param>
        /// <param name="trueCode">true 代码执行体, 必填</param>
        /// <param name="falseCode">false 代码执行体, 选填</param>
        /// <returns></returns>
        public static string If(string whereExpression, string trueCode, string falseCode = @"") {
            if (CheckData.IsStringNull(whereExpression.Trim()) || CheckData.IsStringNull(trueCode.Trim()))
                return string.Empty;
            string sql = string.Format("if {0} begin {1} end", whereExpression, trueCode);
            if (!CheckData.IsStringNull(falseCode.Trim())) {
                sql += string.Format(" else begin {0} end", falseCode);
            }
            return sql;
        }
        /// <summary>
        /// 检测条件是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string NotExists(string where) {
            return string.Format("not exists({0})", where);
        }
        /// <summary>
        /// 清空数据表的数据, 但保留列信息
        /// </summary>
        /// <param name="table_name">表名</param>
        public static string TruncateTable(string table_name) {
            return string.Format("truncate table {0}", table_name);
        }
        /// <summary>
        /// 清空数据表的所有, 包括数据表和列信息
        /// </summary>
        /// <param name="table_name">表名信息</param>
        /// <returns></returns>
        public static string DropTable(string table_name) {
            return string.Format("drop table {0}", table_name);
        }
        /// <summary>
        /// 在SQL Server系统中查询表名信息 返回一条数据, 并只返回 object_id 信息
        /// </summary>
        /// <returns>返回一条数据, 并只返回 object_id 信息</returns>
        public static string MSSSysTable(string table_name) {
            return string.Format("select top 1 object_id from sys.tables where name = '{0}'", table_name);
        }
        /// <summary>
        /// 在SQL Server系统中查询列名信息
        /// </summary>
        /// <param name="table_name">列名所属的表名</param>
        /// <param name="column_name">列名</param>
        /// <returns>返回多条数据</returns>
        public static string MSSSysColumns(string table_name, string column_name) {
            return string.Format("select * from sys.columns where name = '{0}' and object_id = ({1})", column_name, MSSSysTable(table_name));
        }
        /// <summary>
        /// 创建一个表
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="column_formats">所有的列格式信息</param>
        public static string CreateTable(string table_name, string[] column_formats) {
            string columnFormats = ConvertTool.IListToString(column_formats, ',');
            return string.Format("CREATE TABLE {0} ({1})", table_name, columnFormats);
        }
        /// <summary>
        /// 对数据表添加列
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="column_format">列的相关信息</param>
        public static string AlterColumn(string table_name, string column_format) {
            return string.Format("ALTER TABLE {0} ADD {1}", table_name, column_format);
        }
        #endregion

        #region ====== Word Text  ======
        /// <summary>
        /// 替换掉特殊字符
        /// </summary>
        public static string ReplaceSpecialCharacters(string source) {
            return source.Replace("'", "''");
        }
        /// <summary>
        /// 还原回特殊字符
        /// </summary>
        public static string RevertSpecialCharacters(string source) {
            return source;
        }
        #endregion
    }
}

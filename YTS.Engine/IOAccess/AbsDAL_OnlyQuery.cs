using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 抽象-数据访问层(Data Access Layer)-只提供查询功能
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="W">查询条件</typeparam>
    /// <typeparam name="P">解析器</typeparam>
    /// <typeparam name="PI">解析信息数据模型</typeparam>
    public abstract class AbsDAL_OnlyQuery<M, W, P, PI> : IDAL_OnlyQuery<M, W, P, PI>
        where M : AbsShineUpon
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        /// <summary>
        /// 初始化自动生成默认数据映射模型
        /// </summary>
        public M DefaultModel {
            get {
                if (CheckData.IsObjectNull(_default_model)) {
                    _default_model = InitCreateModel();
                }
                return _default_model;
            }
        }
        private M _default_model = null;

        /// <summary>
        /// 数据映射模型解析器
        /// </summary>
        public P Parser {
            get {
                if (CheckData.IsObjectNull(_parser)) {
                    _parser = InitCreateParser();
                }
                return _parser;
            }
        }
        private P _parser = null;

        public AbsDAL_OnlyQuery() { }

        /// <summary>
        /// 初始化创建 默认数据模型Model 对象
        /// </summary>
        public virtual M InitCreateModel() {
            return ReflexHelp.CreateNewObject<M>();
        }

        /// <summary>
        /// 初始化创建 解析器Parser 对象
        /// </summary>
        public virtual P InitCreateParser() {
            return ReflexHelp.CreateNewObject<P>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="top">查询记录数目</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public abstract M[] Select(int top, W where, KeyBoolean[] sorts);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pageCount">每页展现记录数</param>
        /// <param name="pageIndex">浏览页面索引</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public abstract M[] Select(int pageCount, int pageIndex, out int recordCount, W where, KeyBoolean[] sorts);

        /// <summary>
        /// 获取记录数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        public abstract int GetRecordCount(W where);

        /// <summary>
        /// 获取单个记录模型
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">数据映射模型集合结果</param>
        /// <returns>单个记录数据映射模型</returns>
        public virtual M GetModel(W where, KeyBoolean[] sorts) {
            M[] list = Select(1, where, sorts);
            return (CheckData.IsSizeEmpty(list)) ? null : list[0];
        }

        /// <summary>
        /// 数据映射模型值 - 转 - 数据库可用类型值
        /// </summary>
        /// <param name="model_value">数据映射模型值</param>
        /// <returns>数据库可用类型值</returns>
        public string ModelValueToDataBaseValue(object model_value) {
            if (CheckData.IsTypeEqual<DateTime>(model_value, true)) {
                return ((DateTime)model_value).ToString(Tools.Const.Format.DATETIME_MILLISECOND);
            }
            if (CheckData.IsTypeEqual<Enum>(model_value, true)) {
                return ((int)model_value).ToString();
            }
            return ConvertTool.ObjToString(model_value);
        }

        public object DataBaseValueToModelValue(PI parser_info, string field_value) {
            Type detype = parser_info.Property.PropertyType;
            if (CheckData.IsTypeEqual<int>(detype) || CheckData.IsTypeEqual<Enum>(detype, true)) {
                return ConvertTool.ObjToInt(field_value, default(int));
            }
            if (CheckData.IsTypeEqual<float>(detype) || CheckData.IsTypeEqual<double>(detype)) {
                return ConvertTool.ObjToFloat(field_value, default(float));
            }
            if (CheckData.IsTypeEqual<DateTime>(detype)) {
                return ConvertTool.ObjToDateTime(field_value, default(DateTime));
            }
            if (CheckData.IsTypeEqual<DateTime>(detype)) {
                return ConvertTool.ObjToBool(field_value, default(bool));
            }
            return field_value;
        }
    }
}

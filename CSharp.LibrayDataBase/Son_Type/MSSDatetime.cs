using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 datetime 类型
    /// </summary>
    public class MSSDatetime : AbsFieldTypeDefault
    {
        /// <summary>
        /// 默认值 属性器
        /// </summary>
        protected SqlDateTime DefalutValue {
            get { return _defalutValue; }
            set { _defalutValue = value; }
        }
        private SqlDateTime _defalutValue = new SqlDateTime(DateTime.Now);

        /// <summary>
        /// 实例化时间数据类型
        /// </summary>
        /// <param name="defvalue">默认提供一个时间, 出错时使用</param>
        public MSSDatetime(SqlDateTime defvalue)
            : base(defvalue) {
            DefalutValue = defvalue;
        }

        public override string FieldTypeName() {
            return @"datetime";
        }

        public override string PrintSaveValue(object programValue) {
            return ConvertTool.ObjToSqlDateTime(programValue, DefalutValue).Value.ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND);
        }
    }
}

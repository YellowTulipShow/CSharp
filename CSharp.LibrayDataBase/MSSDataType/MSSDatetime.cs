using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 datetime 类型
    /// </summary>
    public class MSSDateTime : AbsDBType
    {
        public override string TypeName() {
            return @"datetime";
        }

        private readonly SqlDateTime _defalutValue = new SqlDateTime(DateTime.Now);
        public override object InputConvert(object sourceValue, ColumnItemModel colmodel) {
            SqlDateTime result = _defalutValue;
            result = ConvertTool.ObjToSqlDateTime(sourceValue, _defalutValue);
            if (result == _defalutValue) {
                return GetDefaultValueString();
            }
            return result.Value.ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND);
        }
    }
}

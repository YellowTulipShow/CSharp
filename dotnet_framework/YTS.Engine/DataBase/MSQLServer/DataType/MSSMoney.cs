using System;
using YTS.Tools;

namespace YTS.Engine.DataBase.MSQLServer.DataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 money 类型
    /// </summary>
    public class MSSMoney : AbsDBType
    {
        public override string TypeName() {
            return @"money";
        }

        public override object InputConvert(object sourceValue, ColumnItemModel colmodel) {
            if (CheckData.IsObjectNull(sourceValue) || !CheckData.IsDouble(sourceValue)) {
                return GetDefaultValueString();
            }
            return sourceValue.ToString();
        }
    }
}

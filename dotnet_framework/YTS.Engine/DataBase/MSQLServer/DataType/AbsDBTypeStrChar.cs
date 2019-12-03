using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Model.Table;
using YTS.Tools;

namespace YTS.Engine.DataBase.MSQLServer.DataType
{
    /// <summary>
    /// 抽象-数据系统 数据类型映射 字符(串)类型
    /// </summary>
    public abstract class AbsDBTypeStrChar : AbsDBType
    {
        public override object OutputConvert(object sourceValue, ColumnItemModel colmodel) {
            sourceValue = base.OutputConvert(sourceValue, colmodel);
            string result = ConvertTool.ObjToString(sourceValue);
            result = CreateSQL.RevertSpecialCharacters(result);
            return result;
        }
        public override object InputConvert(object sourceValue, ColumnItemModel colmodel) {
            string result = ConvertTool.ObjToString(sourceValue);
            result = CreateSQL.ReplaceSpecialCharacters(result);
            return base.CharStringTypeConvert(result);
        }
    }
}

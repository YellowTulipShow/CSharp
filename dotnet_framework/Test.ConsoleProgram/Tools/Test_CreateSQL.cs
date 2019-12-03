using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.LibrayDataBase;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_CreateSQL : CaseModel
    {
        public Test_CreateSQL() {
            this.NameSign = @"测试创建 SQL 语句静态类";
            this.SonCases = new CaseModel[] {
                Select_column(),
            };
        }

        public CaseModel Select_column() {
            return new CaseModel() {
                NameSign = @"指定列的查询语句",
                ExeEvent = () => {
                    string column = ConvertTool.IListToString(new string[] {
                        @"id",
                        @"TypeKey",
                        @"PositionDescription",
                        @"LoadAfterTimeLengthMS",
                        @"TriggerTimeLengthMS",
                        @"IsObsolete",
                        @"TimeAdd",
                        @"Remark",
                    }, CreateSQL.COLUMN_INTERVALSYMBOL);

                },
            };
        }
    }
}

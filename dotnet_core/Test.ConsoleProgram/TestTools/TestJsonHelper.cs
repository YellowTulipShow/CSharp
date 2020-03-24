using System;
using System.Collections.Generic;
using YTS.Tools;

namespace Test.ConsoleProgram.TestTools
{
    public class TestJsonHelper : CaseModel
    {
        public TestJsonHelper()
        {
            this.NameSign = "测试JSON解析";
            this.SonCases = new List<CaseModel>()
            {
            };
        }

        public class TestToString : CaseModel
        {
            public TestToString()
            {
                this.NameSign = "计算最大值";
                this.ExeEvent = () =>
                {
                    Func<decimal, decimal, decimal> calcF = (v1, v2) => v1 > v2 ? v1 : v2;
                    List<bool> results = new List<bool>()
                    {
                        222M == ConvertTool.ToMaxValue(calcF, 12.3M, 222M, 41M, 33M, 22M),
                        4M == ConvertTool.ToMaxValue(calcF, 1M, 4M, 2M, 3M, 2M),
                        1M == ConvertTool.ToMaxValue(calcF, 1M),
                    };
                    return !results.Contains(false);
                };
            }
        }

        public class TestToObject : CaseModel
        {
            public TestToObject()
            {
                this.NameSign = "转为:数值类型:Decimal";
                this.ExeEvent = () =>
                {
                    List<bool> results = new List<bool>()
                    {
                        35.1M == ConvertTool.ToDecimal("35.1", 99),
                        99M == ConvertTool.ToDecimal("eee", 99),
                        99.2M == ConvertTool.ToDecimal(DateTime.Now, 99.2M),
                        45.2M == ConvertTool.ToDecimal(45.2M, 99.2M),
                    };
                    return !results.Contains(false);
                };
            }
        }

        public class TestToAnonymousType : CaseModel
        {
            public TestToAnonymousType()
            {
                this.NameSign = "转为:数值类型:Decimal";
                this.ExeEvent = () =>
                {
                    List<bool> results = new List<bool>()
                    {
                        35.1M == ConvertTool.ToDecimal("35.1", 99),
                        99M == ConvertTool.ToDecimal("eee", 99),
                        99.2M == ConvertTool.ToDecimal(DateTime.Now, 99.2M),
                        45.2M == ConvertTool.ToDecimal(45.2M, 99.2M),
                    };
                    return !results.Contains(false);
                };
            }
        }
    }
}
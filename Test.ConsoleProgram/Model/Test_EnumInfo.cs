using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Model;
using YTS.Model.Attribute;
using YTS.Tools;

namespace Test.ConsoleProgram.Model
{
    public class Test_EnumInfo : CaseModel
    {
        public Test_EnumInfo() {
            this.NameSign = @"静态枚举信息类";
            this.SonCases = new CaseModel[] {
                Func_AnalysisList(),
                Func_AnalysisItem(),
            };
        }

        public enum TestEnum
        {
            /// <summary>
            /// 保密
            /// </summary>
            [Explain(@"保密")]
            Secrecy,
            /// <summary>
            /// 男
            /// </summary>
            Male = 1,
            /// <summary>
            /// 女
            /// </summary>
            [Explain(@"女")]
            Female = 81,
            Test,
        }

        public CaseModel Func_AnalysisList() {
            return new CaseModel() {
                NameSign = @"解析枚举的所有选项",
                ExeEvent = () => {
                    EnumInfo[] answer = new EnumInfo[] {
                        new EnumInfo() { Name = @"Secrecy", IntValue = 0, Explain = @"保密" },
                        new EnumInfo() { Name = @"Male", IntValue = 1, Explain = ExplainAttribute.ERROR_EXPLAIN_TEXT },
                        new EnumInfo() { Name = @"Female", IntValue = 81, Explain = @"女" },
                        new EnumInfo() { Name = @"Test", IntValue = 82, Explain = ExplainAttribute.ERROR_EXPLAIN_TEXT },
                    };

                    EnumInfo[] enums = EnumInfo.AnalysisList<TestEnum>();
                    foreach (EnumInfo ci in enums) {
                        bool is_did_find = false;
                        foreach (EnumInfo ai in answer) {
                            if (ci.Name.Equals(ai.Name)) {
                                is_did_find = true;
                                if (!ci.IntValue.Equals(ai.IntValue)) {
                                    return false;
                                }
                                if (!ci.Explain.Equals(ai.Explain)) {
                                    return false;
                                }
                            }
                        }
                        if (!is_did_find) {
                            return false;
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_AnalysisItem() {
            return new CaseModel() {
                NameSign = @"解析枚举的单个选项",
                ExeEvent = () => {
                    Func<TestEnum, string, int, string, bool> method = (enumItem, name, vint, explain) => {
                        EnumInfo info = EnumInfo.AnalysisItem<TestEnum>(enumItem);
                        if (CheckData.IsObjectNull(info)) {
                            return false;
                        }
                        if (!info.Name.Equals(name)) {
                            return false;
                        }
                        if (!info.IntValue.Equals(vint)) {
                            return false;
                        }
                        if (!info.Explain.Equals(explain)) {
                            return false;
                        }
                        return true;
                    };

                    if (!method(TestEnum.Secrecy, @"Secrecy", 0, @"保密")) {
                        return false;
                    }
                    if (!method(TestEnum.Male, @"Male", 1, ExplainAttribute.ERROR_EXPLAIN_TEXT)) {
                        return false;
                    }
                    if (!method(TestEnum.Female, @"Female", 81, @"女")) {
                        return false;
                    }
                    if (!method(TestEnum.Test, @"Test", 82, ExplainAttribute.ERROR_EXPLAIN_TEXT)) {
                        return false;
                    }
                    return true;
                },
            };
        }
    }
}

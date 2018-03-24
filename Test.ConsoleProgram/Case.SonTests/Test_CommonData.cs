using System;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_CommonData : CaseModel
    {
        public Test_CommonData() {
            this.NameSign = @"常用工具的测试";
            this.ExeEvent = () => { };
            this.SonCases = new CaseModel[] {
                //ExeEvent_Section_ASCII_String(),
                ExeEvent_RandomStrignMethod(),
                ExeEvent_Random_DateTime(),
                ExeEvent_Random_Select_Item(),
            };
        }

        #region === Random String ===
        private CaseModel ExeEvent_RandomStrignMethod() {
            return new CaseModel() {
                NameSign = @"随机字符串",
                ExeEvent = () => {
                    Print.WriteLine(@"(" + CommonData.Random_String(100) + @")");
                    Print.WriteLine(@"(" + CommonData.Random_String(100) + @")");
                    Print.WriteLine(@"(" + CommonData.Random_String(100) + @")");
                    Print.WriteLine(@"(" + CommonData.Random_String(CommonData.ASCII_Number()) + @")");
                    Print.WriteLine(@"(" + CommonData.Random_String(CommonData.ASCII_Number()) + @")");
                    Print.WriteLine(@"(" + CommonData.Random_String(CommonData.ASCII_Number()) + @")");
                    Print.WriteLine(@"(" + CommonData.Random_String(CommonData.ASCII_Special(), 80) + @")");
                    Print.WriteLine(@"(" + CommonData.Random_String(CommonData.ASCII_Special(), 80) + @")");
                    Print.WriteLine(@"(" + CommonData.Random_String(CommonData.ASCII_Special(), 80) + @")");
                },
            };
        }
        #endregion

        #region === Random ASCII Code ===
        private CaseModel ExeEvent_Section_ASCII_String() {
            return new CaseModel() {
                NameSign = @"部分的 ASCII 码字符",
                ExeEvent = () => {
                    PrintMethod(@"所有常用字符:", CommonData.ASCII_ALL());
                    PrintMethod(@"常用文本字符", CommonData.ASCII_WordText());
                    PrintMethod(@"特殊字符:", CommonData.ASCII_Special());
                    PrintMethod(@"阿拉伯数字:", CommonData.ASCII_Number());
                    PrintMethod(@"小写英文:", CommonData.ASCII_LowerEnglish());
                    PrintMethod(@"大写英文:", CommonData.ASCII_UpperEnglish());
                },
            };
        }
        private void PrintMethod(string name, char[] sour) {
            Print.WriteLine(name);
            Print.WriteLine(ConvertTool.IListToString(sour, string.Empty) + @" 个数: " + sour.Length.ToString());
        }
        #endregion

        #region === Random DateTime ===
        private CaseModel ExeEvent_Random_DateTime() {
            return new CaseModel() {
                NameSign = @"随机时间",
                ExeEvent = () => {
                    for (int i = 0; i < 5; i++) {
                        Print.WriteLine(CommonData.Random_DateTime().ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND));
                    }
                },
            };
        }
        #endregion

        #region === Random Select Item ===
        private CaseModel ExeEvent_Random_Select_Item() {
            return new CaseModel() {
                NameSign = @"随机选项",
                ExeEvent = () => {
                    TestEnum[] source = ConvertTool.EnumForeachArray<TestEnum>();
                    foreach (TestEnum item in source) {
                        Print.WriteLine(string.Format("Name: {0}  Value: {1}", item.GetName(), item.GetIntValue()));
                    }

                    Print.WriteLine("随机筛选的值: ");
                    TestEnum random_selitem = CommonData.Random_Item(source);
                    Print.WriteLine(string.Format("Name: {0}  Value: {1}", random_selitem.GetName(), random_selitem.GetIntValue()));
                },
            };
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram.Case.Learn
{
    public class Learn_CalcRule : CaseModel
    {
        public Learn_CalcRule() {
            this.NameSign = @"计算规则";
            this.ExeEvent = () => { };
            this.SonCases = new CaseModel[] {
                Int_Division(),
            };
        }
        protected CaseModel Int_Division() {
            return new CaseModel() {
                NameSign = @"Int 类型 除法 四舍五入",
                ExeEvent = () => {
                    for (int i = 1; i <= 7; i++) {
                        for (int y = 1; y <= 7; y++) {
                            int value = i / y;
                            Print.WriteLine("{0} / {1} = {2}", i, y, value);
                        }
                    }
                },
            };
        }
    }
}

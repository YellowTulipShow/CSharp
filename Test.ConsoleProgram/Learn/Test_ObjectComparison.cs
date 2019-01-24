using System;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_ObjectComparison : CaseModel
    {
        public Test_ObjectComparison() {
            this.NameSign = @"对象比较";
            this.SonCases = new CaseModel[] {
                Func_ObjectDouble(),
            };
        }

        public CaseModel Func_ObjectDouble() {
            return new CaseModel() {
                NameSign = @"双精度浮点(Double)",
                ExeEvent = () => {
                    object s = 10.0;
                    //object r = 10.0;
                    object r = ConvertTool.ToDouble(10, 666.666);
                    Console.WriteLine("s:{0}  r:{1}  s != r : {2}", s, r, s != r);
                    Console.WriteLine("s:{0}  r:{1}  s.Equals(r) : {2}", s, r, s.Equals(r));
                    return s != r;
                },
            };
        }
    }
}

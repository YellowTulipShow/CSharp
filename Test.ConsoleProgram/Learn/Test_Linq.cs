using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram.Learn
{
    public class Test_Linq : CaseModel
    {
        public Test_Linq() {
            NameSign = @"学习Linq";
            SonCases = new CaseModel[] {
                Func_ListArray(),
            };
        }

        public CaseModel Func_ListArray() {
            return new CaseModel() {
                NameSign = @"操作数列集合",
                ExeEvent = () => {
                    IEnumerable<int> es = from i in new int[] { 525, 415, 63, 17, 95, 5, } where i > 100 select i;
                    int[] answer = new int[] { 525, 415 };
                    if (!IsIEnumerableEqual(answer, es)) {
                        return false;
                    }
                    /*
                    Console.WriteLine("b: {0}", YTS.Tools.JSON.SerializeObject(b));
                    result:
                        [525, 415]
                    */

                    /*
                    foreach (int item in b) {
                        Console.WriteLine("item: {0}", item);
                    }
                    result:
                        item: 525
                        item: 415
                    */
                    return true;
                },
            };
        }
    }
}

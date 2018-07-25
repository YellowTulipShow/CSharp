using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.Learn
{
    public class Learn_Algorithm : CaseModel
    {
        public Learn_Algorithm() {
            this.NameSign = @"算法学习构建";
            this.SonCases = new CaseModel[] {
                PyTest_36(),
            };
        }

        public int[] CreateIntList(int min, int max) {
            List<int> list = new List<int>();
            for (int i = min; i < max; i++) {
                list.Add(i);
            }
            return list.ToArray();
        }

        public CaseModel PyTest_36() {
            return new CaseModel() {
                NameSign = @"有n个整数，使其前面各数顺序向后移m个位置，最后m个数变成最前面的m个数",
                ExeEvent = () => {
                    int m = 3;
                    Print.WriteLine("m: {0}", m);
                    int[] slist = CreateIntList(0, 10);
                    Print.WriteLine("slist: {0}", JsonHelper.SerializeObject(slist));
                    int[] rlist = new int[slist.Length];
                    for (int i = 0; i < slist.Length; i++) {
                        int si = i + m;
                        if (si >= slist.Length) {
                            si = si - slist.Length;
                        }
                        rlist[si] = slist[i];
                    }
                    Print.WriteLine("rlist: {0}", JsonHelper.SerializeObject(rlist));

                    //Print.WriteLine(JsonHelper.SerializeObject(CommonData.ASCII_LowerEnglish()));
                    char[] le = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                },
            };
        }
    }
}

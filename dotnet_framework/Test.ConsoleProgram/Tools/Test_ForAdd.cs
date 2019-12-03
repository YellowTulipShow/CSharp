using System;
using System.Collections;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_ForAdd : CaseModel
    {
        public Test_ForAdd() {
            base.NameSign = @"算法: 循环添加";
            base.ExeEvent = Method;
        }
        public void Method() {
            List<string[]> list = new List<string[]>();
            list.Add(List_one());
            list.Add(List_two());
            //list.Add(List_three());
            //list.Add(List_four());

            List<string> re = CalcContent(list);
            foreach (string item in re) {
                Print.WriteLine(item);
            }
        }
        private string[] List_one() {
            return new string[] {
                @"11111111111111",
            };
        }
        private string[] List_two() {
            return new string[] {
                @"222222222222",
                @"55555555",
            };
        }
        private string[] List_three() {
            return new string[] {
                @"33333333333",
                @"666666666666",
                @"88888888",
            };
        }
        private string[] List_four() {
            return new string[] {
                @"44444444444444",
                @"777777777777",
                @"9999999999",
                @"qqqqqqqqq",
            };
        }

        public List<string> CalcContent(List<string[]> SUMLIST) {
            int exe_count = 0;

            const int size = 6;
            List<string> list = new List<string>();

            int lun = 0;
            int max_list_size = 0;
            while (list.Count < size) {
                for (int i = 0; i < SUMLIST.Count; i++) {
                    exe_count++;
                    if (SUMLIST[i].Length > max_list_size) {
                        max_list_size = SUMLIST[i].Length;
                    }
                    if (list.Count >= size) {
                        break;
                    }
                    if (lun >= SUMLIST[i].Length) {
                        continue;
                    }
                    list.Add(SUMLIST[i][lun]);
                }
                lun++;
                if (lun >= max_list_size) {
                    break;
                }
            }
            Print.WriteLine("执行次数: {0}", exe_count);
            return list;
        }

    }
}

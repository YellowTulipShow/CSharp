using System;
using System.Collections;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_Random : CaseModel
    {
        public Test_Random() {
            base.NameSign = @"测试 随机";
            base.ExeEvent = () => { };
            base.SonCases = SonCaseArray();
        }
        public CaseModel[] SonCaseArray() {
            return new CaseModel[] {
                //ListToItem(),
                ListToList(),
            };
        }

        public CaseModel ListToItem() {
            return new CaseModel() {
                NameSign = @"随机从列表中选出一个选项",
                ExeEvent = () => {
                    Func<string, object, bool> ShowResult = (list, obj) => {
                        Print.WriteLine("随机的选项: {0}", obj);
                        return true;
                    };

                    int[] int_list = new int[] { 51, 5, 48, 63, 2, 18, 4, 3, 2, 87, 15, 41, 1, 4, 8, 6, 3 };
                    for (int i = 0; i < int_list.Length; i++) {
                        int int_item = RandomData.GetItem(int_list);
                        ShowResult(int_list.ToJson(), int_item);
                    }

                    string[] str_list = new string[] { "hello", "world", "zrq", "love", "wechatno", "show", "yellowtulip" };
                    for (int i = 0; i < str_list.Length; i++) {
                        string str_item = RandomData.GetItem(str_list);
                        ShowResult(str_list.ToJson(), str_item);
                    }
                },
            };
        }

        public CaseModel ListToList() {
            return new CaseModel() {
                NameSign = "列表中提取指定数量的列表随机获取",
                ExeEvent = () => {
                    string[] list = Assets_DomainList();
                    string[] result = RandomData.GetList(list, 11);
                    foreach (string item in result) {
                        Print.WriteLine(item);
                    }
                },
            };
        }

        private string[] Assets_DomainList() {
            return new string[] {
                "atbd1.chongcaoapp.com",
                "zy2.chongcaobbs.com",
                "at.trsm01.cn",
                "zy.zhiniao22.cn",
                "ztxl1.chongcaoapp.com",
                "zy2.lnfxry.com",
                "anbd1.chongcaoapp.com",
                "zy.fengyu17.cn",
                "at.zhkunbao.net",
                "dcxc.mmhtmy.top",
                "at.dyyh123.top",
                "na.hzkcsmd.cn",
                "mdy.zhiniao33.cn",
                "mdxl1.chongcaobbs.com",
                "at.lnfxry.com",
                "dcat.gdtfn.top",
                "mdy.syhcp.top",
                "at.gdtfn.top",
                "zyzy7.chongcaobbs.com",
                "ana.fengyu88.cn",
                "an.syhcp.top",
                "at.jujiaosm666.top",
                "at.fengyu04.cn",
                "mdy.fengyu97.cn",
                "zy.kaifeifood.com",
                "mdy.ykxyjtnc.cn",
                "anxl3.chongcaoapp.com",
                "zy.lnfxry.com",
            };
        }
    }
}

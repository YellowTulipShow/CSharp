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
                //GetIPv4(),
                //Telephone_ElevenBit(),
                //Sample(),
                RGBColor(),
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
                        int int_item = RandomData.Item(int_list);
                        ShowResult(int_list.ToJson(), int_item);
                    }

                    string[] str_list = new string[] { "hello", "world", "zrq", "love", "wechatno", "show", "yellowtulip" };
                    for (int i = 0; i < str_list.Length; i++) {
                        string str_item = RandomData.Item(str_list);
                        ShowResult(str_list.ToJson(), str_item);
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
        public CaseModel GetIPv4() {
            return new CaseModel() {
                NameSign = @"随机获取IPv4地址",
                ExeEvent = () => {
                    for (int i = 0; i < 20; i++) {
                        Print.WriteLine(RandomData.IPv4());
                    }
                },
            };
        }
        public CaseModel Telephone_ElevenBit() {
            return new CaseModel() {
                NameSign = @"随机获取十一位电话号码",
                ExeEvent = () => {
                    for (int i = 0; i < 20; i++) {
                        Print.WriteLine(RandomData.Telephone_ElevenBit());
                    }
                },
            };
        }
        public CaseModel Sample() {
            return new CaseModel() {
                NameSign = @"随机打乱序列列表的顺序",
                ExeEvent = () => {
                    int[] init_list = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    Print.WriteLine("init_list: {0} {1}", JsonHelper.SerializeObject(init_list), init_list.Length);
                    int[] rlist = RandomData.Sample(init_list);
                    Print.WriteLine("    rlist: {0} {1}", JsonHelper.SerializeObject(rlist), rlist.Length);
                    int need_num = 5;
                    rlist = RandomData.Sample(init_list, need_num);
                    Print.WriteLine("    rlist: {0} {1} {2}", JsonHelper.SerializeObject(rlist), rlist.Length, need_num);
                    need_num = 15;
                    rlist = RandomData.Sample(init_list, need_num);
                    Print.WriteLine("    rlist: {0} {1} {2}", JsonHelper.SerializeObject(rlist), rlist.Length, need_num);
                },
            };
        }
        public CaseModel RGBColor() {
            return new CaseModel() {
                NameSign = @"RGB 颜色随机 数字与字符串",
                ExeEvent = () => {
                    int[] nlist = RandomData.RGBColor_NumberList();
                    Print.WriteLine("数字列表: {0}", JsonHelper.SerializeObject(nlist));

                    string sixstring = RandomData.RGBColor_SixDigitString();
                    Print.WriteLine("六位字符串: {0}", sixstring);
                },
            };
        }
    }
}

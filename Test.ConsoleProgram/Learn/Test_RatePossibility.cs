using System;
using System.Collections.Generic;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_RatePossibility : CaseModel
    {
        public Test_RatePossibility() {
            this.NameSign = @"概率计算";
            this.SonCases = new CaseModel[] {
                Func_CrazyGrabRedPackage(),
            };
        }

        public class Prize : AbsBasicDataModel
        {
            public string Name = @"谢谢参与";
            public int Rate = 0;

            public Prize(string name, int rate) {
                this.Name = name;
                this.Rate = rate;
            }

            public static Prize[] Get_PrizeList() {
                return new Prize[] {
                    new Prize(@"谢谢参与", 2),
                    new Prize(@"六等奖", 6),
                    new Prize(@"5等奖", 3),
                    new Prize(@"4等奖", 3),
                    new Prize(@"3等奖", 3),
                    new Prize(@"2等奖", 2),
                    new Prize(@"1等奖", 1),
                };
            }
        }
        public CaseModel Func_CrazyGrabRedPackage() {
            return new CaseModel() {
                NameSign = @"疯狂抢红包",
                ExeEvent = () => {
                    Func<Prize> CalcMethod = () => {
                        Prize[] list = Prize.Get_PrizeList();
                        for (int i = 0; i < list.Length; i++) {
                            Prize item = list[i];
                            if (RandomData.GetInt(0, 9 + 1) <= item.Rate) {
                                return item;
                            }
                        }
                        return null;
                    };

                    int sum = 100 * 10000;

                    Dictionary<string, int> dic = new Dictionary<string, int>() {
                        { @"空", 0 },
                    };
                    foreach (Prize item in Prize.Get_PrizeList()) {
                        dic.Add(item.Name, 0);
                    }

                    for (int i = 0; i < sum; i++) {
                        Prize prize = CalcMethod();
                        if (CheckData.IsObjectNull(prize)) {
                            dic[@"空"] += 1;
                            continue;
                        }
                        dic[prize.Name] += 1;
                    }

                    Console.WriteLine("测试次数: {0} 次", sum);
                    foreach (KeyValuePair<string, int> kv in dic) {
                        double rate = (double)kv.Value / (double)sum * 100d;
                        Console.WriteLine("{0}: {1}次 比例: {2}%", kv.Key, kv.Value, rate);
                    }
                    return true;
                },
            };
        }
    }
}

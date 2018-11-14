using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_RegularExpression : CaseModel
    {
        public Test_RegularExpression() {
            this.NameSign = @"正则表达式";
            this.SonCases = new CaseModel[] {
                Func_Find(),
                Func_Find_Static(),
                Func_Replace(),
                Func_Replace_Static(),
                Func_Null(),
            };
        }

        public CaseModel Func_Find() {
            return new CaseModel() {
                NameSign = @"查找",
                ExeEvent = () => {
                    string pattern = @"/(\w*)/?(.*)";
                    Regex re = new Regex(pattern);
                    string url = @"/Admin/index.aspx";
                    Match match = re.Match(url);
                    GroupCollection groups = match.Groups;
                    Group first = groups[1];
                    string value = first.Value;
                    return value.ToString().Trim() == @"Admin";
                },
            };
        }
        public CaseModel Func_Find_Static() {
            return new CaseModel() {
                NameSign = @"查找静态",
                ExeEvent = () => {
                    return Regex.Match(@"/Admin/index.aspx", @"/(\w*)/?(.*)").Groups[1].Value == @"Admin";
                },
            };
        }

        public CaseModel Func_Replace() {
            return new CaseModel() {
                NameSign = @"替换",
                ExeEvent = () => {
                    string pattern = @"/(\w*)/?(.*)";
                    Regex re = new Regex(pattern);
                    string url = @"/Admin/index.aspx";
                    string result = re.Replace(url, @"$1-$2");
                    return result == @"Admin-index.aspx";
                },
            };
        }
        public CaseModel Func_Replace_Static() {
            return new CaseModel() {
                NameSign = @"替换静态",
                ExeEvent = () => {
                    return Regex.Replace(@"/Admin/index.aspx", @"/(\w*)/?(.*)", @"$1-$2") == @"Admin-index.aspx";
                },
            };
        }

        public CaseModel Func_Null() {
            return new CaseModel() {
                NameSign = @"匹配结果空值",
                ExeEvent = () => {
                    Regex re = new Regex(@"/TS-(\w*)/?(.*)", RegexOptions.IgnoreCase);

                    Match nullval = re.Match(@"/TSMain/index.html");
                    if (CheckData.IsObjectNull(nullval)) {
                        Console.WriteLine("不匹配返回为空");
                        return false;
                    }
                    if (nullval == Match.Empty) {
                        Console.WriteLine("不匹配返回为正规空值空组");
                    }

                    Match normal = re.Match(@"/TS-Main/index.html");
                    return normal.Groups[1].Value == @"Main";
                },
            };
        }
    }
}

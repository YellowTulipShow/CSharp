using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
    }
}

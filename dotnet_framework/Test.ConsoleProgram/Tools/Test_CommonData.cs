using System;
using System.Collections.Generic;
using System.Text;
using YTS.Tools;

namespace Test.ConsoleProgram.Tools
{
    public class Test_CommonData : CaseModel
    {
        public Test_CommonData() {
            this.NameSign = @"常用字符";
            this.SonCases = new CaseModel[] {
                Func_ForeachPrintALLChars(),
                Func_ASCII_Special(),
            };
        }

        public CaseModel Func_ForeachPrintALLChars() {
            return new CaseModel() {
                NameSign = @"遍历输出所有字符",
                ExeEvent = () => {
                    StringBuilder str = new StringBuilder();
                    foreach (char c in CommonData.ASCII_ALL()) {
                        str.Append(c);
                    }
                    Console.WriteLine(str.ToString());
                    return true;
                },
            };
        }

        public CaseModel Func_ASCII_Special() {
            return new CaseModel() {
                NameSign = @"特别字符",
                ExeEvent = () => {
                    StringBuilder str = new StringBuilder();
                    foreach (char c in CommonData.ASCII_Special()) {
                        str.AppendFormat("{{ @\"{0}\", @\"{1}\" }},\n", c, RandomData.GetString(CommonData.ASCII_UpperEnglish(), 5));
                    }
                    string path = PathHelp.CreateUseFilePath(@"/auto/Tools/Test_CommonData", @"Func_ASCII_Special.txt");
                    this.ClearAndWriteFile(path, str.ToString());

                    Dictionary<string, string> dic = FileNameFormat_Dictionary();
                    return true;
                },
            };
        }

        public Dictionary<string, string> FileNameFormat_Dictionary() {
            return new Dictionary<string, string>() {
                { @"!", @"FVALW" },
                { @"#", @"CVURU" },
                { @"$", @"XXUYF" },
                { @"%", @"VTGOF" },
                { @"&", @"GRSPM" },
                { @"'", @"ETJWD" },
                { @"(", @"HSONF" },
                { @")", @"GXYKB" },
                { @"*", @"XTQRH" },
                { @"+", @"UADSL" },
                { @",", @"MPLFQ" },
                { @"-", @"ANHZK" },
                { @"/", @"EJGTY" },
                { @":", @"XUVNI" },
                { @";", @"MAAIR" },
                { @"<", @"EQRLR" },
                { @"=", @"RGIXF" },
                { @">", @"SQZAS" },
                { @"?", @"LWCDC" },
                { @"@", @"LUJTN" },
                { @"[", @"RUYXE" },
                { @"]", @"SUMXV" },
                { @"^", @"YAJOG" },
                { @"_", @"HVVVW" },
                { @"`", @"QYWFF" },
                { @"{", @"JGYXZ" },
                { @"|", @"XILTF" },
                { @"}", @"FRRTA" },
                { @"~", @"RJFID" },
            };
        }
    }
}

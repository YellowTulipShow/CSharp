using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using YTS.Engine.IOAccess;
using YTS.Tools;
using YTS.Tools.Model;

namespace Test.ConsoleProgram.Engine
{
    public class Test_ini : CaseModel
    {
        public char[] chars = null;
        public IniFile ini = null;
        public Dictionary<string, KeyString[]> dic = null;

        public Test_ini() {
            this.chars = new char[] { };
            this.chars = CommonData.ASCII_WordText();

            string fp = this.GetFilePath();
            if (File.Exists(fp)) {
                File.Delete(fp);
            }
            this.ini = new IniFile(fp);
            this.dic = this.GetTestCase();

            NameSign = @"ini配置文件";
            SonCases = new CaseModel[] {
                Func_WriteString(),
                Func_ReadString(),
                Func_GetKeys(),
                Func_GetSections(),
                Func_GetKeyValues(),
                Func_ClearSection(),
                Func_DeleteKey(),
                Func_IsExistsValue(),
            };
        }

        public string GetFilePath() {
            return PathHelp.CreateUseFilePath("/auto/ini", "first.ini");
        }

        public Dictionary<string, KeyString[]> GetTestCase() {
            Dictionary<string, KeyString[]> dic = new Dictionary<string, KeyString[]>();
            for (int i = 0; i < RandomData.GetInt(5, 10); i++) {
                KeyString[] kss = new KeyString[RandomData.GetInt(5, 52)];
                string section = RandomData.GetString(this.chars, RandomData.GetInt(5, 30));
                for (int x = 0; x < kss.Length; x++) {
                    kss[x] = new KeyString() {
                        Key = RandomData.GetString(this.chars, RandomData.GetInt(5, 30)),
                        Value = RandomData.GetString(this.chars, RandomData.GetInt(5, 30)),
                    };
                }
                dic.Add(section, kss);
            }
            return dic;
        }

        public CaseModel Func_WriteString() {
            return new CaseModel() {
                NameSign = @"写入",
                ExeEvent = () => {
                    foreach (KeyValuePair<string, KeyString[]> section in this.dic) {
                        foreach (KeyString ks in section.Value) {
                            if (!ini.WriteString(section.Key, ks.Key, ks.Value)) {
                                Console.WriteLine("写入错误! section: {0} key: {1} value: {2}", section.Key, ks.Key, ks.Value);
                                return false;
                            }
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_ReadString() {
            return new CaseModel() {
                NameSign = @"读取",
                ExeEvent = () => {
                    foreach (KeyValuePair<string, KeyString[]> section in this.dic) {
                        foreach (KeyString ks in section.Value) {
                            string value = ini.ReadString(section.Key, ks.Key, string.Empty);
                            if (CheckData.IsStringNull(value) || !value.Equals(ks.Value)) {
                                Console.WriteLine("读取错误! section: {0} key: {1} value: {2} readvalue: ({3})", section.Key, ks.Key, ks.Value, value);
                                return false;
                            }
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_GetKeys() {
            return new CaseModel() {
                NameSign = @"获得部分的全部键",
                ExeEvent = () => {
                    VerifyIList<KeyString, string> verify = new VerifyIList<KeyString, string>(CalcWayEnum.DoubleCycle) {
                        Func_isEquals = (ks, key) => ks.Key == key,
                    };
                    foreach (KeyValuePair<string, KeyString[]> section in this.dic) {
                        verify.Answer = section.Value;
                        verify.Source = ini.GetKeys(section.Key);
                        verify.Func_notFind = (ks) => {
                            Console.WriteLine("GetKeys() 错误! section: {0} key: {1} value: {2} 没找到!", section.Key, ks.Key, ks.Value);
                        };
                        if (!verify.Calc()) {
                            return false;
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_GetSections() {
            return new CaseModel() {
                NameSign = @"获得所有部分",
                ExeEvent = () => {
                    VerifyIList<string, string> verify = new VerifyIList<string, string>(CalcWayEnum.DoubleCycle) {
                        Answer = new List<string>(this.dic.Keys),
                        Source = ini.GetSections(),
                        Func_notFind = (section) => {
                            Console.WriteLine("GetSections() 错误! section: {0} 没找到!", section);
                        }
                    };
                    return verify.Calc();
                },
            };
        }

        public CaseModel Func_GetKeyValues() {
            return new CaseModel() {
                NameSign = @"获得部分的所有键值",
                ExeEvent = () => {
                    VerifyIList<KeyString, KeyString> verify = new VerifyIList<KeyString, KeyString>(CalcWayEnum.DoubleCycle) {
                        Func_isEquals = (a, s) => a.Key == s.Key && a.Value == s.Value,
                    };
                    foreach (KeyValuePair<string, KeyString[]> section in this.dic) {
                        verify.Answer = section.Value;
                        verify.Source = ini.GetKeyValues(section.Key);
                        verify.Func_notFind = (ks) => {
                            Console.WriteLine("GetKeyValues() 错误! section: {0} key: {1} value: {2} 没找到!", section.Key, ks.Key, ks.Value);
                        };
                        if (!verify.Calc()) {
                            return false;
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_ClearSection() {
            return new CaseModel() {
                NameSign = @"清空 Section 部分(待)",
                ExeEvent = () => {
                    return true;
                },
            };
        }

        public CaseModel Func_DeleteKey() {
            return new CaseModel() {
                NameSign = @"删除键(待)",
                ExeEvent = () => {
                    return true;
                },
            };
        }

        public CaseModel Func_IsExistsValue() {
            return new CaseModel() {
                NameSign = @"是否存在值(待)",
                ExeEvent = () => {
                    return true;
                },
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram
{
    public class Test_AbsCase : CaseModel
    {
        public Test_AbsCase() {
            this.NameSign = @"季度房价奥四季度覅哦啊接我IE见覅哦啊结尾";
            this.ExeEvent = () => {
                Print.WriteLine(@"asdfawejflaijwefjaiewfw");
                Print.WriteLine(@"季度房价奥四季度覅哦啊接我");
                Print.WriteLine(@"dfasdf");
                Print.WriteLine(@"sdaf15");
                Print.WriteLine(@"123123124125412");
                Print.WriteLine(@"季度房价奥四季度覅哦啊接我");
            };
            this.SonCases = new CaseModel[] {
                new Test_TwoCeng(),
                new CaseModel() {
                    NameSign = "name 1",
                    ExeEvent = () => {
                        Print.WriteLine(@"name 1 exe method");
                    },
                    SonCases = new CaseModel[] {
                        new CaseModel() {
                            NameSign = "name 1",
                            ExeEvent = () => {
                                Print.WriteLine(@"name 1 exe method");
                            },
                        },new CaseModel() {
                            NameSign = "name 1",
                            ExeEvent = () => {
                                Print.WriteLine(@"name 1 exe method");
                            },
                            SonCases = new CaseModel[] {
                                new CaseModel() {
                                    NameSign = "name 1",
                                    ExeEvent = () => {
                                        Print.WriteLine(@"name 1 exe method");
                                    },
                                },new CaseModel() {
                                    NameSign = "name 1",
                                    ExeEvent = () => {
                                        Print.WriteLine(@"name 1 exe method");
                                    },
                                },new CaseModel() {
                                    NameSign = "name 1",
                                    ExeEvent = () => {
                                        Print.WriteLine(@"name 1 exe method");
                                    },
                                    SonCases = new CaseModel[] {
                                        new CaseModel() {
                                            NameSign = "name 1",
                                            ExeEvent = () => {
                                                Print.WriteLine(@"name 1 exe method");
                                            },
                                        },new CaseModel() {
                                            NameSign = "name 1",
                                            ExeEvent = () => {
                                                Print.WriteLine(@"name 1 exe method");
                                            },
                                        },new CaseModel() {
                                            NameSign = "name 1",
                                            ExeEvent = () => {
                                                Print.WriteLine(@"name 1 exe method");
                                            },
                                        }
                                    }
                                }
                            }
                        },new CaseModel() {
                            NameSign = "name 1",
                            ExeEvent = () => {
                                Print.WriteLine(@"name 1 exe method");
                            },
                        }
                    },
                }
            };
        }
    }
    public class Test_TwoCeng : CaseModel
    {
        public Test_TwoCeng() {
            this.NameSign = @"季度房价奥四季度覅哦啊接我IE见覅哦啊结尾";
            this.ExeEvent = () => {
                Print.WriteLine(@"asdfawejflaijwefjaiewfw");
                Print.WriteLine(@"季度房价奥四季度覅哦啊接我");
                Print.WriteLine(@"dfasdf");
                Print.WriteLine(@"sdaf15");
                Print.WriteLine(@"123123124125412");
                Print.WriteLine(@"季度房价奥四季度覅哦啊接我");
            };
            this.SonCases = new CaseModel[] {
                new Test_Result(),
                new Test_Result(),
            };
        }
    }
    public class Test_Result : CaseModel
    {
        public Test_Result() {
            this.NameSign = @"季度房价奥四季度覅哦啊接我IE见覅哦啊结尾";
            this.ExeEvent = () => {
                Print.WriteLine(@"asdfawejflaijwefjaiewfw");
                Print.WriteLine(@"季度房价奥四季度覅哦啊接我");
                Print.WriteLine(@"dfasdf");
                Print.WriteLine(@"sdaf15");
                Print.WriteLine(@"123123124125412");
                Print.WriteLine(@"季度房价奥四季度覅哦啊接我");
            };
        }
    }
}

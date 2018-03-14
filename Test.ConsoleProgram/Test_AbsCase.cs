using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.ConsoleProgram
{
    public class Test_AbsCase : CaseModel
    {
        public Test_AbsCase() {
            CaseModel model = new ItemCase();
            this.NameSign = model.NameSign;
            this.ExeEvent = model.ExeEvent;
            this.SonCases = new CaseModel[] {
                //new Test_TwoCeng(),
                //new ItemCase(),
                new CaseModel() {
                    NameSign = model.NameSign,
                    ExeEvent = model.ExeEvent,
                    SonCases = new CaseModel[] {
                        new CaseModel() {
                            NameSign = model.NameSign,
                            ExeEvent = model.ExeEvent,
                            SonCases = new CaseModel[] {
                                new CaseModel() {
                                    NameSign = model.NameSign,
                                    ExeEvent = model.ExeEvent,
                                    SonCases = new CaseModel[] {
                                        new CaseModel() {
                                            NameSign = model.NameSign,
                                            ExeEvent = model.ExeEvent,
                                            SonCases = new CaseModel[] {

                                            },
                                        },new CaseModel() {
                                            NameSign = model.NameSign,
                                            ExeEvent = model.ExeEvent,
                                            SonCases = new CaseModel[] {

                                            },
                                        },new CaseModel() {
                                            NameSign = model.NameSign,
                                            ExeEvent = model.ExeEvent,
                                            SonCases = new CaseModel[] {

                                            },
                                        },
                                    },
                                },
                            },
                        },new CaseModel() {
                            NameSign = model.NameSign,
                            ExeEvent = model.ExeEvent,
                            SonCases = new CaseModel[] {

                            },
                        },new CaseModel() {
                            NameSign = model.NameSign,
                            ExeEvent = model.ExeEvent,
                            SonCases = new CaseModel[] {
                                new CaseModel() {
                                    NameSign = model.NameSign,
                                    ExeEvent = model.ExeEvent,
                                    SonCases = new CaseModel[] {
                                        new CaseModel() {
                                            NameSign = model.NameSign,
                                            ExeEvent = model.ExeEvent,
                                            SonCases = new CaseModel[] {

                                            },
                                        },new CaseModel() {
                                            NameSign = model.NameSign,
                                            ExeEvent = model.ExeEvent,
                                            SonCases = new CaseModel[] {

                                            },
                                        },new CaseModel() {
                                            NameSign = model.NameSign,
                                            ExeEvent = model.ExeEvent,
                                            SonCases = new CaseModel[] {

                                            },
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
            };
        }
    }
    public class Test_TwoCeng : CaseModel
    {
        public Test_TwoCeng() {
            CaseModel model = new ItemCase();
            this.NameSign = model.NameSign;
            this.ExeEvent = model.ExeEvent;
            this.SonCases = new CaseModel[] {
                new ItemCase(),
                new ItemCase(),
                new ItemCase(),
            };
        }
    }
    public class ItemCase : CaseModel
    {
        public ItemCase() {
            this.NameSign = @"季度房价奥四季度覅哦啊接我IE见覅哦啊结尾";
            this.ExeEvent = () => {
                return new string[] {
                    @"asdfawejflaijwefjaiewfw",
                };
            };
        }
    }
}

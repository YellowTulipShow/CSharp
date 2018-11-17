using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_XML : CaseModel
    {
        public Test_XML() {
            NameSign = @"XML文件";
            SonCases = new CaseModel[] {
                Func_XmlSerializer(),
            };
        }

        public class Person
        {
            [XmlAttribute]
            public int Attribute { get; set; }

            [XmlIgnore]
            public int Ignore { get; set; }

            [XmlElement]
            public int Element { get; set; }

            public int Age { get; set; }

            public Customer Bill { get; set; }
        }
        public class Customer
        {
            public string Title { get; set; }
            public string Surname { get; set; }
        }

        public CaseModel Func_XmlSerializer() {
            return new CaseModel() {
                NameSign = @"序列化",
                ExeEvent = () => {
                    Person[] list = new Person[99];
                    for (int i = 0; i < list.Length; i++) {
                        list[i] = new Person() {
                            Bill = new Customer() {
                                Surname = RandomData.GetString(43),
                                Title = RandomData.GetString(31),
                            },
                        };
                    }

                    string absfile = PathHelp.CreateUseFilePath("/auto/XML/XmlSerializer", "testmodel.xml");
                    XmlWriterSettings settings = new XmlWriterSettings() {
                        CheckCharacters = true,
                        CloseOutput = true,
                        ConformanceLevel = ConformanceLevel.Document,
                        Encoding = YTS.Tools.Const.Format.FILE_ENCODING,
                        Indent = true,
                        IndentChars = @"    ",
                        NamespaceHandling = NamespaceHandling.Default,
                        NewLineOnAttributes = false,
                        NewLineHandling = NewLineHandling.Replace,
                        OmitXmlDeclaration = false,
                    };

                    using (XmlWriter writer = XmlWriter.Create(absfile, settings)) {
                        XmlSerializer xmlser = new XmlSerializer(typeof(Person[]));
                        xmlser.Serialize(writer, list);
                    }
                    return true;
                },
            };
        }
    }
}

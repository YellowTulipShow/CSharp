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

            //XmlSerializer xmlser = XmlSerializerFactory
        }

        public class Person
        {
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
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(absfile);

                    XmlWriterSettings settings = new XmlWriterSettings() {
                        CheckCharacters = true,
                        CloseOutput = true,
                        ConformanceLevel = ConformanceLevel.Document,
                        Encoding = Encoding.UTF8,
                        Indent = true,
                        IndentChars = @"    ",
                        NamespaceHandling = NamespaceHandling.Default,
                        NewLineOnAttributes = true, // NewLineChars = @"\n", 不要设置, 设置也只能设置为: \r\n
                        NewLineHandling = NewLineHandling.Replace,
                        OmitXmlDeclaration = true,
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

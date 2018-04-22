using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.Learn
{
    public class Learn_XML : CaseModel
    {
        public const string TEXTFOLDER_ABSPATH = @"D:\ZRQWork\Work-History-File\XMLLearnTestFolder";
        public const string DEFAULT_FILE_NAME = @"first_xml.xml";

        public Learn_XML() {
            base.NameSign = @"学习 XML 操作";
            base.ExeEvent = MainMethod;
            base.SonCases = new CaseModel[] {
                //new CaseModel() {
                //    NameSign = @"CreateFile",
                //    ExeEvent = CreateFile,
                //},
                //new CaseModel() {
                //    NameSign = @"LoadXML",
                //    ExeEvent = LoadXML,
                //},
                
                Insert(),
                //new CaseModel() {
                //    NameSign = @"Delete",
                //    ExeEvent = Delete,
                //},
                //new CaseModel() {
                //    NameSign = @"Update",
                //    ExeEvent = Update,
                //},
                //new CaseModel() {
                //    NameSign = @"Select",
                //    ExeEvent = Select,
                //},
            };
        }

        private XmlDocument GetXmlDocument() {
            string path = TEXTFOLDER_ABSPATH + @"\Book.xml";
            return XmlHelper.GetDocument(path);
        }

        private void MainMethod() {
            Print.WriteLine(System.Text.Encoding.UTF8.BodyName);
        }
        private void CreateFile() {
            string fileurl = TEXTFOLDER_ABSPATH + "\\" + DEFAULT_FILE_NAME;
            //string fileurl = "D:\\ZRQWork\\JianGuoYunFolder\\YellowTulipShowSystem\\YTS.CSharp\\Test.ConsoleProgram\\bin\\Debug\\DataXML\\dt_User.xml";
            Print.WriteLine(fileurl);

            XmlDocument document = new XmlDocument();
            XmlDeclaration xmldecl = document.CreateXmlDeclaration(XmlHelper.DECLARATION_VERSION_NO, Encoding.UTF8.BodyName, null);
            document.AppendChild(xmldecl);

            XmlElement root = document.CreateElement("root");
            document.AppendChild(root);

            document.Save(fileurl);
        }

        private CaseModel Insert() {
            return new CaseModel() {
                NameSign = @"Insert",
                ExeEvent = () => {
                    for (int i = 0; i < 500; i++) {
                        XmlDocument xmldocument = GetXmlDocument();
                        XmlNode root = xmldocument.SelectSingleNode(XmlHelper.ROOT_NODE_NAME);
                        if (CheckData.IsObjectNull(root)) {
                            root = xmldocument.CreateElement(XmlHelper.ROOT_NODE_NAME);
                            xmldocument.AppendChild(root);
                        }
                        root.AppendChild(CreateRandomNode(xmldocument));
                        xmldocument.Save(TEXTFOLDER_ABSPATH + @"\Book.xml");
                        Print.WriteLine("Complete: {0}", i);
                    }
                },
            };
            
        }
        private XmlNode CreateRandomNode(XmlDocument xmldocument) {
            char[] ISBN_Sour = new List<char>(CommonData.ASCII_Number()) { '-' }.ToArray();
            char[] Ascii_words = CommonData.ASCII_WordText();
            char[] Ascii_lowerenglish = CommonData.ASCII_LowerEnglish();
            XmlElement book = xmldocument.CreateElement(@"book");
            Dictionary<string, string> Dic_Attrs = new Dictionary<string, string>() {
                { @"Type", RandomData.GetChineseString(2) + @"课" },
                { @"ISBN", RandomData.GetString(ISBN_Sour, RandomData.GetInt(4, 13)) }
            };
            foreach (KeyValuePair<string, string> item in Dic_Attrs) {
                book.SetAttributeNode(XmlHelper.CreateNewAttribute(xmldocument, item.Key, item.Value));
            }
            Dictionary<string, string> Dic_Nodes = new Dictionary<string,string>() {
                { @"title", RandomData.GetString(Ascii_words, RandomData.GetInt(4, 10)) },
                { @"author", RandomData.GetString(Ascii_lowerenglish, RandomData.GetInt(2, 4)) },
                { @"price", RandomData.GetDouble().ToString() },
            };
            foreach (KeyValuePair<string, string> item in Dic_Nodes) {
                book.AppendChild(XmlHelper.CreateNewElement(xmldocument, item.Key, item.Value));
            }
            return book;
        }



        private void Delete() {
            Print.WriteLine("Delete Before: ");
            Select();

            XmlDocument xmldocument = GetXmlDocument();

            XmlElement xe = xmldocument.DocumentElement;
            string strpath = string.Format("/bookstore/book[@ISBN=\"{0}\"]", @"639372");
            XmlNode selectxe = xe.SelectSingleNode(strpath);
            selectxe.ParentNode.RemoveChild(selectxe);
            xmldocument.Save(TEXTFOLDER_ABSPATH + @"\Book.xml");

            Print.WriteLine("Delete After: ");
            Select();
        }
        private void Update() {
            Print.WriteLine("Update Before: ");
            Select();

            XmlDocument xmldocument = GetXmlDocument();

            XmlElement xe = xmldocument.DocumentElement;
            string strpath = string.Format("/bookstore/book[@ISBN=\"{0}\"]", @"145860");
            XmlElement selectxe = (XmlElement)xe.SelectSingleNode(strpath);
            selectxe.SetAttribute("Type", "正经的课");
            selectxe.SelectSingleNode("title").InnerText = @"正经的标题";
            selectxe.SelectSingleNode("author").InnerText = @"正经的作者";
            selectxe.SelectSingleNode("price").InnerText = (4054684.35d).ToString();

            xmldocument.Save(TEXTFOLDER_ABSPATH + @"\Book.xml");

            Print.WriteLine("Update After: ");
            Select();
        }
        private void Select() {
            XmlDocument xmldocument = GetXmlDocument();
            XmlNode root = xmldocument.SelectSingleNode("bookstore");
            foreach (XmlNode item in root.ChildNodes) {
                Print.WriteLine(JsonHelper.SerializeObject(new BookModel() {
                    BookType = item.Attributes["Type"].Value,
                    BookISBN = item.Attributes["ISBN"].Value,
                    BookName = item.SelectSingleNode("title").InnerText,
                    BookAuthor = item.SelectSingleNode("author").InnerText,
                    BookPrice = Convert.ToDouble(item.SelectSingleNode("price").InnerText),
                }));
            }
        }

        private void LoadXML() {
            XmlDocument xmldocument = GetXmlDocument();
            xmldocument.LoadXml("<bookstore></bookstore>");
            xmldocument.Save(TEXTFOLDER_ABSPATH + @"\Book.xml");
        }

        public class BookModel
        {
            public BookModel() { }

            /// <summary>
            /// 所对应的课程类型
            /// </summary>
            public string BookType {
                get { return bookType; }
                set { bookType = value; }
            }
            private string bookType;

            /// <summary>
            /// 书所对应的ISBN号
            /// </summary>
            public string BookISBN {
                get { return bookISBN; }
                set { bookISBN = value; }
            }
            private string bookISBN;

            /// <summary>
            /// 书名
            /// </summary>
            public string BookName {
                get { return bookName; }
                set { bookName = value; }
            }
            private string bookName;

            /// <summary>
            /// 作者
            /// </summary>
            public string BookAuthor {
                get { return bookAuthor; }
                set { bookAuthor = value; }
            }
            private string bookAuthor;

            /// <summary>
            /// 价格
            /// </summary>
            public double BookPrice {
                get { return bookPrice; }
                set { bookPrice = value; }
            }
            private double bookPrice;
        }
    }
}

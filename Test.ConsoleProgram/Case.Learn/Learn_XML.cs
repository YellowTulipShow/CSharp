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
                new CaseModel() {
                    NameSign = @"CreateFile",
                    ExeEvent = CreateFile,
                },
                //new CaseModel() {
                //    NameSign = @"LoadXML",
                //    ExeEvent = LoadXML,
                //},
                //new CaseModel() {
                //    NameSign = @"Insert",
                //    ExeEvent = Insert,
                //},
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
            //string fileurl = TEXTFOLDER_ABSPATH + "\\" + DEFAULT_FILE_NAME;
            string fileurl = "D:\\ZRQWork\\JianGuoYunFolder\\YellowTulipShowSystem\\YTS.CSharp\\Test.ConsoleProgram\\bin\\Debug\\DataXML\\dt_User.xml";
            Print.WriteLine(fileurl);

            XmlDocument document = new XmlDocument();
            XmlDeclaration xmldecl = document.CreateXmlDeclaration(XmlHelper.DECLARATION_VERSION_NO, Encoding.UTF8.BodyName, null);
            document.AppendChild(xmldecl);

            XmlElement root = document.CreateElement("root");
            document.AppendChild(root);

            document.Save(fileurl);
        }
        private void Insert() {
            Print.WriteLine("Insert Before: ");
            Select();

            XmlDocument xmldocument = GetXmlDocument();
            XmlNode root = xmldocument.SelectSingleNode("bookstore");
            root.AppendChild(CreateRandomNode(xmldocument));
            xmldocument.Save(TEXTFOLDER_ABSPATH + @"\Book.xml");

            Print.WriteLine("Insert After: ");
            Select();
        }
        private XmlNode CreateRandomNode(XmlDocument xmldocument) {
            char[] ISBN_Sour = new List<char>(CommonData.ASCII_Number()) { '-' }.ToArray();
            XmlElement book = xmldocument.CreateElement(@"book");
            book.SetAttributeNode(XmlHelper.CreateNewAttribute(xmldocument, @"Type", RandomData.GetChineseString(2) + @"课"));
            book.SetAttributeNode(XmlHelper.CreateNewAttribute(xmldocument, @"ISBN", RandomData.GetString(ISBN_Sour, RandomData.R.Next(4, 13))));
            book.AppendChild(XmlHelper.CreateNewElement(xmldocument, @"title", RandomData.GetChineseString(RandomData.R.Next(4, 10))));
            book.AppendChild(XmlHelper.CreateNewElement(xmldocument, @"author", RandomData.GetChineseString(RandomData.R.Next(2, 4))));
            book.AppendChild(XmlHelper.CreateNewElement(xmldocument, @"price", RandomData.GetDouble().ToString()));
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

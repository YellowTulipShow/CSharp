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
                new CaseModel() {
                    NameSign = @"Insert",
                    ExeEvent = Insert,
                },
                new CaseModel() {
                    NameSign = @"Delete",
                    ExeEvent = Delete,
                },
                new CaseModel() {
                    NameSign = @"Update",
                    ExeEvent = Update,
                },
                new CaseModel() {
                    NameSign = @"Select",
                    ExeEvent = Select,
                },
            };
        }

        private XmlDocument GetXmlDocument() {
            string path = TEXTFOLDER_ABSPATH + @"\Book.xml";
            return XmlHelper.GetXmlDocument(path);
        }

        private void MainMethod() {
            Print.WriteLine(System.Text.Encoding.UTF8.BodyName);
        }
        private void CreateFile() {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", System.Text.Encoding.UTF8.BodyName, null);
            doc.AppendChild(dec);
            //创建一个根节点（一级）
            XmlElement root = doc.CreateElement("First");
            doc.AppendChild(root);
            //创建节点（二级）
            XmlNode node = doc.CreateElement("Seconde");
            //创建节点（三级）
            XmlElement element1 = doc.CreateElement("Third1");
            element1.SetAttribute("Name", "Sam");
            element1.SetAttribute("ID", "665");
            element1.InnerText = "Sam Comment";
            node.AppendChild(element1);

            XmlElement element2 = doc.CreateElement("Third2");
            element2.SetAttribute("Name", "Round");
            element2.SetAttribute("ID", "678");
            element2.InnerText = "Round Comment";
            node.AppendChild(element2);



            root.AppendChild(node);
            string path = TEXTFOLDER_ABSPATH + DEFAULT_FILE_NAME;
            Print.WriteLine(path);
            doc.Save(path);
            Print.WriteLine(doc.OuterXml);
        }
        private void Insert() {
            char[] Sour_ISBN = new List<char>(CommonData.ASCII_Number()) { '-' }.ToArray();
            XmlDocument document = GetXmlDocument();
            XmlNode first_node = document.SelectSingleNode("bookstore");
            XmlElement book = XmlHelper.CreateXmlElement(document, @"book");
            book.SetAttributeNode(XmlHelper.CreateXmlAttribute(document, @"Type", RandomData.GetChineseString(2) + @"课"));
            book.SetAttributeNode(XmlHelper.CreateXmlAttribute(document, @"ISBN", RandomData.GetChineseString(RandomData.R.Next(4, 13))));
            book.AppendChild(XmlHelper.CreateXmlElement(document, @"title", RandomData.GetChineseString(RandomData.R.Next(4, 10))));
            book.AppendChild(XmlHelper.CreateXmlElement(document, @"author", RandomData.GetChineseString(RandomData.R.Next(2, 4))));
            book.AppendChild(XmlHelper.CreateXmlElement(document, @"price", RandomData.GetDouble().ToString()));
            first_node.AppendChild(book);
            document.Save(TEXTFOLDER_ABSPATH + @"\Book.xml");
        }
        private void Delete() {
        }
        private void Update() {
        }
        private void Select() {
            XmlDocument xmldoc = GetXmlDocument();

            XmlNode first_node = xmldoc.SelectSingleNode("bookstore");
            XmlNodeList xn_son_list = first_node.ChildNodes;

            List<BookModel> bool_m_list = new List<BookModel>();
            foreach (XmlNode item in xn_son_list) {
                bool_m_list.Add(new BookModel() {
                    BookType = item.Attributes["Type"].Value,
                    BookISBN = item.Attributes["ISBN"].Value,
                    BookName = item.SelectSingleNode("title").InnerText,
                    BookAuthor = item.SelectSingleNode("author").InnerText,
                    BookPrice = Convert.ToDouble(item.SelectSingleNode("price").InnerText),
                });
            }

            foreach (BookModel model in bool_m_list) {
                Print.WriteLine(JsonHelper.SerializeObject(model));
            }
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

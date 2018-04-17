using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// XML 帮助类
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// XML文件声明版本号
        /// </summary>
        public const string DECLARATION_VERSION_NO = @"1.0";
        /// <summary>
        /// 根节点名称
        /// </summary>
        public const string ROOT_NODE_NAME = @"root.root";

        /// <summary>
        /// 获得 XML 文档对象
        /// </summary>
        /// <param name="path">文档路径</param>
        /// <param name="defaultRootName">默认根节点名称</param>
        /// <returns></returns>
        public static XmlDocument GetDocument(string path, string defaultRootName = ROOT_NODE_NAME) {
            string abspath = PathHelper.ConvertToAbsolutePath(path);
            if (FileHelper.IsExistFile(abspath)) {
                return ReadExistDocument(path);
            } else {
                return CreateNewDocument(defaultRootName);
            }
        }
        public static XmlDocument ReadExistDocument(string path) {
            XmlDocument document = new XmlDocument();
            XmlReader reader = XmlReader.Create(path, new XmlReaderSettings() {
                IgnoreComments = true,
                IgnoreWhitespace = true,
            }); // 配置
            document.Load(reader); // 加载
            reader.Close(); // 关闭
            return document;
        }
        public static XmlDocument CreateNewDocument(string defaultRootName = ROOT_NODE_NAME) {
            XmlDocument document = new XmlDocument();
            document.AppendChild(CreateNewDeclaration(document)); // 声明节点
            document.AppendChild(document.CreateElement(defaultRootName)); // 根节点
            return document;
        }

        /// <summary>
        /// 创建新XML声明节点
        /// </summary>
        public static XmlDeclaration CreateNewDeclaration(XmlDocument document) {
            return document.CreateXmlDeclaration(DECLARATION_VERSION_NO, Encoding.UTF8.BodyName, null);
        }
        /// <summary>
        /// 创建新属性
        /// </summary>
        /// <param name="document">来源文档</param>
        /// <param name="attrName">属性名称</param>
        /// <param name="attrValue">属性值</param>
        public static XmlAttribute CreateNewAttribute(XmlDocument document, string attrName, string attrValue) {
            XmlAttribute xmlattr = document.CreateAttribute(attrName);
            xmlattr.InnerText = attrValue;
            return xmlattr;
        }
        /// <summary>
        /// 创建新元素
        /// </summary>
        /// <param name="document">来源文档</param>
        /// <param name="elementName">元素名称</param>
        public static XmlElement CreateNewElement(XmlDocument document, string elementName) {
            return document.CreateElement(elementName);
        }
        /// <summary>
        /// 创建新元素
        /// </summary>
        /// <param name="document">来源文档</param>
        /// <param name="elementName">元素名称</param>
        /// <param name="elementValue">元素值</param>
        public static XmlElement CreateNewElement(XmlDocument document, string elementName, string elementValue) {
            XmlElement xmlelement = CreateNewElement(document, elementName);
            xmlelement.InnerText = elementValue;
            return xmlelement;
        }
    }
}

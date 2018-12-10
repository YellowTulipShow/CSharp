using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace YTS.Tools
{
    /// <summary>
    /// XML帮助类
    /// </summary>
    public class XML
    {
        /// <summary>
        /// 序列化: 对象 转为 XML格式字符串
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="obj">对象数据</param>
        /// <returns>XML格式字符串</returns>
        public static string Serializer<T>(T obj) {
            return Serializer(obj, typeof(T));
        }

        /// <summary>
        /// 序列化: 对象 转为 XML格式字符串
        /// </summary>
        /// <param name="obj">对象数据</param>
        /// <param name="type">对象数据类型</param>
        /// <returns>XML格式字符串</returns>
        public static string Serializer(object obj, Type type) {
            XmlSerializer xml = new XmlSerializer(type);
            using (MemoryStream stream = new MemoryStream()) {
                xml.Serialize(stream, obj);
                stream.Position = 0;
                using (StreamReader sr = new StreamReader(stream)) {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 反序列化: XML字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="xml">XML字符串</param>
        /// <returns>对象实体, 默认为: default(T)</returns>
        public static T Deserialize<T>(string xml) {
            try {
                return (T)Deserialize(xml, typeof(T));
            } catch (Exception) {
                return default(T);
            }
        }

        /// <summary>
        /// 反序列化: XML字符串生成对象实体
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <param name="type">对象数据类型</param>
        /// <returns>对象数据</returns>
        public static object Deserialize(string xml, Type type) {
            using (StringReader sr = new StringReader(xml)) {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }

        /// <summary>
        /// 反序列化: XML字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="reader">读取流</param>
        /// <returns>对象实体</returns>
        public static T Deserialize<T>(XmlReader reader) {
            return (T)Deserialize(reader, typeof(T));
        }

        /// <summary>
        /// 反序列化: XML字符串生成对象实体
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="type">对象数据类型</param>
        /// <returns>对象实体</returns>
        public static object Deserialize(XmlReader reader, Type type) {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(reader);
        }
    }
}

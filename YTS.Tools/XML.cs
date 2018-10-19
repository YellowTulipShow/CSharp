using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace YTS.Tools
{
    public class XML
    {
        public static T Deserialize<T>(string xml) {
            try {
                return (T)Deserialize(xml, typeof(T));
            } catch (Exception) {
                return default(T);
            }
        }
        public static object Deserialize(string xml, Type type) {
            using (StringReader sr = new StringReader(xml)) {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }
        public static T Deserialize<T>(XmlReader reader) {
            return (T)Deserialize(reader, typeof(T));
        }
        public static object Deserialize(XmlReader reader, Type type) {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(reader);
        }

        public static string Serializer<T>(T obj) {
            return Serializer(obj, typeof(T));
        }
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
    }
}

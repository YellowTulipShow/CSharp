using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 本地XML文件-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class DAL_LocalXML<M> :
        DAL_LocalFile<M>,
        IFileInfo
        where M : AbsShineUpon, IFileInfo, new()
    {
        public DAL_LocalXML() : base() { }
        public DAL_LocalXML(FileShare fileShare) : base(fileShare) { }

        /// <summary>
        /// 配置-写入设置
        /// </summary>
        public XmlWriterSettings Config_XmlWriterSettings() {
            return new XmlWriterSettings() {
                CheckCharacters = true,
                CloseOutput = true,
                ConformanceLevel = ConformanceLevel.Document,
                Encoding = System.Text.Encoding.UTF8,
                Indent = true,
                IndentChars = @"    ",
                NamespaceHandling = NamespaceHandling.Default,
                NewLineOnAttributes = true, // NewLineChars = @"\n", 不要设置, 设置也只能设置为: \r\n
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = false,
            };
        }

        /// <summary>
        /// 配置-读取设置
        /// </summary>
        public XmlReaderSettings Config_XmlReaderSettings() {
            return new XmlReaderSettings() {
                IgnoreComments = true,
                IgnoreWhitespace = true,
                CloseInput = true,
            };
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="models">数据映射模型集合结果</param>
        /// <param name="isOverride">是否覆盖写入数据</param>
        /// <returns>是否成功</returns>
        public override bool Insert(M[] models, bool isOverride) {
            if (CheckData.IsSizeEmpty(models)) {
                models = new M[] { };
            }
            if (isOverride) {
                File.Delete(this.AbsFilePath);
            }
            XmlSerializer xs = new XmlSerializer(typeof(M[]));
            using (FileStream fs = File.Open(this.AbsFilePath, FileMode.OpenOrCreate, FileAccess.Write, this.FileShare)) {
                using (XmlWriter sw = XmlWriter.Create(fs, Config_XmlWriterSettings())) {
                    xs.Serialize(sw, models);
                    sw.Flush();
                }
            }
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="top">查询记录数目</param>
        /// <param name="where">查询条件</param>
        /// <returns>数据映射模型集合结果</returns>
        public override M[] Select(int top, Func<M, bool> where) {
            if (CheckData.IsObjectNull(where)) {
                where = model => true;
            }
            if (!File.Exists(this.AbsFilePath)) {
                return new M[] { };
            }
            using (FileStream fs = File.Open(this.AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, this.FileShare)) {
                fs.Position = 0;
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    using (XmlReader reader = XmlReader.Create(sr, Config_XmlReaderSettings())) {
                        XmlSerializer xs = new XmlSerializer(typeof(M[]));
                        M[] list = (M[])xs.Deserialize(reader);
                        List<M> results = new List<M>();
                        foreach (M model in list) {
                            M result = SingleModelProcessing(model);
                            if (CheckData.IsObjectNull(result)) {
                                continue;
                            }
                            if (where(result)) {
                                results.Add(result);
                            }
                            if (top > 0 && results.Count >= top) {
                                break;
                            }
                        }
                        return results.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// 单一模型处理
        /// </summary>
        /// <param name="model">需要处理的数据模型</param>
        /// <returns>处理的结果</returns>
        public virtual M SingleModelProcessing(M model) {
            return model;
        }
    }
}

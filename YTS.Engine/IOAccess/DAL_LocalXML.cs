using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 本地XML文件-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class DAL_LocalXML<M> :
        AbsDAL<M, Func<M, bool>, ShineUponParser<M, ShineUponInfo>, ShineUponInfo>,
        IFileInfo
        where M : AbsShineUpon, IFileInfo
    {
        /// <summary>
        /// 绝对文件路径
        /// </summary>
        public string AbsFilePath { get { return _AbsFilePath; } set { _AbsFilePath = value; } }
        private string _AbsFilePath = string.Empty;

        /// <summary>
        /// 用于控制其他 System.IO.FileStream 对象对同一文件可以具有的访问类型的常数
        /// </summary>
        public FileShare FileShare { get { return _FileShare; } set { _FileShare = value; } }
        private FileShare _FileShare = FileShare.Read;

        public DAL_LocalXML()
            : base() {
            this.AbsFilePath = CreateGetFilePath();
        }

        #region ====== using:IFileInfo ======
        public virtual string GetPathFolder() {
            return this.DefaultModel.GetPathFolder();
        }

        public virtual string GetFileName() {
            return this.DefaultModel.GetPathFolder();
        }

        /// <summary>
        /// 创建并获取文件路径
        /// </summary>
        /// <returns>文件的绝对路径</returns>
        public string CreateGetFilePath() {
            M model = ReflexHelp.CreateNewObject<M>();
            string rel_directory = model.GetPathFolder();
            string rel_filename = string.Format("{0}.xml", model.GetFileName());
            string abs_file_path = PathHelp.CreateUseFilePath(rel_directory, rel_filename);
            return abs_file_path;
        }
        #endregion

        public XmlWriterSettings Global_XmlWriterSettings() {
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

        public XmlReaderSettings Global_XmlReaderSettings() {
            return new XmlReaderSettings() {
                IgnoreComments = true,
                IgnoreWhitespace = true,
                CloseInput = true,
            };
        }

        public bool Insert(M[] models, bool isOverride) {
            if (CheckData.IsSizeEmpty(models)) {
                models = new M[] { };
            }
            if (isOverride) {
                File.Delete(this.AbsFilePath);
            }
            XmlSerializer xs = new XmlSerializer(typeof(M[]));
            using (FileStream fs = File.Open(this.AbsFilePath, FileMode.OpenOrCreate, FileAccess.Write, this.FileShare)) {
                using (XmlWriter sw = XmlWriter.Create(fs, Global_XmlWriterSettings())) {
                    xs.Serialize(sw, models);
                    sw.Flush();
                }
            }
            return true;
        }

        public M[] Select(int top, Func<M, bool> where) {
            if (CheckData.IsObjectNull(where)) {
                where = model => true;
            }
            if (!File.Exists(this.AbsFilePath)) {
                return new M[] { };
            }
            using (FileStream fs = File.Open(this.AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, this.FileShare)) {
                fs.Position = 0;
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    using (XmlReader reader = XmlReader.Create(sr, Global_XmlReaderSettings())) {
                        XmlSerializer xs = new XmlSerializer(typeof(M[]));
                        M[] list = (M[])xs.Deserialize(reader);
                        List<M> results = new List<M>();
                        foreach (M model in list) {
                            if (where(model)) {
                                results.Add(model);
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

        #region ====== using:AbsDAL<Model, Where, Parser, ParserInfo> ======
        public override bool Insert(M model) {
            return Insert(new M[] { model });
        }

        public override bool Insert(M[] models) {
            return Insert(models, false);
        }

        public override bool Delete(Func<M, bool> where) {
            List<M> nowlist = null;
            if (CheckData.IsObjectNull(where)) {
                nowlist = new List<M>();
            } else {
                nowlist = new List<M>(this.Select(0, null, null));
            }
            for (var i = nowlist.Count - 1; i >= 0; i--) {
                M model = nowlist[i];
                if (where(model)) {
                    nowlist.Remove(model);
                }
            }
            return this.Insert(nowlist.ToArray(), true);
        }

        public override bool Update(KeyObject[] kos, Func<M, bool> where) {
            if (CheckData.IsSizeEmpty(kos)) {
                return true;
            }
            List<M> nowlist = new List<M>(Select(0, null, null));
            if (CheckData.IsObjectNull(where)) {
                where = model => true;
            }
            Dictionary<string, ShineUponInfo> dic = this.Parser.GetAnalyticalResult();
            for (var i = nowlist.Count - 1; i >= 0; i--) {
                M model = nowlist[i];
                if (where(model)) {
                    foreach (KeyObject item in kos) {
                        if (!CheckData.IsStringNull(item.Key) && dic.ContainsKey(item.Key)) {
                            model = this.Parser.SetModelValue(dic[item.Key], model, item.Value);
                        }
                    }
                    nowlist[i] = model;
                }
            }
            return this.Insert(nowlist.ToArray(), true);
        }

        public override M[] Select(int top, Func<M, bool> where, KeyBoolean[] sorts) {
            return this.Select(top, where);
        }

        public override M[] Select(int pageCount, int pageIndex, out int recordCount, Func<M, bool> where, KeyBoolean[] sorts) {
            List<M> list = new List<M>(Select(0, where, null));
            recordCount = list.Count;
            if (CheckData.IsSizeEmpty(list)) {
                return new M[] { };
            }
            return ConvertTool.GetIListRange<M>(list, pageIndex, pageCount);
        }

        public override int GetRecordCount(Func<M, bool> where) {
            return Select(0, where, null).Length;
        }
        #endregion
    }
}

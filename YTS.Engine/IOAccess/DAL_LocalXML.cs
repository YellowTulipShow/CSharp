using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public class DAL_LocalXML<M> :
        AbsDAL<M, Func<M, bool>, ShineUponParser<M, ShineUponInfo>, ShineUponInfo>,
        IXMLInfo
        where M : AbsShineUpon, IXMLInfo
    {
        /// <summary>
        /// 绝对文件路径
        /// </summary>
        public string AbsFilePath { get { return _AbsFilePath; } set { _AbsFilePath = value; } }
        private string _AbsFilePath = string.Empty;

        public DAL_LocalXML()
            : base() {
            this.AbsFilePath = CreateGetFilePath();
        }

        #region ====== using:IFileInfo ======
        public string GetPathFolder() {
            return this.DefaultModel.GetPathFolder();
        }

        public string GetFileName() {
            return this.DefaultModel.GetPathFolder();
        }

        public string GetRootNodeName() {
            return this.DefaultModel.GetRootNodeName();
        }

        public string GetModelName() {
            return this.DefaultModel.GetModelName();
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

        public XmlNode ModelToXmlNode(M model, XmlDocument doc) {
            XmlElement element = doc.CreateElement(this.GetModelName());
            foreach (ShineUponInfo info in this.Parser.GetSortResult()) {
                if (CheckData.IsTypeEqual<AbsShineUpon>(info.Property)) {
                    ShineUponParser<AbsShineUpon, ShineUponInfo> sonparser = new ShineUponParser<AbsShineUpon, ShineUponInfo>();
                } else {
                    KeyObject ko = this.Parser.GetModelValue(info, model);
                    XmlElement item = doc.CreateElement(ko.Key);
                    item.InnerText = ModelValueToDataBaseValue(ko.Value);
                    element.AppendChild(item);
                }
            }
            return element;
        }

        public M XmlNodeToModel(XmlNode node) {
            if (CheckData.IsObjectNull(node)) {
                return null;
            }
            M model = ReflexHelp.CreateNewObject<M>();
            Dictionary<string, ShineUponInfo> dic = this.Parser.GetAnalyticalResult();
            foreach (XmlNode item in node.ChildNodes) {
                if (!dic.ContainsKey(item.Name)) {
                    continue;
                }
                ShineUponInfo info = dic[item.Name];
                object convert_value = DataBaseValueToModelValue(info, item.InnerText);
                model = this.Parser.SetModelValue(info, model, convert_value);
            }
            return model;
        }

        #region ====== using:AbsDAL<Model, Where, Parser, ParserInfo> ======
        public override bool Insert(M model) {
            return Insert(new M[] { model });
        }

        public override bool Insert(M[] models) {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.AbsFilePath);
            XmlNode root = doc.SelectSingleNode(GetRootNodeName());
            foreach (M model in models) {
                XmlNode node = ModelToXmlNode(model, doc);
                if (CheckData.IsObjectNull(node)) {
                    continue;
                }
                root.AppendChild(node);
            }
            doc.Save(this.AbsFilePath);
            return true;
        }

        public override bool Delete(Func<M, bool> where) {
            XmlDocument doc = new XmlDocument();
            if (CheckData.IsObjectNull(where)) {
                doc.LoadXml(NullRootNodeXML());
                doc.Save(this.AbsFilePath);
                return true;
            }
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; // 忽略文档里面的注释
            using (XmlReader reader = XmlReader.Create(this.AbsFilePath, settings)) {
                doc.Load(reader);
                XmlNode root = doc.SelectSingleNode(GetRootNodeName());
                for (int i = 0; i < root.ChildNodes.Count; i++) {
                    XmlNode node = root.ChildNodes[i];
                    M model = XmlNodeToModel(node);
                    if (CheckData.IsObjectNull(node) || !where(model)) {
                        continue;
                    }
                    root.RemoveChild(node);
                }
            }
            doc.Save(this.AbsFilePath);
            return true;
        }

        public string NullRootNodeXML() {
            return string.Format("<{0}></{0}>", GetRootNodeName());
        }

        public override bool Update(KeyObject[] kos, Func<M, bool> where) {
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; // 忽略文档里面的注释
            using (XmlReader reader = XmlReader.Create(this.AbsFilePath, settings)) {
                doc.Load(reader);
                XmlNode root = doc.SelectSingleNode(GetRootNodeName());
                for (int i = 0; i < root.ChildNodes.Count; i++) {
                    XmlNode oldnode = root.ChildNodes[i];
                    M model = XmlNodeToModel(oldnode);
                    if (!CheckData.IsObjectNull(where)) {
                        if (!where(model)) {
                            continue;
                        }
                    }
                    foreach (KeyObject ko in kos) {
                        XmlNode itemnode = oldnode.SelectSingleNode(ko.Key);
                        itemnode.InnerText = ModelValueToDataBaseValue(ko.Value);
                    }
                }
            }
            doc.Save(this.AbsFilePath);
            return true;
        }

        public override M[] Select(int top, Func<M, bool> where, KeyBoolean[] sorts) {
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; // 忽略文档里面的注释

            List<M> list = new List<M>();
            using (XmlReader reader = XmlReader.Create(this.AbsFilePath, settings)) {
                doc.Load(reader);
                XmlNode root = doc.SelectSingleNode(GetRootNodeName());
                foreach (XmlNode node in root.ChildNodes) {
                    M model = XmlNodeToModel(node);
                    if (CheckData.IsObjectNull(model)) {
                        continue;
                    }
                    if (!CheckData.IsObjectNull(where)) {
                        if (!where(model)) {
                            continue;
                        }
                    }
                    list.Add(model);
                    if (top > 0 && list.Count >= top) {
                        break;
                    }
                }
            }
            return list.ToArray();
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

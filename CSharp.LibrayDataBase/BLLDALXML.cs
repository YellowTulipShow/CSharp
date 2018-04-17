using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// XML 文件系统数据访问器
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class BLLXML<D, M> : AbsBLL<D, M>
        where D : DALXML<M>
        where M : AbsModelNull
    {
        public BLLXML(D dal) : base(dal) { }

        /// <summary>
        /// 获得文件绝对路径
        /// </summary>
        public string GetFileAbsPath() {
            return base.SelfDAL.GetFileAbsPath();
        }
    }

    /// <summary>
    /// XML 文件系统数据访问器
    /// </summary>
    /// <typeparam name="M">数据访问模型</typeparam>
    public class DALXML<M> : AbsDAL<M> where M : AbsModelNull
    {
        public DALXML() : base() { }

        /// <summary>
        /// 获得文件夹绝对路径
        /// </summary>
        public string GetDirectoryAbsPath() {
            return PathHelper.ConvertToAbsolutePath("/DataXML");
        }
        /// <summary>
        /// 获得文件绝对路径
        /// </summary>
        public string GetFileAbsPath() {
            string directoryabspath = GetDirectoryAbsPath();
            return string.Format("{0}\\{1}.xml", directoryabspath, GetTableName());
        }

        /// <summary>
        /// 默认根节点名称
        /// </summary>
        public virtual string DefaultRootNodeName() {
            return string.Format("model.root");
        }

        public override void Supplementary() {
            FileHelper.CreateDirectory(GetDirectoryAbsPath());
            string fileabspath = GetFileAbsPath();
            XmlDocument document = XmlHelper.GetDocument(fileabspath, DefaultRootNodeName());
            document.Save(fileabspath);
        }

        public override bool Insert(M model) {
            try {
                string fileabspath = GetFileAbsPath();
                string defaultRootNodeName = DefaultRootNodeName();
                string tablename = GetTableName();
                XmlDocument document = XmlHelper.GetDocument(fileabspath, defaultRootNodeName);
                XmlNode root = document.SelectSingleNode(defaultRootNodeName);
                XmlElement modelnode = document.CreateElement(tablename);
                foreach (ColumnItemModel item in base.GetCanWriteColumn()) {
                    KeyValueModel im = base.modelParser.GetModelValue(item, model);
                    if (CheckData.IsObjectNull(im) || CheckData.IsStringNull(im.Key))
                        continue;
                    XmlElement columnnode = document.CreateElement(im.Key);
                    columnnode.InnerText = im.Value;
                    modelnode.AppendChild(columnnode);
                }
                root.AppendChild(modelnode);
                document.Save(fileabspath);
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public override bool Delete(WhereModel wheres) {
            return false;
        }

        public override bool Update(FieldValueModel[] fielvals, WhereModel wheres) {
            return false;
        }

        public override M[] Select(int top = 0, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            return new M[] { };
        }

        public override M[] Select(int pageCount, int pageIndex, out int recordCount, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            recordCount = 0;
            return new M[] { };
        }

        public override int GetRecordCount(WhereModel wheres) {
            try {
                string fileabspath = GetFileAbsPath();
                string defaultRootNodeName = DefaultRootNodeName();
                string tablename = GetTableName();
                XmlDocument document = XmlHelper.GetDocument(fileabspath, defaultRootNodeName);
                XmlNode root = document.SelectSingleNode(defaultRootNodeName);
                XmlNodeList list = root.SelectNodes(string.Format("/{0}/{1}", defaultRootNodeName, tablename));
                return list.Count;
            } catch (Exception ex) {
                return 0;
            }
        }
    }
}

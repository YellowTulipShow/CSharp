using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 本地文件-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public abstract class DAL_LocalFile<M> :
        AbsDAL<M, Func<M, bool>, ShineUponParser<M, ShineUponInfo>, ShineUponInfo>,
        IFileInfo
        where M : AbsShineUpon, IFileInfo, new()
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

        public DAL_LocalFile()
            : base() {
            ReCreateAbsFilePath();
        }
        public DAL_LocalFile(FileShare fileShare)
            : base() {
            ReCreateAbsFilePath();
            this.FileShare = fileShare;
        }

        /// <summary>
        /// 重新创建绝对文件路径
        /// </summary>
        public void ReCreateAbsFilePath() {
            string rel_directory = GetPathFolder();
            string rel_filename = GetFileName();
            this.AbsFilePath = PathHelp.CreateUseFilePath(rel_directory, rel_filename);
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        #region ====== using:IFileInfo ======
        public virtual string GetPathFolder() {
            return this.DefaultModel.GetPathFolder();
        }

        public virtual string GetFileName() {
            return this.DefaultModel.GetFileName();
        }
        #endregion

        /* ================================== ~华丽的间隔线~ ================================== */

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="models">数据映射模型集合结果</param>
        /// <param name="isOverride">是否覆盖写入数据</param>
        /// <returns>是否成功</returns>
        public abstract bool Insert(M[] models, bool isOverride);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="top">查询记录数目</param>
        /// <param name="where">查询条件</param>
        /// <returns>数据映射模型集合结果</returns>
        public abstract M[] Select(int top, Func<M, bool> where);

        #region ====== using:AbsDAL<Model, Where, Parser, ParserInfo> ======

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model">数据映射模型</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public override bool Insert(M model) {
            return Insert(new M[] { model });
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="models">数据映射模型多条记录</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public override bool Insert(M[] models) {
            return Insert(models, false);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>是否成功 是:True 否:False</returns>
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="kos">需要更新的键值</param>
        /// <param name="where">查询条件</param>
        /// <returns>是否成功 是:True 否:False</returns>
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
                            this.Parser.SetValue_Object(dic[item.Key], model, item.Value);
                        }
                    }
                    nowlist[i] = model;
                }
            }
            return this.Insert(nowlist.ToArray(), true);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="top">查询记录数目</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public override M[] Select(int top, Func<M, bool> where, KeyBoolean[] sorts) {
            return this.Select(top, where);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pageCount">每页展现记录数</param>
        /// <param name="pageIndex">浏览页面索引</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public override M[] Select(int pageCount, int pageIndex, out int recordCount, Func<M, bool> where, KeyBoolean[] sorts) {
            M[] array = Select(0, where, sorts);
            recordCount = array.Length;
            if (CheckData.IsSizeEmpty(array)) {
                return new M[] { };
            }
            return ConvertTool.ToRangeList<M>(array, pageIndex, pageCount);
        }

        /// <summary>
        /// 获取记录数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        public override int GetRecordCount(Func<M, bool> where) {
            return Select(0, where, null).Length;
        }
        #endregion
    }
}

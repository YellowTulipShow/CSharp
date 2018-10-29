using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 本地文本文件-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class DAL_LocalTXT<M> :
        DAL_LocalFile<M>,
        IFileInfo
        where M : AbsShineUpon, IFileInfo, new()
    {
        public DAL_LocalTXT() : base() { }
        public DAL_LocalTXT(FileShare fileShare) : base(fileShare) { }

        /// <summary>
        /// 数据映射模型 - 转 - 文件行字符串内容
        /// </summary>
        /// <param name="model">数据映射模型</param>
        /// <returns>文件行字符串内容</returns>
        public virtual string ModelToString(M model) {
            return JSON.Serializer(model);
        }

        /// <summary>
        /// 文件行字符串内容 - 转 - 数据映射模型
        /// </summary>
        /// <param name="line">文件行字符串内容</param>
        /// <returns>数据映射模型</returns>
        public virtual M StringToModel(string line) {
            return JSON.Deserialize<M>(line);
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
            using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare)) {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8)) {
                    foreach (M model in models) {
                        string line = ModelToString(model);
                        sw.WriteLine(line);
                    }
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
            using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare)) {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    List<M> results = new List<M>();
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null) {
                        try {
                            M model = StringToModel(line);
                            if (CheckData.IsObjectNull(model)) {
                                continue;
                            }
                            if (where(model)) {
                                results.Add(model);
                            }
                            if (top > 0 && results.Count >= top) {
                                break;
                            }
                        } catch (Exception) {
                            continue;
                        }
                    }
                    return results.ToArray();
                }
            }
        }
    }
}

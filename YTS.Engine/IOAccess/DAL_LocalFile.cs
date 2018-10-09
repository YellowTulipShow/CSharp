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
    /// 本地文件-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class DAL_LocalFile<M> :
        AbsDAL<M, Func<M, bool>, ShineUponParser<M, ShineUponInfo>, ShineUponInfo>,
        IFileInfo
        where M : AbsShineUpon, IFileInfo
    {
        public DAL_LocalFile()
            : base() {
            Init();
        }
        public DAL_LocalFile(FileShare fileShare)
            : base() {
            Init();
            this.FileShare = fileShare;
        }

        #region === read / write ===
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

        /// <summary>
        /// 初始化当前对象信息
        /// </summary>
        public void Init() {
            this.AbsFilePath = CreateGetFilePath();
        }

        /// <summary>
        /// 创建并获取文件路径
        /// </summary>
        /// <returns>文件的绝对路径</returns>
        public string CreateGetFilePath() {
            M model = ReflexHelp.CreateNewObject<M>();
            string rel_directory = model.GetPathFolder();
            string rel_filename = string.Format("{0}.ytsdb", model.GetFileName());
            string abs_file_path = PathHelp.CreateUseFilePath(rel_directory, rel_filename);
            return abs_file_path;
        }

        /// <summary>
        /// 数据映射模型 - 转 - 文件行字符串内容
        /// </summary>
        /// <param name="model">数据映射模型</param>
        /// <returns>文件行字符串内容</returns>
        public string ModelToString(M model) {
            return JSON.SerializeObject(model);
        }

        /// <summary>
        /// 文件行字符串内容 - 转 - 数据映射模型
        /// </summary>
        /// <param name="line">文件行字符串内容</param>
        /// <returns>数据映射模型</returns>
        public M StringToModel(string line) {
            return JSON.DeserializeToObject<M>(line);
        }

        /// <summary>
        /// 清空文件的所有内容
        /// </summary>
        public void Clear() {
            File.Delete(AbsFilePath);
            File.Create(AbsFilePath).Close();
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="models">数据映射模型集合内容</param>
        public void Write(IList<M> models) {
            Write<M>(models, ModelToString);
        }
        /// <summary>
        /// 写
        /// </summary>
        /// <param name="lines">文件行字符串内容集合</param>
        public void Write(IList<string> lines) {
            Write<string>(lines, str => str);
        }
        /// <summary>
        /// 写
        /// </summary>
        /// <typeparam name="T">内容集合类型</typeparam>
        /// <param name="list">内容集合</param>
        /// <param name="tolineMethod">内容 - 转 - 文件行内容 委托执行方法</param>
        public void Write<T>(IList<T> list, Func<T, string> tolineMethod) {
            if (CheckData.IsSizeEmpty(list)) {
                throw new Exception(@"写入内容为空");
            }
            if (CheckData.IsObjectNull(tolineMethod)) {
                throw new Exception(@"转换方法为空");
            }
            using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare)) {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8)) {
                    for (int i = 0; i < list.Count; i++) {
                        string line = tolineMethod(list[i]);
                        sw.WriteLine(line);
                    }
                    sw.Flush();
                }
            }
        }
        /// <summary>
        /// 读
        /// </summary>
        /// <typeparam name="T">内容类型</typeparam>
        /// <param name="where">文件行内容 - 转 - 内容-委托方法(文件行内容, 已查出结果数量)</param>
        /// <returns>内容集合</returns>
        public T[] Read<T>(Func<string, int, T> where) {
            List<T> list = new List<T>();
            using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare)) {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null) {
                        T value = default(T);
                        try {
                            value = where(line, list.Count);
                        } catch (Exception) {
                            break;
                        }
                        if (CheckData.IsObjectNull(value)) {
                            continue; // 剔除空值
                        }
                        list.Add(value);
                    }
                }
            }
            return list.ToArray();
        }
        #endregion

        #region ====== using:IFileInfo ======
        /// <summary>
        /// 获取 /(根目录) 相对路径文件夹 格式: /xxx/xxx
        /// </summary>
        /// <returns>相对路径</returns>
        public string GetPathFolder() {
            return this.DefaultModel.GetPathFolder();
        }

        /// <summary>
        /// 获取文件名称 (只是名称, 不需要后缀)
        /// </summary>
        /// <returns>文件名</returns>
        public string GetFileName() {
            return this.DefaultModel.GetFileName();
        }
        #endregion

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
            Write(models);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public override bool Delete(Func<M, bool> where) {
            if (CheckData.IsObjectNull(where)) {
                Clear();
                return true;
            }
            string[] sava_lines = Read<string>((line, rlen) => {
                // 筛选符合规则的数据行
                M model = StringToModel(line);
                if (!CheckData.IsObjectNull(model)) {
                    if (where(model)) { // true 表示同意删除
                        return null;
                    }
                }
                return line;
            });
            Clear();
            Write(sava_lines);
            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="kos">需要更新的键值</param>
        /// <param name="where">查询条件</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public override bool Update(KeyObject[] kos, Func<M, bool> where) {
            if (CheckData.IsSizeEmpty(kos) || CheckData.IsObjectNull(where)) {
                return true;
            }
            Dictionary<string, ShineUponInfo> dicinfos = Parser.GetAnalyticalResult();
            M[] sava_models = Read<M>((line, rlen) => {
                // 筛选符合规则的数据行
                M model = StringToModel(line);
                if (CheckData.IsObjectNull(model)) {
                    return null;
                }
                if (where(model)) {
                    foreach (KeyObject ko in kos) {
                        if (dicinfos.ContainsKey(ko.Key)) {
                            Parser.SetModelValue(dicinfos[ko.Key], model, ko.Value);
                        }
                    }
                }
                return model;
            });
            Clear();
            Write(sava_models);
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="top">查询记录数目</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public override M[] Select(int top, Func<M, bool> where, KeyBoolean[] sorts = null) {
            if (CheckData.IsObjectNull(where)) {
                where = model => true;
            }
            return Read<M>((line, rlen) => {
                if (top > 0 && rlen >= top) {
                    throw new Exception();
                }
                // 筛选符合规则的数据行
                M model = StringToModel(line);
                if (CheckData.IsObjectNull(model)) {
                    return null;
                }
                if (!where(model)) { // false 表示不符合条件, 跳过
                    return null;
                }
                return model;
            });
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
            /*
                10条 1页 开始: 1 结束: 10

                10条 2页 开始: 11 结束: 20

                10条 3页 开始: 21 结束: 30

                x条 n页 开始: (n-1)*x + 1 结束 n*x

                8条 1页 开始: 1 结束: 8

                8条 2页 开始: 9 结束: 16
             */

            recordCount = 0;
            int start_index = (pageIndex - 1) * pageCount + 1;
            int end_index = pageIndex * pageCount;
            if (CheckData.IsObjectNull(where)) {
                where = model => true;
            }

            int arg_record_count = 0;
            M[] result = Read<M>((line, rlen) => {
                // 筛选符合规则的数据行
                M model = StringToModel(line);
                if (CheckData.IsObjectNull(model)) {
                    return null;
                }
                if (!where(model)) { // false 表示不符合条件, 跳过
                    return null;
                }
                arg_record_count++;
                if (start_index <= arg_record_count && arg_record_count <= end_index) {
                    return model;
                }
                return null;
            });
            recordCount = arg_record_count;
            return result;
        }

        /// <summary>
        /// 获取记录数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        public override int GetRecordCount(Func<M, bool> where) {
            return Read<string>((line, rlen) => {
                if (!CheckData.IsObjectNull(where)) {
                    // 筛选符合规则的数据行
                    M model = StringToModel(line);
                    if (CheckData.IsObjectNull(model)) {
                        return null;
                    }
                    if (!where(model)) { // false 表示不符合条件, 跳过
                        return null;
                    }
                }
                return line;
            }).Length;
        }
        #endregion
    }
}

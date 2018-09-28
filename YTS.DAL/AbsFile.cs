using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YTS.Tools;

namespace YTS.DAL
{
    public class AbsFile<M> :
        IShineUponInsert<M>
        //where M : Model.AbsShineUpon
        where M : Model.File.AbsFile
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

        public AbsFile() {
            Init();
        }
        public AbsFile(FileShare fileShare) {
            Init();
            this.FileShare = fileShare;
        }

        public void Init() {
            this.AbsFilePath = CreateGetFilePaht();
        }

        public string CreateGetFilePaht() {
            M model = ReflexHelp.CreateNewObject<M>();
            string rel_directory = model.GetPathFolder();
            string rel_filename = string.Format("{0}.ytsdb", model.GetFileName());
            string abs_file_path = PathHelp.CreateUseFilePath(rel_directory, rel_filename);
            return abs_file_path;
        }

        public string ModelToString(M model) {
            return JSON.SerializeObject(model);
        }
        public M StringToModel(string line) {
            return JSON.DeserializeToObject<M>(line);
        }

        public void Clear() {
            File.Delete(AbsFilePath);
            File.Create(AbsFilePath).Close();
        }

        public virtual bool Insert(M model) {
            return Insert(new M[] { model });
        }

        public virtual bool Insert(M[] models) {
            try {
                using (FileStream fs = File.Open(AbsFilePath, FileMode.Append, FileAccess.Write, FileShare)) {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8)) {
                        foreach (M model in models) {
                            string line = ModelToString(model);
                            sw.WriteLine(line);
                        }
                        sw.Flush();
                    }
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public virtual bool Delete(Func<M, bool> where) {
            if (CheckData.IsObjectNull(where)) {
                Clear();
                return true;
            }
            try {
                List<string> sava_lines = new List<string>();
                using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare)) {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                        string line = string.Empty;
                        while ((line = sr.ReadLine()) != null) {
                            // 筛选符合规则的数据行
                            M model = StringToModel(line);
                            if (!CheckData.IsObjectNull(model)) {
                                if (where(model)) { // true 表示同意删除
                                    continue;
                                }
                            }
                            // 否则所有数据还原
                            sava_lines.Add(line);
                        }
                    }
                }
                Clear();
                using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare)) {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8)) {
                        foreach (string line in sava_lines) {
                            sw.WriteLine(line);
                        }
                        sw.Flush();
                    }
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public virtual bool Update(Func<M, M> where) {
            if (CheckData.IsObjectNull(where)) {
                File.Delete(AbsFilePath);
                File.Create(AbsFilePath).Close();
                return true;
            }
            try {
                List<M> sava_models = new List<M>();
                using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare)) {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                        string line = string.Empty;
                        while ((line = sr.ReadLine()) != null) {
                            // 筛选符合规则的数据行
                            M model = StringToModel(line);
                            if (CheckData.IsObjectNull(model)) {
                                continue;
                            }
                            model = where(model);
                            if (CheckData.IsObjectNull(model)) {
                                continue;
                            }
                            sava_models.Add(model);
                        }
                    }
                }
                Clear();
                using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare)) {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8)) {
                        foreach (M model in sava_models) {
                            string line = ModelToString(model);
                            sw.WriteLine(line);
                        }
                        sw.Flush();
                    }
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public virtual M[] Select(int top, Func<M, bool> where) {
            if (CheckData.IsObjectNull(where)) {
                where = model => true;
            }
            List<M> lines = new List<M>();
            using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare)) {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null) {
                        if (top > 0 && lines.Count >= top) {
                            break;
                        }
                        // 筛选符合规则的数据行
                        M model = StringToModel(line);
                        if (CheckData.IsObjectNull(model)) {
                            continue;
                        }
                        if (!where(model)) { // false 表示不符合条件, 跳过
                            continue;
                        }
                        lines.Add(model);
                    }
                }
            }
            return lines.ToArray();
        }

        public virtual M[] Select(int pageCount, int pageIndex, out int recordCount, Func<M, bool> where) {
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
            List<M> lines = new List<M>();
            using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare)) {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null) {
                        // 筛选符合规则的数据行
                        M model = StringToModel(line);
                        if (CheckData.IsObjectNull(model)) {
                            continue;
                        }
                        if (!where(model)) { // false 表示不符合条件, 跳过
                            continue;
                        }
                        recordCount++;
                        if (start_index <= recordCount && recordCount <= end_index) {
                            lines.Add(model);
                        }
                    }
                }
            }
            return lines.ToArray();
        }

        public virtual int GetRecordCount(Func<M, bool> where) {
            int sum_count = 0;
            using (FileStream fs = File.Open(AbsFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare)) {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null) {
                        if (!CheckData.IsObjectNull(where)) {
                            // 筛选符合规则的数据行
                            M model = StringToModel(line);
                            if (CheckData.IsObjectNull(model)) {
                                continue;
                            }
                            if (!where(model)) { // false 表示不符合条件, 跳过
                                continue;
                            }
                        }
                        sum_count++;
                    }
                }
            }
            return sum_count;
        }

        public virtual M GetModel(Func<M, bool> where) {
            M[] list = Select(1, where);
            return (CheckData.IsSizeEmpty(list)) ? null : list[0];
        }
    }
}

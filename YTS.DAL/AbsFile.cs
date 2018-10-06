using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YTS.Tools;

namespace YTS.DAL
{
    public class AbsFile<M> :
        IShineUponInsert<M>
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
            Write(models);
            return true;
        }

        public virtual bool Delete(Func<M, bool> where) {
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

        public virtual bool Update(Func<M, M> where) {
            if (CheckData.IsObjectNull(where)) {
                return true;
            }
            M[] sava_models = Read<M>((line, rlen) => {
                // 筛选符合规则的数据行
                M model = StringToModel(line);
                if (CheckData.IsObjectNull(model)) {
                    return null;
                }
                model = where(model);
                if (CheckData.IsObjectNull(model)) {
                    return null;
                }
                return model;
            });
            Clear();
            Write(sava_models);
            return true;
        }

        public virtual M[] Select(int top, Func<M, bool> where) {
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

        public virtual int GetRecordCount(Func<M, bool> where) {
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

        public virtual M GetModel(Func<M, bool> where) {
            M[] list = Select(1, where);
            return (CheckData.IsSizeEmpty(list)) ? null : list[0];
        }

        public void Write(IList<M> models) {
            Write<M>(models, ModelToString);
        }
        public void Write(IList<string> lines) {
            Write<string>(lines, str => str);
        }
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
    }
}

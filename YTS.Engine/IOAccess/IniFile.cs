using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// ini 配置文件
    /// 引用自: https://jingyan.baidu.com/article/d5c4b52bc5da02da570dc540.html
    /// </summary>
    public class IniFile
    {
        #region === kernel32 API ===
        /// <summary>
        /// 声明INI文件的API函数: 写
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 声明INI文件的API函数: 读
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        #endregion

        /// <summary>
        /// ini 文件绝对路径
        /// </summary>
        public string AbsFilePath = null; //INI文件名

        #region === Init ===
        /// <summary>
        /// 构造函数: 初始化文件路径
        /// </summary>
        /// <param name="absfilepath">文件绝对路径</param>
        public IniFile(string absfilepath) {
            // 判断文件是否存在
            FileInfo fileInfo = new FileInfo(absfilepath);
            //Todo:搞清枚举的用法
            if (!fileInfo.Exists) {
                //文件不存在，建立文件
                using (FileStream fs = File.Open(absfilepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write)) {
                    using (StreamWriter sw = new StreamWriter(fs, this.GetEncoding())) {
                        sw.WriteLine("# ini 配置\n");
                        sw.Flush();
                    }
                }
            }
            //必须是完全路径，不能是相对路径
            this.AbsFilePath = fileInfo.FullName;
        }

        /// <summary>
        /// 析构函数: 将操作结果更新到文件内 并 确保资源的释放
        /// </summary>
        ~IniFile() {
            UpdateFile();
        }

        /// <summary>
        /// Note:对于Win9X，来说需要实现UpdateFile方法将缓冲中的数据写入文件
        /// 在Win NT, 2000和XP上，都是直接写文件，没有缓冲，所以，无须实现UpdateFile
        /// 执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。
        /// </summary>
        public void UpdateFile() {
            WritePrivateProfileString(null, null, null, AbsFilePath);
        }

        public Encoding GetEncoding() {
            return YTS.Tools.Const.Format.FILE_ENCODING;
        }
        #endregion

        #region === Basic ===
        /// <summary>
        /// 写入一条记录
        /// </summary>
        /// <param name="section">部分名称</param>
        /// <param name="key">键名称</param>
        /// <param name="value">值内容</param>
        /// <returns>是否成功</returns>
        public bool WriteString(string section, string key, string value) {
            return WritePrivateProfileString(section, key, value, AbsFilePath);
        }

        /// <summary>
        /// 读取一条记录
        /// </summary>
        /// <param name="section">部分名称</param>
        /// <param name="key">键名称</param>
        /// <param name="defval">默认值(可选填)</param>
        /// <returns>值内容</returns>
        public string ReadString(string section, string key, string defval = "") {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(section, key, defval, Buffer, Buffer.GetUpperBound(0), AbsFilePath);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            string s = this.GetEncoding().GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }
        #endregion

        #region === Get Infos ===
        /// <summary>
        /// 获得指定的 section 名称中的所有 key 名称集合
        /// </summary>
        /// <param name="section">部分名称</param>
        /// <returns>key 名称集合</returns>
        public string[] GetKeys(string section) {
            Byte[] Buffer = new Byte[16384];
            int bufLen = GetPrivateProfileString(section, null, null, Buffer, Buffer.GetUpperBound(0), AbsFilePath);
            return GetStringsFromBuffer(Buffer, bufLen);
        }

        /// <summary>
        /// 从字节缓冲区中读取字符串集合
        /// </summary>
        /// <param name="Buffer">字节缓冲区</param>
        /// <param name="bufLen">缓冲区长度</param>
        /// <param name="Strings">字符串集合</param>
        private string[] GetStringsFromBuffer(Byte[] Buffer, int bufLen) {
            List<string> strings = new List<string>();
            if (bufLen != 0) {
                int start = 0;
                for (int i = 0; i < bufLen; i++) {
                    if ((Buffer[i] == 0) && ((i - start) > 0)) {
                        String s = this.GetEncoding().GetString(Buffer, start, i - start);
                        strings.Add(s);
                        start = i + 1;
                    }
                }
            }
            return strings.ToArray();
        }

        /// <summary>
        /// 读取所有的 section 的名称
        /// </summary>
        /// <returns>字符串集合</returns>
        public string[] GetSections() {
            //Note:必须得用Bytes来实现，StringBuilder只能取到第一个section
            byte[] buffer = new byte[65535];
            int bufLen = GetPrivateProfileString(null, null, null, buffer, buffer.GetUpperBound(0), AbsFilePath);
            return GetStringsFromBuffer(buffer, bufLen);
        }

        /// <summary>
        /// 读取指定的 section 的所有 key-value 集合
        /// </summary>
        /// <param name="section">部分名称</param>
        /// <returns>键值集合</returns>
        public KeyString[] GetKeyValues(string section) {
            string[] keys = GetKeys(section);
            List<KeyString> kss = new List<KeyString>();
            foreach (string key in keys) {
                kss.Add(new KeyString() {
                    Key = key,
                    Value = ReadString(section, key, ""),
                });
            }
            return kss.ToArray();
        }
        #endregion

        #region === Function ===
        /// <summary>
        /// 清除某个section
        /// </summary>
        /// <param name="section">某个部分</param>
        /// <returns>是否成功</returns>
        public bool ClearSection(string section) {
            return WritePrivateProfileString(section, null, null, AbsFilePath);
        }

        /// <summary>
        /// 删除某个 section 下的 某个键
        /// </summary>
        /// <param name="section">部分名称</param>
        /// <param name="key">键名称</param>
        public void DeleteKey(string section, string key) {
            WritePrivateProfileString(section, key, null, AbsFilePath);
        }

        /// <summary>
        /// 检查某个 section 下的某个 key-value 是否存在
        /// </summary>
        /// <param name="section">部分名称</param>
        /// <param name="key">键名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistsValue(string section, string key) {
            //StringCollection keys = GetKeys(section);
            //return keys.Contains(key);
            string value = ReadString(section, key);
            return !CheckData.IsStringNull(key);
        }
        #endregion

        #region === Reflex ===
        /// <summary>
        /// 读取: ini.file => model
        /// </summary>
        public void IniConfig_Read(AbsShineUpon model) {
            Type mtype = model.GetType();
            string section_name = mtype.FullName;
            ShineUponParser perser = new ShineUponParser(mtype);
            foreach (ShineUponInfo info in perser.GetDictionary().Values) {
                string sinival = ReadString(section_name, info.Name, string.Empty);
                if (CheckData.IsStringNull(sinival)) {
                    continue;
                }
                perser.SetValue_Object(info, model, sinival);
            }
        }
        /// <summary>
        /// 读取: model => ini.file
        /// </summary>
        public void IniConfig_Write<M>(M model) where M : AbsShineUpon {
            if (CheckData.IsObjectNull(model)) {
                return;
            }
            Type mtype = model.GetType();
            string section_name = mtype.FullName;
            ShineUponParser perser = new ShineUponParser(mtype);
            foreach (ShineUponInfo info in perser.GetDictionary().Values) {
                KeyString ks = perser.GetValue_KeyString(info, model);
                if (CheckData.IsObjectNull(ks)) {
                    continue;
                }
                WriteString(section_name, info.Name, ks.Value);
            }
        }
        #endregion
    }
}

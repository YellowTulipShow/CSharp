using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据类型解析器
    /// </summary>
    public class DataTypeParser
    {
        public DataTypeParser GetInstance() {
            return HolderClass.dataTypeParser;
        }
        private static class HolderClass {
            public static DataTypeParser dataTypeParser = new DataTypeParser();
        }
        private DataTypeParser() { }
    }
}

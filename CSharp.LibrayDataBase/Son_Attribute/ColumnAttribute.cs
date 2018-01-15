using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase.MSSDataType;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// Microsoft SQL Server 数据库字段类型: 值类型
    /// </summary>
    public enum MSSFieldTypeStruct
    {
        /// <summary>
        /// 数值整形
        /// </summary>
        Int = 1,
        /// <summary>
        /// 金额字段 搭配C#程序的数据类型是 decimal
        /// </summary>
        Money = 4,
    }

    /// <summary>
    /// Microsoft SQL Server 数据库字段类型: 限定字符长度
    /// </summary>
    public enum MSSFieldTypeCharCount
    {
        ///// <summary>
        ///// 固定长度，存储ANSI字符，不足的补英文半角空格。(1-8000)
        ///// </summary>
        //Char = 5,
        ///// <summary>
        ///// 固定长度，存储Unicode字符，不足的补英文半角空格。(1-4000)
        ///// </summary>
        //NChar = 6,
        ///// <summary>
        ///// 可变长度，存储ANSI字符，根据数据长度自动变化。(1-8000，MAX Yes size: 2^31-1byte 4GB)
        ///// </summary>
        //VarChar = 7,
        /// <summary>
        /// 可变长度，存储Unicode字符，根据数据长度自动变化。(1-4000，MAX Yes size: 2^31-1byte 4GB)
        /// </summary>
        NVarChar = 2,
    }

    /// <summary>
    /// Microsoft SQL Server 数据库字段类型: 不为空, 需要默认值
    /// </summary>
    public enum MSSFieldTypeDefault
    {
        Datetime = 3,
    }

    /// Microsoft SQL Server 默认值
    public enum MSSDefalutValues
    {
        DateTimeNow = 1,
        SqlMinDateTime = 2,
        SqlMaxDateTime = 3,
    }

    /// <summary>
    /// 数据表列特性  同一程序不能多个解释。无法继承此类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : AbsBasicsAttribute
    {
        #region === Constructor Init DataType ===
        public ColumnAttribute(MSSFieldTypeStruct dbDataType) {
            this._dbType = AnalysisTypeStruct(dbDataType);
        }
        public ColumnAttribute(MSSFieldTypeDefault dbDataType, MSSDefalutValues defalutEnum) {
            this._dbType = AnalysisTypeDefalut(dbDataType, AnalysisDefalutValue(defalutEnum));
        }
        public ColumnAttribute(MSSFieldTypeCharCount dbDataType, ushort charCount) {
            this._dbType = AnalysisTypeCharCount(dbDataType, charCount);
        }
        private AbsFieldType AnalysisTypeStruct(MSSFieldTypeStruct dbDataType) {
            switch (dbDataType) {
                case MSSFieldTypeStruct.Int: return new MSSInt();
                case MSSFieldTypeStruct.Money: return new MSSMoney();
                default: return new MSSNvarchar(MSSNvarchar.MAXCHARSIGN);
            }
        }
        private AbsFieldType AnalysisTypeCharCount(MSSFieldTypeCharCount dbDataType, ushort charCount) {
            switch (dbDataType) {
                case MSSFieldTypeCharCount.NVarChar: return new MSSNvarchar(charCount);
                default: return new MSSNvarchar(MSSNvarchar.MAXCHARSIGN);
            }
        }
        private AbsFieldType AnalysisTypeDefalut(MSSFieldTypeDefault dbDataType, object defalutValue) {
            switch (dbDataType) {
                case MSSFieldTypeDefault.Datetime: return new MSSDatetime(ConvertTool.ObjToSqlDateTime(defalutValue, new SqlDateTime(DateTime.Now)));
                default: return new MSSNvarchar(MSSNvarchar.MAXCHARSIGN);
            }
        }
        private object AnalysisDefalutValue(MSSDefalutValues defalutEnum) {
            switch (defalutEnum) {
                case MSSDefalutValues.DateTimeNow: return DateTime.Now.ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND);
                case MSSDefalutValues.SqlMinDateTime: return SqlDateTime.MinValue.Value.ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND);
                case MSSDefalutValues.SqlMaxDateTime: return SqlDateTime.MaxValue.Value.ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND);
                default: return string.Empty;
            }
        }
        #endregion


        /// <summary>
        /// 获取或设置数据库列的类型。
        /// </summary>
        public AbsFieldType DbType { get { return _dbType; } }
        private AbsFieldType _dbType = null;


        /// <summary>
        /// 获取或设置指示是否此类成员表示的部分或全部表的主键的列。
        /// </summary>
        public bool IsPrimaryKey {
            get { return _isPrimaryKey; }
            set {
                _isPrimaryKey = value;
                if (_isPrimaryKey) {
                    IsCanBeNull = !_isPrimaryKey;
                }
            }
        }
        private bool _isPrimaryKey = false;


        /// <summary>
        /// 获取或设置是否为标识列。
        /// </summary>
        public bool IsIDentity {
            get { return _isIDentity; }
            set {
                _isIDentity = value;
                if (_isIDentity) {
                    IsCanBeNull = !_isIDentity;
                    IsDbGenerated = _isIDentity;
                }
            }
        }
        private bool _isIDentity = false;


        /// <summary>
        /// 获取或设置是否列包含数据库自动生成的值。
        /// </summary>
        public bool IsDbGenerated {
            get { return _isDbGenerated; }
            set {
                _isDbGenerated = value;
                if (_isDbGenerated) {
                    IsCanBeNull = !_isDbGenerated;
                }
            }
        }
        private bool _isDbGenerated = false;


        /// <summary>
        /// 获取或设置是否列可以包含 null 值。
        /// </summary>
        public bool IsCanBeNull { get { return _iscanBeNull; } set { _iscanBeNull = value; } }
        private bool _iscanBeNull = true;
    }
}

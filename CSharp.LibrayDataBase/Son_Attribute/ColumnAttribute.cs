using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表列特性  同一程序不能多个解释。无法继承此类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : AbsBasicsAttribute
    {
        public enum FieldTypeStruct
        {
            Int = 2,
            Money = 4,
        }
        public ColumnAttribute(FieldTypeStruct dbDataType) {
            this._dbType = AnalysisTypeStruct(dbDataType);
        }
        private AbsFieldType AnalysisTypeStruct(FieldTypeStruct dbDataType) {
            switch (dbDataType) {
                case FieldTypeStruct.Int: return new Int();
                case FieldTypeStruct.Money: return new Money();
                default: return new Nvarchar(Nvarchar.MAXCHARSIGN);
            }
        }

        public enum FieldTypeDefault
        {
            Datetime = 3,
        }
        public enum DefalutValues
        {
            DateTimeNow = 1,
            SqlMinDateTime = 2,
            SqlMaxDateTime = 3,
        }
        public ColumnAttribute(FieldTypeDefault dbDataType, DefalutValues defalutEnum) {
            this._dbType = AnalysisTypeDefalut(dbDataType, AnalysisDefalutValue(defalutEnum));
        }
        private object AnalysisDefalutValue(DefalutValues defalutEnum) {
            switch (defalutEnum) {
                case DefalutValues.DateTimeNow: return DateTime.Now.ToString();
                case DefalutValues.SqlMinDateTime: return SqlDateTime.MinValue.Value.ToString();
                case DefalutValues.SqlMaxDateTime: return SqlDateTime.MaxValue.Value.ToString();
                default: return string.Empty;
            }
        }
        private AbsFieldType AnalysisTypeDefalut(FieldTypeDefault dbDataType, object defalutValue) {
            switch (dbDataType) {
                case FieldTypeDefault.Datetime: return new Datetime(ConvertTool.ObjToSqlDateTime(defalutValue, new SqlDateTime(DateTime.Now)));
                default: return new Nvarchar(Nvarchar.MAXCHARSIGN);
            }
        }

        public enum FieldTypeCharCount
        {
            Nvarchar = 1,
        }
        public ColumnAttribute(FieldTypeCharCount dbDataType, ushort charCount) {
            this._dbType = AnalysisTypeCharCount(dbDataType, charCount);
        }
        private AbsFieldType AnalysisTypeCharCount(FieldTypeCharCount dbDataType, ushort charCount) {
            switch (dbDataType) {
                case FieldTypeCharCount.Nvarchar: return new Nvarchar(charCount);
                default: return new Nvarchar(Nvarchar.MAXCHARSIGN);
            }
        }


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

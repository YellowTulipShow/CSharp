using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase.MSSDataType;

namespace CSharp.LibrayDataBase
{
    #region === DataBase Field Type Enum ===
    /// <summary>
    /// Microsoft SQL Server 数据库字段类型: 值类型
    /// </summary>
    public enum MSSFieldTypeStruct
    {
        /// <summary>
        /// 数值整形
        /// </summary>
        Int = 0,
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
        /// <summary>
        /// 固定长度，存储ANSI字符，不足的补英文半角空格。(1-8000, 不存中文)
        /// </summary>
        Char = 5,
        /// <summary>
        /// 固定长度，存储Unicode字符，不足的补英文半角空格。(1-4000)
        /// </summary>
        NChar = 6,
        /// <summary>
        /// 可变长度，存储ANSI字符，根 据数据长度自动变化。(1-8000，不存中文 MAX Yes size: 2^31-1byte 4GB)
        /// </summary>
        VarChar = 7,
        /// <summary>
        /// 可变长度，存储Unicode字符，根据数据长度自动变化。(1-4000，MAX Yes size: 2^31-1byte 4GB)
        /// </summary>
        NVarChar = 0,
    }

    /// <summary>
    /// Microsoft SQL Server 数据库字段类型: 不为空, 需要默认值
    /// </summary>
    public enum MSSFieldTypeDefault
    {
        Datetime = 0,
    }

    /// <summary>
    /// Microsoft SQL Server 默认值
    /// </summary>
    public enum MSSDefalutValues
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        DateTimeNow = 0,
        /// <summary>
        /// Microsoft SQL Server 最小值时间
        /// </summary>
        SqlMinDateTime = 2,
        /// <summary>
        /// Microsoft SQL Server 最大值时间
        /// </summary>
        SqlMaxDateTime = 3,
    }
    #endregion

    #region === CSharp Program Data Type Enum ===
    /// <summary>
    /// CSharp 程序数据类型
    /// </summary>
    public enum CsDTEnum
    {
        /// <summary>
        /// 值类型(int,float,double,char...), 常规类型(string)
        /// </summary>
        Struct,
        /// <summary>
        /// 枚举类型
        /// </summary>
        Enum,
    }
    #endregion

    /// <summary>
    /// 数据表列特性  同一程序不能多个解释。无法继承此类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : AbsBasicAttribute
    {
        #region === Constructor Init DataType ===
        /// <summary>
        /// 初始化信息 数据类型为一种: 值类型
        /// </summary>
        /// <param name="dbDataType">枚举: 数据库值类型</param>
        public ColumnAttribute(MSSFieldTypeStruct dbDataType) {
            switch (dbDataType) {
                case MSSFieldTypeStruct.Int:
                    this._dbType = new MSSInt();
                    break;
                case MSSFieldTypeStruct.Money:
                    this._dbType = new MSSMoney();
                    break;
                default:
                    this._dbType = new MSSNVarChar(AbsFieldTypeCharMAX.MAXCHARSIGN);
                    break;
            }
        }
        /// <summary>
        /// 初始化信息 数据类型为一种: 默认值特殊类型
        /// </summary>
        /// <param name="dbDataType">枚举: 数据库值类型</param>
        /// <param name="defalutEnum">枚举: 数据库默认值</param>
        public ColumnAttribute(MSSFieldTypeDefault dbDataType, MSSDefalutValues defalutEnum) {
            object defalutValue = new object();
            switch (defalutEnum) {
                case MSSDefalutValues.DateTimeNow:
                    defalutValue = DateTime.Now.ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND);
                    break;
                case MSSDefalutValues.SqlMinDateTime:
                    defalutValue = SqlDateTime.MinValue.Value.ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND);
                    break;
                case MSSDefalutValues.SqlMaxDateTime:
                    defalutValue = SqlDateTime.MaxValue.Value.ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND);
                    break;
                default:
                    defalutValue = string.Empty;
                    break;
            }

            switch (dbDataType) {
                case MSSFieldTypeDefault.Datetime:
                    this._dbType = new MSSDatetime(ConvertTool.ObjToSqlDateTime(defalutValue, new SqlDateTime(DateTime.Now)));
                    break;
                default:
                    this._dbType = new MSSNVarChar(AbsFieldTypeCharMAX.MAXCHARSIGN);
                    break;
            }
        }
        /// <summary>
        /// 初始化信息 数据类型为一种: 字符类型 需指定字符长度
        /// </summary>
        /// <param name="dbDataType">枚举: 数据库值类型</param>
        /// <param name="charCount">正整数: 字符长度</param>
        public ColumnAttribute(MSSFieldTypeCharCount dbDataType, ushort charCount) {
            switch (dbDataType) {
                case MSSFieldTypeCharCount.Char:
                    this._dbType = new MSSChar(charCount);
                    break;
                case MSSFieldTypeCharCount.NChar:
                    this._dbType = new MSSNChar(charCount);
                    break;
                case MSSFieldTypeCharCount.VarChar: 
                    this._dbType = new MSSVarChar(charCount);
                    break;
                case MSSFieldTypeCharCount.NVarChar: 
                    this._dbType = new MSSNVarChar(charCount);
                    break;
                default:
                    this._dbType = new MSSNVarChar(AbsFieldTypeCharMAX.MAXCHARSIGN);
                    break;
            }
        }
        #endregion


        /// <summary>
        /// 获取或设置数据库列的类型。
        /// </summary>
        public AbsFieldType DbType {
            get {
                return !_dbType.IsObjectNull() ? _dbType :
                    new MSSNVarChar(AbsFieldTypeCharMAX.MAXCHARSIGN);
            }
        }
        private AbsFieldType _dbType = null;


        /// <summary>
        /// 设置字段CSharp数据类型-枚举指定。
        /// </summary>
        public CsDTEnum CsTypeEnumSign {
            get { return CsDTEnum.Struct; }
            set {
                switch (value) {
                    case CsDTEnum.Struct: _csType = new MCSDataType.MCSStruct(); break;
                    case CsDTEnum.Enum: _csType = new MCSDataType.MCSEnum(); break;
                    default: CsTypeEnumSign = CsDTEnum.Struct; break;
                }
            }
        }
        /// <summary>
        /// 获取字段CSharp数据类型
        /// </summary>
        public AbsCsType CsType {
            get { return !CheckData.IsObjectNull(_csType) ? _csType : new MCSDataType.MCSStruct(); }
        }
        private AbsCsType _csType = null;


        /// <summary>
        /// 获取或设置指示是否此类成员表示的部分或全部表的主键的列。
        /// </summary>
        public bool IsPrimaryKey {
            get { return _isPrimaryKey; }
            set {
                _isPrimaryKey = value;
                if (_isPrimaryKey) {
                    IsCanBeNull = !_isPrimaryKey;
                    SortIndex = ushort.MinValue;
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
        /// 获取或设置是否列包含数据系统自动生成的值。
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


        /// <summary>
        /// 获取或设置排序序列等级 数值越小越靠前。默认值=999
        /// </summary>
        public ushort SortIndex { get { return _sortIndex; } set { _sortIndex = value; } }
        private ushort _sortIndex = 999;
    }
}

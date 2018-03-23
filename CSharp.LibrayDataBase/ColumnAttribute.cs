using System;
using System.Data.SqlTypes;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    #region === CSharp Program Data Type Enum ===
    #endregion

    /// <summary>
    /// 数据表列特性  同一程序不能多个解释。无法继承此类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : AbsBasicAttribute
    {
        /// <summary>
        /// 初始化信息 Microsoft SQL Server 数据库数据类型枚举标识
        /// </summary>
        /// <param name="dbDataType">枚举: 数据库值类型</param>
        public ColumnAttribute(MSQLServerDTParser.DTEnum dtenum) {
            this._dbParser = MSQLServerDTParser.DataTypeBind(dtenum);
        }

        /// <summary>
        /// 获取或设置数据库列的类型。
        /// </summary>
        public AbsDBType DTParser {
            get {
                return !_dbParser.IsObjectNull() ? _dbParser : new MSSDataType.MSSNVarChar();
            }
        }
        private AbsDBType _dbParser = null;
        /// <summary>
        /// 设置字符长度
        /// </summary>
        public ushort CharLength {
            get { return this.DTParser.CharLength; }
            set { this.DTParser.SetCharLength(value); }
        }
        /// <summary>
        /// 设置默认值
        /// </summary>
        public ConstData.ConstEnum DefaultValue {
            get { return this.DTParser.DefaultValue; }
            set { this.DTParser.SetDefaultValue(value); }
        }


        /// <summary>
        /// 设置字段CSharp数据类型-枚举指定。
        /// </summary>
        public MCSharpDTParser.DTEnum CSDTEnum {
            get { return _csDTEnum; }
            set { _csDTEnum = value; }
        }
        private MCSharpDTParser.DTEnum _csDTEnum = MCSharpDTParser.DTEnum.Struct;
        /// <summary>
        /// 获取字段CSharp数据类型
        /// </summary>
        public AbsCSType CSParser {
            get {
                if (CheckData.IsObjectNull(_csParser)) {
                    _csParser = MCSharpDTParser.DataTypeBind(this.CSDTEnum);
                }
                return _csParser;
            }
        }
        private AbsCSType _csParser = null;


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

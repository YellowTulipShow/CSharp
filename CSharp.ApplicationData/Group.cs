using System;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase;
using System.Security.Cryptography;

namespace CSharp.ApplicationData
{
    /// <summary>
    /// 数据模型类: 组别
    /// </summary>
    [Explain(@"组别")]
    [Table]
    public class ModelGroup : AbsModel_ID
    {
        public ModelGroup() { }

        public override string GetTableName() {
            return @"dt_Group";
        }

        #region === Model ===
        /// <summary>
        /// 组别名称
        /// </summary>
        [Explain(@"组别名称")]
        [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 30)]
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = string.Empty;

        /// <summary>
        /// 组别类型枚举
        /// </summary>
        [Explain(@"组别类型")]
        public enum GroupTypeEnum
        {
            /// <summary>
            /// 用户组类型
            /// </summary>
            [Explain(@"用户组")]
            UserGroup = 0,
            /// <summary>
            /// 系统组类型(最高权限组)
            /// </summary>
            [Explain(@"系统组(最高权限组)")]
            SystemGroup = 1
        }
        /// <summary>
        /// 组别类型-(使用枚举 enum GroupTypeEnum 赋值)
        /// </summary>
        [Explain(@"组别类型")]
        [Column(MSQLServerDTParser.DTEnum.Int)]
        public int GroupType {
            get { return _groupType; }
            set {
                if (Enum.IsDefined(typeof(GroupTypeEnum), value)) {
                    _groupType = value;
                }
            }
        }
        private int _groupType = GroupTypeEnum.UserGroup.GetIntValue();
        #endregion
    }
}

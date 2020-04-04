using System;
using System.Linq;
using YTS.Shop.Models;
using YTS.Tools;

namespace YTS.Shop.Tools
{
    /// <summary>
    /// 系统字典表扩展
    /// </summary>
    public class SystemSetTypeExtend
    {
        protected YTSEntityContext db;
        public SystemSetTypeExtend(YTSEntityContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// 枚举类型转为字典设置
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>字典设置</returns>
        public SystemSetType EnumToSystemSetType<T>() where T : Enum
        {
            Type enumType = typeof(T);
            EnumInfo[] enumInfos = EnumInfo.AnalysisList<T>();
            var explain = ExplainAttribute.Extract(enumType);
            int ID = db.SystemSetType
                .Where(a => a.Key == enumType.FullName)
                .Select(a => (int?)a.ID)
                .FirstOrDefault() ?? 0;
            string key = enumType.FullName.Replace('+', '.');
            return new SystemSetType()
            {
                ID = ID,
                ParentID = null,
                Explain = explain?.Text,
                Key = key,
                Value = null,
                Ordinal = 0,
                Remark = null,
                Belows = enumInfos
                    .Select(m =>
                    {
                        var belowKey = string.Join('.', new string[] { key, m.Name });
                        return new SystemSetType()
                        {
                            ID = db.SystemSetType
                                .Where(a => a.Key == belowKey)
                                .Select(a => (int?)a.ID)
                                .FirstOrDefault() ?? 0,
                            ParentID = ID,
                            Explain = m.Explain,
                            Key = belowKey,
                            Value = m.IntValue.ToString(),
                            Ordinal = 0,
                            Remark = null,
                        };
                    })
                    .ToList(),
            };
        }
    }
}

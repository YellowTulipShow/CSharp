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
            string key = enumType.FullName.Replace('+', '.');
            var km = db.SystemSetType
                .Where(a => a.Key == key)
                .Select(a => new
                {
                    ID = a.ID,
                    Ordinal = a.Ordinal,
                })
                .FirstOrDefault() ?? new
                {
                    ID = 0,
                    Ordinal = (db.SystemSetType.Where(a => a.ParentID == null).Max(a => (int?)a.Ordinal) ?? 0) + 1,
                };
            var model = new SystemSetType()
            {
                ID = km.ID,
                ParentID = null,
                Explain = explain?.Text,
                Key = key,
                Value = null,
                Ordinal = km.Ordinal,
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
                            ParentID = km.ID,
                            Explain = m.Explain,
                            Key = belowKey,
                            Value = m.IntValue.ToString(),
                            Ordinal = m.IntValue,
                            Remark = null,
                        };
                    })
                    .ToList(),
            };
            return model;
        }
    }
}

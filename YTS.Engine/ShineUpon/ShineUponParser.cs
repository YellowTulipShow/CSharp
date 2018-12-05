using System;
using System.Collections.Generic;
using System.Reflection;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.ShineUpon
{
    /// <summary>
    /// 映射分析器
    /// </summary>
    /// <typeparam name="I">信息结果类型</typeparam>
    public class ShineUponParser
    {
        /// <summary>
        /// 需要解析的数据类型, 只能设定一次类型
        /// </summary>
        public Type NeedParserType {
            get {
                return _need_parser_type;
            }
            set {
                if (CheckData.IsObjectNull(_need_parser_type)) {
                    _need_parser_type = value;
                }
            }
        }
        private Type _need_parser_type = null;

        /// <summary>
        /// 初始化类型解析器, 需要手动指定解析类型
        /// </summary>
        public ShineUponParser() { }

        /// <summary>
        /// 初始化类型解析器
        /// </summary>
        /// <param name="type">需要解析的数据类型</param>
        public ShineUponParser(Type type) {
            this.NeedParserType = type;
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns>键值对结果</returns>
        public Dictionary<string, ShineUponInfo> GetDictionary() {
            return GetDictionary<ShineUponInfo>();
        }
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <typeparam name="I">指定结果类型解析</typeparam>
        /// <returns>键值对结果</returns>
        public Dictionary<string, I> GetDictionary<I>() where I : ShineUponInfo, new() {
            return GetDictionary<I>(null);
        }
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <typeparam name="I">指定结果类型解析</typeparam>
        /// <param name="AnalyticalItem">对于结果类型进行额外处理</param>
        /// <returns>键值对结果</returns>
        public Dictionary<string, I> GetDictionary<I>(Func<I, I> AnalyticalItem) where I : ShineUponInfo, new() {
            if (CheckData.IsObjectNull(this.NeedParserType)) {
                return new Dictionary<string, I>();
            }
            ShineUponModelAttribute model_attr = ReflexHelp.AttributeFindOnly<ShineUponModelAttribute>(this.NeedParserType, true);
            if (CheckData.IsObjectNull(model_attr)) {
                return new Dictionary<string, I>();
            }
            if (CheckData.IsObjectNull(AnalyticalItem)) {
                AnalyticalItem = i => i;
            }

            Dictionary<string, I> dic = new Dictionary<string, I>();
            PropertyInfo[] protertys = this.NeedParserType.GetProperties();
            foreach (PropertyInfo property in protertys) {
                ShineUponPropertyAttribute spma = ReflexHelp.AttributeFindOnly<ShineUponPropertyAttribute>(property, true);
                if (CheckData.IsObjectNull(spma) || !spma.IsShineUpon) {
                    continue;
                }
                ExplainAttribute attr_explain = ExplainAttribute.Extract(property);
                I info = ReflexHelp.CreateNewObject<I>();
                info.Name = property.Name;
                info.Property = property;
                info.Explain = attr_explain;

                // 钩子: 子类处理解析内容
                info = AnalyticalItem(info);
                if (CheckData.IsObjectNull(info)) {
                    continue;
                }
                dic.Add(info.Name, info);
            }
            return dic;
        }
        
        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="model">数据来源</param>
        /// <returns>键值数据</returns>
        public KeyObject GetValue_KeyObject(ShineUponInfo info, AbsShineUpon model) {
            if (CheckData.IsObjectNull(info) || CheckData.IsObjectNull(model)) {
                return null;
            }
            object ov = info.Property.GetValue(model, null);
            return new KeyObject(info.Property.Name, ov);
        }
        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="model">数据来源</param>
        /// <returns>键值数据</returns>
        public KeyString GetValue_KeyString(ShineUponInfo info, AbsShineUpon model) {
            KeyObject ko = GetValue_KeyObject(info, model);
            if (CheckData.IsObjectNull(ko)) {
                return null;
            }
            string sv = ConvertTool.ToString(ko.Value);
            return new KeyString(ko.Key, sv);
        }
        /// <summary>
        /// 设置_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="target_model">目标模型</param>
        /// <param name="ov">数据</param>
        /// <returns>目标模型</returns>
        public AbsShineUpon SetValue_Object(ShineUponInfo info, AbsShineUpon target_model, object ov) {
            if (!CheckData.IsObjectNull(ov) && info.Property.CanWrite) {
                Type itype = info.Property.PropertyType;
                object oo = ConvertTool.ToObject(itype, ov);
                info.Property.SetValue(target_model, oo, null);
            }
            return target_model;
        }
    }
}

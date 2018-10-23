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
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="I">信息结果类型</typeparam>
    public class ShineUponParser<M, I>
        where M : AbsShineUpon
        where I : ShineUponInfo
    {
        private Dictionary<string, I> info_dic = null;
        private I[] info_sortAfter = null;

        public ShineUponParser() {
            Analytical();
        }

        #region === Info ===
        public void Analytical() {
            Type modelT = typeof(M);
            ShineUponModelAttribute model_attr = ReflexHelp.AttributeFindOnly<ShineUponModelAttribute>(modelT, true);
            if (CheckData.IsObjectNull(model_attr)) {
                return;
            }

            Dictionary<string, I> dic = new Dictionary<string, I>();
            PropertyInfo[] protertys = modelT.GetProperties();
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
            this.info_dic = dic;
        }
        public virtual I AnalyticalItem(I info) {
            return info;
        }

        public Dictionary<string, I> GetAnalyticalResult() {
            if (CheckData.IsSizeEmpty(this.info_dic)) {
                this.info_dic = new Dictionary<string, I>();
            }
            return this.info_dic;
        }
        public I[] GetSortResult() {
            if (!CheckData.IsSizeEmpty(this.info_sortAfter)) {
                return this.info_sortAfter;
            }
            List<I> list = new List<I>(this.info_dic.Values);
            if (IsNeedSort()) {
                list = SortComparison(list);
            }
            this.info_sortAfter = list.ToArray();
            return this.info_sortAfter;
        }
        #endregion

        #region === Sort ===
        /// <summary>
        /// 钩子: 判断是否需要排序
        /// </summary>
        /// <returns>是否需要布尔值</returns>
        public virtual bool IsNeedSort() {
            return false;
        }

        /// <summary>
        /// 执行排序比较方法
        /// </summary>
        /// <param name="array">需要排序的信息模型列表</param>
        /// <returns>排序完成信息模型列表</returns>
        public virtual List<I> SortComparison(List<I> list) {
            if (CheckData.IsSizeEmpty(list)) {
                return new List<I>();
            }
            return list;
        }
        #endregion

        #region === Data: Get/Set ===
        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="model">数据来源</param>
        /// <returns>键值数据</returns>
        public KeyObject GetModelValue(I info, M model) {
            if (CheckData.IsObjectNull(info) || CheckData.IsObjectNull(model)) {
                return null;
            }
            object value = info.Property.GetValue(model, null);
            return new KeyObject() {
                Key = info.Property.Name,
                Value = value,
            };
        }

        /// <summary>
        /// 设置_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="targetModel">目标模型</param>
        /// <param name="value">数据</param>
        /// <returns>目标模型</returns>
        public M SetModelValue(I info, M targetModel, object value) {
            if (!CheckData.IsObjectNull(value) && info.Property.CanWrite) {
                info.Property.SetValue(targetModel, value, null);
            }
            return targetModel;
        }
        #endregion

        /// <summary>
        /// 数据基础转化
        /// </summary>
        /// <param name="parser_info"></param>
        /// <param name="field_value"></param>
        /// <returns></returns>
        public object DataBasicConvert(I parser_info, string field_value) {
            field_value = ConvertTool.ObjToString(field_value);
            Type detype = parser_info.Property.PropertyType;
            if (CheckData.IsTypeEqual<int>(detype) || CheckData.IsTypeEqual<Enum>(detype, true)) {
                return ConvertTool.ObjToInt(field_value, default(int));
            }
            if (CheckData.IsTypeEqual<float>(detype) || CheckData.IsTypeEqual<double>(detype)) {
                return ConvertTool.ObjToFloat(field_value, default(float));
            }
            if (CheckData.IsTypeEqual<DateTime>(detype)) {
                return ConvertTool.ObjToDateTime(field_value, default(DateTime));
            }
            if (CheckData.IsTypeEqual<bool>(detype)) {
                return ConvertTool.ObjToBool(field_value, default(bool));
            }
            return field_value;
        }
    }
}

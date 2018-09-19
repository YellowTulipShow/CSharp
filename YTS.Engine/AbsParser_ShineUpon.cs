using System;
using System.Collections.Generic;
using System.Reflection;
using YTS.Model;
using YTS.Model.Attribute;
using YTS.Tools;

namespace YTS.Engine
{
    /// <summary>
    /// 抽象-映射分析器
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="I"></typeparam>
    public abstract class AbsParser_ShineUpon<M, I> where M : Model.AbsShineUpon where I: AbsInfo_ShineUpon
    {
        /// <summary>
        /// 列字段列表数组
        /// </summary>
        private I[] __infos__ = null;

        public AbsParser_ShineUpon() {
            this.__infos__ = Analytical();
            if (IsNeedSort()) {
                this.__infos__ = SortComparison(this.__infos__);
            }
        }

        #region === Info ===
        /// <summary>
        /// 分析映射模型
        /// </summary>
        /// <returns>映射模型的列信息集合</returns>
        public virtual I[] Analytical() {
            Type modelT = typeof(M);
            ShineUponModelAttribute model_attr = ReflexHelp.AttributeFindOnly<ShineUponModelAttribute>(modelT, true);
            if (CheckData.IsObjectNull(model_attr)) {
                return new I[] { };
            }
            List<I> colms = new List<I>();
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
                colms.Add(info);
            }
            return colms.ToArray();
        }

        /// <summary>
        /// 获得列信息 全部
        /// </summary>
        public I[] GetColumn_ALL() {
            return this.__infos__;
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
        public virtual I[] SortComparison(I[] array) {
            return array;
        }
        #endregion

        #region === Data: Get/Set ===
        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="info">行信息</param>
        /// <param name="model">数据来源</param>
        /// <returns>键值数据</returns>
        public virtual KeyObject GetModelValue(I info, M model) {
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
        public virtual M SetModelValue(I info, M targetModel, object value) {
            if (!CheckData.IsObjectNull(value) && info.Property.CanWrite) {
                info.Property.SetValue(targetModel, value, null);
            }
            return targetModel;
        }
        #endregion
    }
}

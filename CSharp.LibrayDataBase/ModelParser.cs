using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 模型解析器
    /// </summary>
    public class ColumnModelParser<M> where M : AbsModelNull
    {
        /// <summary>
        /// 列字段列表数组
        /// </summary>
        public ColumnItemModel[] ColumnInfoArray { get { return _columnInfoArray; } }
        private readonly ColumnItemModel[] _columnInfoArray = null;

        public ColumnModelParser() {
            this._columnInfoArray = Analysis.PropertyColumns<M>();
        }

        /// <summary>
        /// 创建默认模型实例
        /// </summary>
        public M CreateDefaultModel() {
            return System.Activator.CreateInstance<M>();
        }
        
        /// <summary>
        /// 提取'行'数据
        /// </summary>
        /// <param name="c">行信息</param>
        /// <param name="sourceModel">数据来源</param>
        /// <returns>选项模型</returns>
        public KeyValueModel ExtractValue(ColumnItemModel c, M sourceModel) {
            return new KeyValueModel() {
                Key = c.Property.Name,
                Value = c.Attribute.DbType.PrintSaveValue(c.Property.GetValue(sourceModel, null)),
            };
        }

        /// <summary>
        /// 填充'模型'数据
        /// </summary>
        /// <param name="c">行信息</param>
        /// <param name="targetModel">目标模型</param>
        /// <param name="value">数据</param>
        /// <returns>目标模型</returns>
        public M FillValue(ColumnItemModel c, M targetModel, object value) {
            if (!CheckData.IsObjectNull(value) && c.Property.CanWrite) {
                value = c.Attribute.CsType.ToModelValue(c, value);
                c.Property.SetValue(targetModel, value, null);
            }
            return targetModel;
        }
    }
}

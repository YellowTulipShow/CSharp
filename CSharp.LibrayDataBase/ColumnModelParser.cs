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

        /** 数据转化六部曲: 
         * 设置模型数据: 数据源 @1=> DTParser @2=> CSParser @3=> 模型
         * 获取模型数据: 数据源 <=@4 DTParser <=@5 CSParser <=@6 模型
         */

        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="colmodel">行信息</param>
        /// <param name="sourceModel">数据来源</param>
        /// <returns>键值数据</returns>
        public KeyValueModel GetModelValue(ColumnItemModel colmodel, M sourceModel) {
            if (CheckData.IsObjectNull(colmodel) || CheckData.IsObjectNull(sourceModel)) {
                return null;
            }
            return GetModelValue(colmodel, colmodel.Property.GetValue(sourceModel, null));
        }
        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="colmodel">行信息</param>
        /// <param name="srouceValue">数据来源</param>
        /// <returns>键值数据</returns>
        public KeyValueModel GetModelValue(ColumnItemModel colmodel, object srouceValue) {
            if (CheckData.IsObjectNull(colmodel) || CheckData.IsObjectNull(srouceValue)) {
                return null;
            }
            object sour = null;
            if (colmodel.Attribute.IsOnlySetToDefaultValue) {
                sour = colmodel.Attribute.DTParser.GetDefaultValueString();
            } else {
                sour = srouceValue; // @6
                sour = colmodel.Attribute.CSParser.OutputConvert(sour, colmodel); // @5
                sour = colmodel.Attribute.DTParser.InputConvert(sour, colmodel); // @4
            }
            return new KeyValueModel() {
                Key = colmodel.Property.Name,
                Value = ConvertTool.ObjToString(sour),
            };
        }

        /// <summary>
        /// 设置_模型_数据
        /// </summary>
        /// <param name="colmodel">行信息</param>
        /// <param name="targetModel">目标模型</param>
        /// <param name="value">数据</param>
        /// <returns>目标模型</returns>
        public M SetModelValue(ColumnItemModel colmodel, M targetModel, object value) { // @1
            if (!CheckData.IsObjectNull(value) && colmodel.Property.CanWrite) {
                value = colmodel.Attribute.DTParser.OutputConvert(value, colmodel); // @2
                value = colmodel.Attribute.CSParser.InputConvert(value, colmodel); // @3
                colmodel.Property.SetValue(targetModel, value, null);
            }
            return targetModel;
        }
    }
}

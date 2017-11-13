using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 反射操作帮助类
    /// </summary>
    public static class ReflexHelper
    {
        /// <summary>
        /// 获取指定 "内容" 名称 用法: ***.Name(() => new ModelClass().ID)
        /// </summary>
        public static String Name<T>(Expression<Func<T>> memberExpression) {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

        /// <summary>
        /// 获得 "Object" 对象公共属性 名称列表
        /// </summary>
        public static String[] AttributeNames(Object obj) {
            List<String> names = new List<String>();
            Dictionary<String, String> attrKeyValues = AttributeKeyValues(obj);
            foreach (KeyValuePair<String, String> item in attrKeyValues) {
                names.Add(item.Key);
            }
            return names.ToArray();
        }

        /// <summary>
        /// 获得 "Object" 对象公共属性 及其值
        /// </summary>
        public static Dictionary<String, String> AttributeKeyValues(Object obj) {
            Dictionary<String, String> dicArray = new Dictionary<String, String>();
            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pis) {
                dicArray.Add(pi.Name, pi.GetValue(obj, null).ToString());
            }
            return dicArray;
        }

        /// <summary>
        /// 获得 "Object" 对象公共字段 及其值
        /// </summary>
        public static Dictionary<String, String> FieldKeyValues(Object obj) {
            Dictionary<String, String> dicArray = new Dictionary<String, String>();
            FieldInfo[] fis = obj.GetType().GetFields();
            foreach (FieldInfo fi in fis) {
                dicArray.Add(fi.Name, fi.GetValue(obj).ToString());
            }
            return dicArray;
        }

        /// <summary>
        /// 克隆 对象 公共属性属性值 (但克隆DataGridView 需调用CloneDataGridView()方法)
        /// </summary>
        public static T CloneAllAttribute<T>(T obj) where T : class {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            T model = (T)type.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, obj, null);
            foreach (PropertyInfo pi in properties) {
                if (pi.CanWrite) {
                    object value = pi.GetValue(obj, null);
                    pi.SetValue(model, value, null);
                }
            }
            return model;
        }

        /// <summary>
        /// 克隆DataGridView对象
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static DataGridView CloneDataGridView(DataGridView dgv) {
            try {
                DataGridView ResultDGV = new DataGridView();
                ResultDGV.ColumnHeadersDefaultCellStyle = dgv.ColumnHeadersDefaultCellStyle.Clone();
                DataGridViewCellStyle dtgvdcs = dgv.RowsDefaultCellStyle.Clone();
                dtgvdcs.BackColor = dgv.DefaultCellStyle.BackColor;
                dtgvdcs.ForeColor = dgv.DefaultCellStyle.ForeColor;
                dtgvdcs.Font = dgv.DefaultCellStyle.Font;
                ResultDGV.RowsDefaultCellStyle = dtgvdcs;
                ResultDGV.AlternatingRowsDefaultCellStyle = dgv.AlternatingRowsDefaultCellStyle.Clone();
                for (int i = 0; i < dgv.Columns.Count; i++) {
                    DataGridViewColumn DTGVC = dgv.Columns[i].Clone() as DataGridViewColumn;
                    DTGVC.DisplayIndex = dgv.Columns[i].DisplayIndex;
                    if (DTGVC.CellType == null) {
                        DTGVC.CellTemplate = new DataGridViewTextBoxCell();
                        ResultDGV.Columns.Add(DTGVC);
                    } else {
                        ResultDGV.Columns.Add(DTGVC);
                    }
                }
                foreach (DataGridViewRow var in dgv.Rows) {
                    DataGridViewRow Dtgvr = var.Clone() as DataGridViewRow;
                    Dtgvr.DefaultCellStyle = var.DefaultCellStyle.Clone();
                    for (int i = 0; i < var.Cells.Count; i++) {
                        Dtgvr.Cells[i].Value = var.Cells[i].Value;
                    }
                    if (var.Index % 2 == 0)
                        Dtgvr.DefaultCellStyle.BackColor = ResultDGV.RowsDefaultCellStyle.BackColor;
                    ResultDGV.Rows.Add(Dtgvr);
                }
                return ResultDGV;
            } finally {
            }
        }
    }
}

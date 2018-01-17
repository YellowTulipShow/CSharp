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
        /// 查找指定的 Attribute '特性' 内容列表 默认不查找继承链
        /// </summary>
        /// <typeparam name="A">指定的 Attribute '特性'</typeparam>
        /// <param name="memberInfo">元数据</param>
        /// <returns></returns>
        public static A[] FindAttributes<A>(MemberInfo memberInfo) where A : System.Attribute {
            object[] attrs = memberInfo.GetCustomAttributes(typeof(A), false);
            if (CheckData.IsSizeEmpty(attrs))
                return new A[] { };
            List<A> rl = new List<A>();
            foreach (object item in attrs) {
                try { rl.Add((A)item); } catch (Exception) { }
            }
            return rl.ToArray();
        }
        /// <summary>
        /// 查找指定的 Attribute '特性' 内容对象 默认不查找继承链
        /// </summary>
        /// <typeparam name="A">指定的 Attribute '特性'</typeparam>
        /// <param name="memberInfo">元数据</param>
        /// <returns></returns>
        public static A FindAttributeOnly<A>(MemberInfo memberInfo) where A : System.Attribute {
            object[] attrs = memberInfo.GetCustomAttributes(typeof(A), false);
            try {
                return CheckData.IsSizeEmpty(attrs) ? null : (A)attrs[0];
            } catch (Exception) {
                return null;
            }
        }

        #region === Clone Data ===
        /// <summary>
        /// 克隆 对象 公共属性属性值 (但克隆DataGridView 需调用CloneDataGridView()方法)
        /// </summary>
        public static T CloneProperties<T>(T obj) where T : class {
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
        #endregion
    }
}

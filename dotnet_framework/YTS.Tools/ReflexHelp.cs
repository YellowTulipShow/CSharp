using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace YTS.Tools
{
    /// <summary>
    /// 反射操作帮助类
    /// </summary>
    public static class ReflexHelp
    {
        #region ====== Lambda Name: ======
        /// <summary>
        /// 获取指定 "内容" 名称 用法: ***.Name(() => new ModelClass().ID)
        /// </summary>
        public static string Name<T>(Expression<Func<T>> memberExpression) {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
        #endregion

        #region ====== Attributes: ======
        /// <summary>
        /// 查找指定的 Attribute '特性' 内容列表
        /// </summary>
        /// <typeparam name="A">指定的 Attribute '特性'</typeparam>
        /// <param name="memberInfo">元数据</param>
        /// <param name="isFindInherit">是否查找继承链, 默认不查找</param>
        /// <returns>返回指定'特性'类型列表结果</returns>
        public static A[] AttributeFindALL<A>(MemberInfo memberInfo, bool isFindInherit = false) where A : System.Attribute {
            object[] attrs = memberInfo.GetCustomAttributes(typeof(A), isFindInherit);
            return ConvertTool.ListConvertType(attrs, o => o as A);
        }

        /// <summary>
        /// 查找指定的 Attribute '特性' 内容对象
        /// </summary>
        /// <typeparam name="A">指定的 Attribute '特性'</typeparam>
        /// <param name="memberInfo">元数据</param>
        /// <param name="isFindInherit">是否查找继承链, 默认不查找</param>
        /// <returns>返回指定'特性'类型的第一个结果</returns>
        public static A AttributeFindOnly<A>(MemberInfo memberInfo, bool isFindInherit = false) where A : System.Attribute {
            A[] attrs = AttributeFindALL<A>(memberInfo, isFindInherit);
            return CheckData.IsSizeEmpty(attrs) ? null : attrs[0];
        }
        #endregion

        #region ====== Clone: ======
        /* 忘记添加引用地址了... */

        /// <summary>
        /// 克隆 对象公共属性属性值 (但克隆DataGridView 需调用CloneDataGridView()方法)
        /// </summary>
        public static T CloneProperties<T>(T obj) where T : class {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            T model = (T)type.InvokeMember(string.Empty, BindingFlags.CreateInstance, null, obj, null);
            foreach (PropertyInfo pi in properties) {
                if (pi.CanWrite) {
                    object value = pi.GetValue(obj, null);
                    pi.SetValue(model, value, null);
                }
            }
            return model;
        }
        /// <summary>
        /// 克隆 DataGridView对象
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static DataGridView CloneDataGridView(this DataGridView dgv) {
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
        /// <summary>
        /// 克隆数据时间
        /// </summary>
        public static DateTime CloneDateTime(DateTime sourcetime) {
            return new DateTime(sourcetime.Year, sourcetime.Month, sourcetime.Day, sourcetime.Hour, sourcetime.Minute, sourcetime.Second, sourcetime.Millisecond, sourcetime.Kind);
        }
        #endregion

        #region ====== New Instantiation: ======
        /// <summary>
        /// 创建初始化一个含有无参数构造方法的实例类对象
        /// </summary>
        /// <typeparam name="C">实例类对象类型</typeparam>
        /// <returns>实例对象, 如果发生错误则返回Null</returns>
        public static C CreateNewObject<C>() where C : class {
            try {
                return System.Activator.CreateInstance<C>();
            } catch (System.MissingMethodException) {
                return null;
            }
        }
        #endregion
    }
}

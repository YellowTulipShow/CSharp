using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Tools;

namespace YTS.Web.UI
{
    /// <summary>
    /// 管理页面 基础 推广页面
    /// </summary>
    public abstract class ManagePageBasicsSpread : ManagePage
    {
        #region ====== Override Class ManagePage Function Event 重写类管理页面功能事件 ======
        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(string nav_name, DTEnums.ActionEnum action_type) {
            this.ChkAdminLevel(nav_name, action_type.ToString());
        }
        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(DTEnums.ActionEnum action_type, string remark) {
            return this.AddAdminLog(action_type.ToString(), remark);
        }
        #endregion

        #region ====== Spread Basics Method ======
        /// <summary>
        /// 推广 时间 格式
        /// </summary>
        protected string SpreadTimeFormat_SECOND() {
            return DTKeys.TABLE_DATETIME_FORMAT_SECOND;
        }

        /// <summary>
        /// 输出HTML代码: 将内容套上红色
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected string PageFormat_AddRed(string content) {
            return String.Format("<span style=\"color:red;\">{0}</span>", content);
        }

        /// <summary>
        /// 最后一个字符
        /// </summary>
        protected String LastChar(object obj) {
            if (CheckData.IsObjectNull(obj)) {
                return String.Empty;
            }
            String str = obj.ToString();
            return str.Substring(str.Length - 1);
        }
        #endregion
    }
}

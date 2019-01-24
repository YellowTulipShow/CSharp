using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using YTS.DAL;
using YTS.Tools;
namespace YTS.Web.UI
{
    public abstract class ManagePageTableShowListType<T> : ManagePageBasicsSpread where T : Model.AbsTableModel
    {
        #region ====== Variable Data Region ======
        protected int _default_page_size = 10;
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected Repeater rptList;
        protected TextBox txtPageNum;
        protected HtmlGenericControl PageContent;
        protected BasicsPageDataModel BapdModel;
        protected ITableBasicFunction<T> BapdBLL;
        #endregion

        protected class BasicsPageDataModel : AbsBasicDataModel
        {
            /// <summary>
            /// 用于记录浏览的页数(不要随意更改其值)
            /// </summary>
            public int page { get { return _page; } set { _page = value; } }
            private int _page = 1;

            public int ManagerID { get { return _managerID; } set { _managerID = value; } }
            private int _managerID = 0;
        }

        #region ====== Basic page processing methods 基本页面处理方法 ======

        #region Basics Function
        /// <summary>
        /// 页面加载事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) {
            InitCreateBasicsComponentAndObject(out rptList, out txtPageNum, out PageContent, out BapdBLL);
            this.BapdModel = GetBeforeSetWhereDataModel();
            this.page = GetPageSignNo();

            this.pageSize = GetPageSize(this._default_page_size, Get_CTCookie_Page_Size_Name()); //每页数量
            if (!Page.IsPostBack) {
                ChkAdminLevel(Get_CT_Nav_Name(), DTEnums.ActionEnum.View); //检查权限

                InitPageComponentTemplateValues();

                this.BapdModel = InitIsPostBackBasicsPageDataModel(this.BapdModel);
                SetPageControlValue(this.BapdModel);
                RptBind(GetSetSQLWhereLastValueString(this.BapdModel), GetSetSQLOrderByString());
            }
        }
        protected int GetPageSignNo() {
            int model_value = this.BapdModel.page;
            int query_value = DTRequest.GetQueryInt("page", 0);
            if (model_value == query_value) {
                return model_value;
            }
            if (query_value > 0) {
                model_value = query_value;
            }
            this.BapdModel.page = model_value;
            return this.BapdModel.page;
        }
        /// <summary>
        /// 绑定数据视图
        /// </summary>
        protected void RptBind(string _strWhere, string _orderby) {
            DataSourceBind(_strWhere, _orderby);

            // 绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = GetAccessThisPageURLLink(Get_CT_ThisPage_Url(), this.BapdModel, "page=__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }

        /// <summary>
        /// 数据源绑定
        /// </summary>
        protected virtual void DataSourceBind(string _strWhere, string _orderby) {
            this.rptList.DataSource = BapdBLL.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();
        }
        #endregion

        #region Yes Override Function
        /// <summary>
        /// 初始化回发基础页面数据模型
        /// </summary>
        protected virtual BasicsPageDataModel InitIsPostBackBasicsPageDataModel(BasicsPageDataModel Obj_DaModel) {
            return Obj_DaModel;
        }
        /// <summary>
        /// 普通管理员 的 额外查询条件
        /// </summary>
        /// <param name="ManagerID">既然是普通管理员,那就需要获得是哪一个管理员</param>
        protected virtual string UsualManagerSQLWhere(BasicsPageDataModel Obj_DaModel, int ManagerID) {
            return String.Empty;
        }
        /// <summary>
        /// 超级管理员 的 额外查询条件
        /// </summary>
        protected virtual string SuperManagerSQLWhere(BasicsPageDataModel Obj_DaModel) {
            return String.Empty;
        }
        /// <summary>
        /// 传递页面查询参数值
        /// </summary>
        protected virtual string GetSetSQLWhereLastValueString(BasicsPageDataModel Obj_DaModel) {
            StringBuilder SQLWhere = new StringBuilder();
            SQLWhere.Append(" id >= 0 " + CombSqlTxt(Obj_DaModel));
            Model.manager adminUserInfo = GetAdminInfo();

            string string_usualManSqlWhere = UsualManagerSQLWhere(Obj_DaModel, adminUserInfo.id);
            if (!CheckData.IsStringNull(string_usualManSqlWhere) && adminUserInfo.role_id != 1)
                SQLWhere.Append(string_usualManSqlWhere);

            string string_superManSqlWhere = SuperManagerSQLWhere(Obj_DaModel);
            if (!CheckData.IsStringNull(string_superManSqlWhere) && adminUserInfo.role_id == 1)
                SQLWhere.Append(string_superManSqlWhere);

            return SQLWhere.ToString();
        }
        /// <summary>
        /// 获得设置好的SQL排序字符串
        /// </summary>
        protected virtual string GetSetSQLOrderByString() {
            return @" id desc ";
        }
        #endregion

        #region Control Event Click
        /// <summary>
        /// 关健字查询
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e) {
            ExecuteSearch();
        }
        protected void ExecuteSearch() {
            ChkAdminLevel(Get_CT_Nav_Name(), DTEnums.ActionEnum.Show); //检查权限
            this.BapdModel = GetPageControlValue();
            Response.Redirect(GetAccessThisPageURLLink(Get_CT_ThisPage_Url(), this.BapdModel));
        }
        /// <summary>
        /// 清空查询信息
        /// </summary>
        protected void lbtnEmpty_Click(object sender, EventArgs e) {
            Response.Redirect(GetAccessThisPageURLLink(Get_CT_ThisPage_Url(), new BasicsPageDataModel()));
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e) {
            ChkAdminLevel(Get_CT_Nav_Name(), DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0, errorCount = 0;

            int[] selectedIDs = GetTableTrUserSelectDataIDs(true);
            foreach (int id in selectedIDs) {
                if (BapdBLL.Delete(id)) {
                    sucCount++;
                } else {
                    errorCount++;
                }
            }

            string msg = String.Format("删除记录成功{0}条，失败{1}条！", sucCount, errorCount);
            string url = GetAccessThisPageURLLink(Get_CT_ThisPage_Url(), this.BapdModel);
            AddAdminLog(DTEnums.ActionEnum.Delete, msg); //记录日志
            JscriptMsg(msg, url);
        }
        /// <summary>
        /// 批量作废记录
        /// </summary>
        protected void btnInvalid_Click(object sender, EventArgs e) {
            ChkAdminLevel(Get_CT_Nav_Name(), DTEnums.ActionEnum.Invalid); //检查权限
            SetUpdateInvalidInfo(1);
        }
        /// <summary>
        /// 批量取消作废记录
        /// </summary>
        protected void btnCancelInvalid_Click(object sender, EventArgs e) {
            ChkAdminLevel(Get_CT_Nav_Name(), DTEnums.ActionEnum.Invalid); //检查权限
            SetUpdateInvalidInfo(0);
        }
        #endregion

        #region Control Event TextChanged
        /// <summary>
        /// 设置分页数量
        /// </summary>
        protected void txtPageNum_TextChanged(object sender, EventArgs e) {
            PageNumChangContentEvent(this.txtPageNum.Text.Trim(), this.BapdModel, Get_CT_ThisPage_Url(), Get_CTCookie_Page_Size_Name());
        }
        #endregion

        #region Control DropDownList Event SelectedIndexChanged
        /// <summary>
        /// 下拉控件选择触发事件
        /// </summary>
        protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e) {
            ChkAdminLevel(Get_CT_Nav_Name(), DTEnums.ActionEnum.View); //检查权限
            this.BapdModel = GetPageControlValue();
            RptBind(GetSetSQLWhereLastValueString(GetPageControlValue()), GetSetSQLOrderByString());
        }
        #endregion

        #region Function Check Judgment(If)
        /// <summary>
        /// 检查是否是最高权限者
        /// </summary>
        protected bool IsAdministrator() {
            Model.manager managerModel = GetAdminInfo();
            if (managerModel.role_id == 1) {
                return true;
            }
            return false;
        }
        #endregion

        #region Function Check Set Info
        /// <summary>
        /// 设置更新关于作废记录信息
        /// </summary>
        private void SetUpdateInvalidInfo(int status) {
            ChkAdminLevel(Get_CT_Nav_Name(), DTEnums.ActionEnum.Invalid); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            int[] selectedIDs = GetTableTrUserSelectDataIDs(true);
            foreach (int id in selectedIDs) {
                if (this.BapdBLL.UpdateField(id, "isinvalid = " + status.ToString())) {
                    sucCount += 1;
                } else {
                    errorCount += 1;
                }
            }

            AddAdminLog(DTEnums.ActionEnum.Invalid, "执行关于作废记录成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            string pageUrl = GetAccessThisPageURLLink(Get_CT_ThisPage_Url(), this.BapdModel);
            JscriptMsg("执行关于作废记录成功" + sucCount + "条，失败" + errorCount + "条！", pageUrl);
        }
        /// <summary>
        /// 设置分页数量
        /// </summary>
        protected void PageNumChangContentEvent(string txtPageNumStr, AbsBasicDataModel daModel, string CT_ThisPage_Url, string CT_Cookie_Page_Size_Name) {
            int _pagesize;
            if (int.TryParse(txtPageNumStr, out _pagesize)) {
                if (_pagesize > 0) {
                    Utils.WriteCookie(CT_Cookie_Page_Size_Name, "YiMeiPage", _pagesize.ToString(), 14400);
                }
            }

            Response.Redirect(GetAccessThisPageURLLink(CT_ThisPage_Url, daModel));
        }
        #endregion

        #region Function Check Get Info
        /// <summary>
        /// 获得 之前设置的条件 数信息
        /// </summary>
        protected BasicsPageDataModel GetBeforeSetWhereDataModel() {
            string keyname = String.Format("{0}_model_value", Get_CT_Nav_Name());

            BasicsPageDataModel model = Session[keyname] as BasicsPageDataModel;
            if (model != null) {
                return model;
            }
            return new BasicsPageDataModel();
        }
        /// <summary>
        /// 获取管理员用户名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetManagerUserName(int id) {
            string defaultstring = string.Format("错误值: ({0})", id);
            Model.manager manModel = new BLL.manager().GetModel(id);
            if (!CheckData.IsObjectNull(manModel)) {
                return string.Format("{0}({1})({2})", manModel.real_name, manModel.user_name, manModel.id);
            }
            return defaultstring;
        }
        /// <summary>
        /// 获得页面大小
        /// </summary>
        protected int GetPageSize(int _default_size, string CTCookie_Page_Size_Name) {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie(CTCookie_Page_Size_Name, "YiMeiPage"), out _pagesize)) {
                if (_pagesize > 0) {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        /// <summary>
        /// 获取当前页面URL链接
        /// </summary>
        protected string GetAccessThisPageURLLink(string pageUrl, AbsBasicDataModel daModel) {
            return GetAccessThisPageURLLink(pageUrl, daModel, null);
        }
        /// <summary>
        /// 获取当前页面URL链接
        /// </summary>
        protected string GetAccessThisPageURLLink(string pageUrl, AbsBasicDataModel daModel, string AddParamsStr) {
            //string paramsStr = URLHelper.ConvertDictionaryToString(ReflexHelp.GetObjectAttribute(daModel));
            //string ResuPageUrl = string.Empty;
            //if (!string.IsNullOrEmpty(AddParamsStr) && !string.IsNullOrWhiteSpace(AddParamsStr))
            //{
            //    ResuPageUrl = String.Format("{0}?{1}&{2}", pageUrl, AddParamsStr, paramsStr);
            //}
            //else
            //{
            //    ResuPageUrl = String.Format("{0}?{1}", pageUrl, paramsStr);
            //}


            string keyname = String.Format("{0}_model_value", Get_CT_Nav_Name());
            Session[keyname] = daModel;
            string ResuPageUrl = string.Empty;
            ResuPageUrl = String.Format("{0}?{1}", pageUrl, AddParamsStr);

            return ResuPageUrl;
        }
        /// <summary>
        /// 获得 表中 TR行 选择的数据ID 列值
        /// </summary>
        /// <param name="isSelected">是否被选中</param>
        protected int[] GetTableTrUserSelectDataIDs(bool isSelected) {
            List<int> ids = new List<int>();
            for (int i = 0; i < rptList.Items.Count; i++) {
                int id = ConvertTool.ToInt(((HiddenField)rptList.Items[i].FindControl("hidId")).Value, 0);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked == isSelected) {
                    ids.Add(id);
                }
            }
            return ids.ToArray();
        }
        #endregion

        /* ========================Down Function Need Override======================== */
        #region Function Need Override
        public abstract string Get_CT_ThisPage_Url();
        public abstract string Get_CT_Nav_Name();
        public abstract string Get_CTCookie_Page_Size_Name();
        /// <summary>
        /// 初始化基础控件对象
        /// </summary>
        protected abstract void InitCreateBasicsComponentAndObject(out Repeater rptList, out TextBox txtPageNum, out HtmlGenericControl PageContent, out ITableBasicFunction<T> bll);
        /// <summary>
        /// 初始化页面控件模板值
        /// </summary>
        protected abstract void InitPageComponentTemplateValues();
        /// <summary>
        /// 设置页面控件值
        /// </summary>
        protected abstract void SetPageControlValue(BasicsPageDataModel Obj_DaModel);
        /// <summary>
        /// 获得页面控件值
        /// </summary>
        protected abstract BasicsPageDataModel GetPageControlValue();
        /// <summary>
        /// 组合字符串
        /// </summary>
        protected abstract string CombSqlTxt(BasicsPageDataModel Obj_DaModel);
        #endregion
        /* ========================       Override End        ======================== */
        #endregion

        #region ====== Method for decorating pages 页面装饰方法 ======
        /// <summary>
        /// 输出HTML代码: 返回图片内容
        /// </summary>
        protected string imgResult(string sourceImgUrl) {
            StringBuilder resultStr = new StringBuilder();

            if (!FileHelp.FileExists(sourceImgUrl)) {
                return String.Format("文件不存在<br/>{0}", sourceImgUrl);
            }
            resultStr.Append(String.Format("<a href=\"{0}\" target=\"_blank\">", sourceImgUrl));
            resultStr.Append(String.Format("<img class=\"img-box\" src=\"{0}\" />", sourceImgUrl));
            resultStr.Append("</a>");
            return resultStr.ToString();
        }

        /// <summary>
        /// 输出HTML代码: 将内容套上 标题样式: 黑色,加粗,字号变为1.4倍
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected string PageFormat_Titel(string content) {
            return String.Format("<span style=\"color:#000;font-weight: bold;font-size: 1.4em;\">{0}</span>", content);
        }

        /// <summary>
        /// 输出HTML代码: 给内容套上区域模块
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected string PageFormat_Region(string content) {
            return PageFormat_Region(new string[] { content });
        }
        /// <summary>
        /// 输出HTML代码: 给内容套上区域模块
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string PageFormat_Region(string[] list) {
            string[] formatlist = ConvertTool.ListConvertType(list, str => string.Format("<span>{0}</span>", str));
            return string.Format("<div class=\"l-btns\">{0}</div>", ConvertTool.ToString(formatlist, PAGE_OUTPUT_LINE_BREAK));
        }

        /// <summary>
        /// 显示 Double 类型 乘100后显示成百分比的数据
        /// </summary>
        /// <param name="objValue">数据源</param>
        /// <param name="decimals">取小数点后 N 位</param>
        protected string ShowDoudlePercentage(object objValue, int decimals) {
            double num = Convert.ToDouble(objValue);
            num *= 100;
            return ShowDoudleNormal(num, decimals);
        }

        /// <summary>
        /// 显示 Double 类型的正常数据
        /// </summary>
        /// <param name="objValue">数据源</param>
        /// <param name="decimals">取小数点后 N 位</param>
        protected string ShowDoudleNormal(object objValue, int decimals) {
            double num = ConvertTool.ToFloat(objValue, 0);
            return Math.Round(num, decimals).ToString();
        }

        #endregion
    }
}

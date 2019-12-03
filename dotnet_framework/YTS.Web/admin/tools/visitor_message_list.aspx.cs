using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using YTS.DAL;
using YTS.Engine.DataBase.MSQLServer;
using YTS.Tools;
using YTS.Web.UI;

namespace YTS.Web.admin.tools
{
    public partial class visitor_message_list : ManagePageTableShowListType<Model.visitor_message>
    {
        protected readonly BLL.visitor_message BLL_vismsg = new BLL.visitor_message();

        protected class DataModel : BasicsPageDataModel
        {
            public string LikeSelectValue { get { return _LikeSelectValue; } set { _LikeSelectValue = value; } }
            private string _LikeSelectValue = string.Empty;

            public string Name { get { return _Name; } set { _Name = value; } }
            private string _Name = string.Empty;

            public string Time_Start { get { return _time_Start; } set { _time_Start = value; } }
            private string _time_Start = string.Empty;

            public string Time_End { get { return _time_End; } set { _time_End = value; } }
            private string _time_End = string.Empty;
        }

        public override string Get_CT_ThisPage_Url() {
            return @"visitor_message_list.aspx";
        }
        public override string Get_CT_Nav_Name() {
            return @"tools_visitor_message";
        }
        public override string Get_CTCookie_Page_Size_Name() {
            return @"visitor_message_list_page_size";
        }

        /// <summary>
        /// 初始化基础控件对象
        /// </summary>
        protected override void InitCreateBasicsComponentAndObject(out Repeater rptList, out TextBox txtPageNum, out HtmlGenericControl PageContent, out ITableBasicFunction<Model.visitor_message> bll) {
            rptList = this.rptList;
            txtPageNum = this.txtPageNum;
            PageContent = this.PageContent;
            bll = (ITableBasicFunction<Model.visitor_message>)new BLL.visitor_message();
            this.btnDelete.Click += btnDelete_Click;
            this.lbtnEmpty.Click += lbtnEmpty_Click;
            this.lbtnSearch.Click += btnSearch_Click;
            this.txtPageNum.TextChanged += txtPageNum_TextChanged;
        }

        /// <summary>
        /// 初始化页面控件模板值
        /// </summary>
        protected override void InitPageComponentTemplateValues() {
        }

        /// <summary>
        /// 设置页面控件值
        /// </summary>
        protected override void SetPageControlValue(BasicsPageDataModel Obj_DaModel) {
            DataModel daModel = new DataModel();
            try { daModel = (DataModel)Obj_DaModel; } catch (Exception) { }

            txt_Name.Text = daModel.Name;
            txt_StartTime.Text = daModel.Time_Start;
            txt_EndTime.Text = daModel.Time_End;
            this.txt_LikeSelect.Text = daModel.LikeSelectValue;
        }

        /// <summary>
        /// 获得页面控件值
        /// </summary>
        protected override BasicsPageDataModel GetPageControlValue() {
            DataModel daModel = new DataModel();

            daModel.Name = txt_Name.Text;
            daModel.Time_Start = txt_StartTime.Text;
            daModel.Time_End = txt_EndTime.Text;
            daModel.LikeSelectValue = this.txt_LikeSelect.Text.ToString().Trim();

            return daModel;
        }

        /// <summary>
        /// 组合字符串
        /// </summary>
        protected override string CombSqlTxt(BasicsPageDataModel Obj_DaModel) {
            DataModel daModel = new DataModel();
            try { daModel = (DataModel)Obj_DaModel; } catch (Exception) { }

            List<string> sqls = new List<string>();

            if (!CheckData.IsStringNull(daModel.Name)) {
                sqls.Add(CreateSQL.WhereEqual(BLL_vismsg.ColName_name, daModel.Name));
            }

            daModel.Time_Start = daModel.Time_Start.Replace("'", "");
            if (!CheckData.IsStringNull(daModel.Time_Start)) {
                sqls.Add(CreateSQL.WhereBigThanEqual(BLL_vismsg.ColName_TimeAdd, daModel.Time_Start));
            }
            daModel.Time_End = daModel.Time_End.Replace("'", "");
            if (!CheckData.IsStringNull(daModel.Time_End)) {
                sqls.Add(CreateSQL.WhereSmallThanEqual(BLL_vismsg.ColName_TimeAdd, daModel.Time_End));
            }

            if (!CheckData.IsStringNull(daModel.LikeSelectValue)) {
                string[] valArr = ConvertTool.ToArrayList(daModel.LikeSelectValue, ',');
                string likeSelString = CreateSQL.WhereLikeAllPropertyInfo(new Model.visitor_message(), valArr);
                if (!CheckData.IsStringNull(likeSelString)) {
                    sqls.Add(likeSelString);
                }
            }

            string resultSQLStr = ConvertTool.ToString(sqls, CreateSQL.WHERE_AND);
            if (CheckData.IsStringNull(resultSQLStr.Trim())) {
                return string.Empty;
            }
            return CreateSQL.WHERE_AND + resultSQLStr;
        }
    }
}
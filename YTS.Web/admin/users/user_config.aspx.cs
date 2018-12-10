using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YTS.Tools;

namespace YTS.Web.admin.users
{
    public partial class user_config : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_config", DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo();
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            BLL.userconfig bll = new BLL.userconfig();
            Model.userconfig model = bll.loadConfig();

            regstatus.SelectedValue = model.regstatus.ToString();
            regmsgstatus.SelectedValue = model.regmsgstatus.ToString();
            regmsgtxt.Text = model.regmsgtxt;
            regkeywords.Text = model.regkeywords;
            regctrl.Text = model.regctrl.ToString();
            regsmsexpired.Text = model.regsmsexpired.ToString();
            regemailexpired.Text = model.regemailexpired.ToString();
            if (model.regverify == 1)
            {
                regverify.Checked = true;
            }
            else
            {
                regverify.Checked = false;
            }
            if (model.mobilelogin == 1)
            {
                mobilelogin.Checked = true;
            }
            else
            {
                mobilelogin.Checked = false;
            }
            if (model.emaillogin == 1)
            {
                emaillogin.Checked = true;
            }
            else
            {
                emaillogin.Checked = false;
            }
            if (model.regrules == 1)
            {
                regrules.Checked = true;
            }
            else
            {
                regrules.Checked = false;
            }
            regrulestxt.Text = model.regrulestxt;

            invitecodeexpired.Text = model.invitecodeexpired.ToString();
            invitecodecount.Text = model.invitecodecount.ToString();
            invitecodenum.Text = model.invitecodenum.ToString();
            pointcashrate.Text = model.pointcashrate.ToString();
            pointinvitenum.Text = model.pointinvitenum.ToString();
            pointloginnum.Text = model.pointloginnum.ToString();
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_config", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.userconfig bll = new BLL.userconfig();
            Model.userconfig model = bll.loadConfig();
            try
            {
                model.regstatus = ConvertTool.ToInt(regstatus.SelectedValue, 0);
                model.regmsgstatus = ConvertTool.ToInt(regmsgstatus.SelectedValue, 0);
                model.regmsgtxt = regmsgtxt.Text;
                model.regkeywords = regkeywords.Text.Trim();
                model.regctrl = ConvertTool.ToInt(regctrl.Text.Trim(), 0);
                model.regsmsexpired = ConvertTool.ToInt(regsmsexpired.Text.Trim(), 0);
                model.regemailexpired = ConvertTool.ToInt(regemailexpired.Text.Trim(), 0);
                if (regverify.Checked == true)
                {
                    model.regverify = 1;
                }
                else
                {
                    model.regverify = 0;
                }
                if (mobilelogin.Checked == true)
                {
                    model.mobilelogin = 1;
                }
                else
                {
                    model.mobilelogin = 0;
                }
                if (emaillogin.Checked == true)
                {
                    model.emaillogin = 1;
                }
                else
                {
                    model.emaillogin = 0;
                }
                if (regrules.Checked == true)
                {
                    model.regrules = 1;
                }
                else
                {
                    model.regrules = 0;
                }
                model.regrulestxt = regrulestxt.Text;

                model.invitecodeexpired = ConvertTool.ToInt(invitecodeexpired.Text.Trim(), 1);
                model.invitecodecount = ConvertTool.ToInt(invitecodecount.Text.Trim(), 0);
                model.invitecodenum = ConvertTool.ToInt(invitecodenum.Text.Trim(), 0);
                model.pointcashrate = ConvertTool.ToDecimal(pointcashrate.Text.Trim(), 0);
                model.pointinvitenum = ConvertTool.ToInt(pointinvitenum.Text.Trim(), 0);
                model.pointloginnum = ConvertTool.ToInt(pointloginnum.Text.Trim(), 0);
                bll.saveConifg(model);
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改用户配置信息"); //记录日志
                JscriptMsg("修改用户配置成功！", "user_config.aspx");
            }
            catch
            {
                JscriptMsg("文件写入失败，请检查是否有权限！", string.Empty);
            }
        }

    }
}
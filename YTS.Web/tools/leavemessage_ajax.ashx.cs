using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YTS.Web.UI;
using YTS.Common;

namespace YTS.Web.tools
{
    /// <summary>
    /// leavemessage_ajax 的摘要说明
    /// </summary>
    public class leavemessage_ajax : AbsHttpRequestHandler
    {
        public override string LogUseRequestFileName() {
            return @"YTS.Web.tools.leavemessage_ajax";
        }

        public override Dictionary<string, Func<HttpContext, Model.AjaxResult, Model.AjaxResult>> ActionSource() {
            return new Dictionary<string, Func<HttpContext, Model.AjaxResult, Model.AjaxResult>>() {
                { @"commit", Action_Commit },
            };
        }
        #region === Action: Commit ===
        private Model.AjaxResult Action_Commit(HttpContext context, Model.AjaxResult jsonResult) {
            base.ExecuteMethodName = @"Action_Commit";
            Model.visitor_message info_model = GetCommitInfo();
            string error_msg = CheckCommitInfo(info_model);
            if (!CheckData.IsStringNull(error_msg)) {
                return base.ErrorAjaxResult(jsonResult, error_msg);
            }

            info_model.ipaddress = DTRequest.GetIP();
            info_model.TimeAdd = DateTime.Now;

            BLL.visitor_message bll_vismsg = new BLL.visitor_message();
            const int minute_interval = 15;
            int ip_repeat_count = bll_vismsg.GetIPRepeatCount(info_model.ipaddress, info_model.TimeAdd, 15);
            if (ip_repeat_count >= 2) {
                return base.ErrorAjaxResult(jsonResult, @"您提交的太频繁了, 请" + minute_interval.ToString() + @"分钟后再提交!");
            }
            int rid = bll_vismsg.Add(info_model);
            if (rid > 0) {
                jsonResult.Status = Model.AjaxResult.StatusValue.Success;
                jsonResult.Msg = @"添加记录成功";
            } else {
                jsonResult.Status = Model.AjaxResult.StatusValue.Error;
                jsonResult.Msg = @"添加记录失败";
            }
            return jsonResult;
        }

        private Model.visitor_message GetCommitInfo() {
            return new Model.visitor_message() {
                name = DTRequest.GetString("name"),
                tel = DTRequest.GetString("tel"),
                msg = DTRequest.GetString("msg"),
            };
        }
        private string CheckCommitInfo(Model.visitor_message info_model) {
            if (CheckData.IsStringNull(info_model.name)) {
                return @"名称为空";
            }
            if (CheckData.IsStringNull(info_model.tel)) {
                return @"电话为空";
            }
            if (CheckData.IsStringNull(info_model.msg)) {
                return @"电话消息为空";
            }
            try {
                info_model.name = Utils.Filter(info_model.name);
                info_model.tel = Utils.Filter(info_model.tel);
                info_model.msg = Utils.Filter(info_model.msg);
            } catch (Exception) {
                return @"存在危险内容, 无法提交!";
            }
            return string.Empty;
        }
        #endregion
    }
}
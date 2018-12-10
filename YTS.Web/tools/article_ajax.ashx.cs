using System;
using System.Collections.Generic;
using System.Web;
using YTS.Common;
using YTS.Web.UI;

namespace YTS.Web.tools
{
    /// <summary>
    /// article_ajax 的摘要说明
    /// </summary>
    public class article_ajax : AbsHttpRequestHandler
    {
        public override string LogUseRequestFileName() {
            return @"YTS.Web.tools.article_ajax";
        }

        public override Dictionary<string, Func<HttpContext, Model.AjaxResult, Model.AjaxResult>> ActionSource() {
            return new Dictionary<string, Func<HttpContext, Model.AjaxResult, Model.AjaxResult>> {
                { @"like", Like },
            };
        }

        public Model.AjaxResult Like(HttpContext context, Model.AjaxResult ajax) {
            int channel_id = ConvertTool.StrToInt(DTRequest.GetString("channel_id"), 0);
            if (channel_id <= 0) {
                return ErrorAjaxResult(ajax, @"频道错误");
            }

            int article_id = ConvertTool.StrToInt(DTRequest.GetString("article_id"), 0);
            if (article_id <= 0) {
                return ErrorAjaxResult(ajax, @"文章错误");
            }

            BLL.article bllarticle = new BLL.article();
            string fieldname = ReflexHelp.Name(() => new Model.article().like_count);
            bool issu = bllarticle.UpdateField(channel_id, article_id, string.Format("{0} = {0} + 1", fieldname));
            if (!issu) {
                return ErrorAjaxResult(ajax, @"点赞失败!");
            }

            ajax.Status = Model.AjaxResult.StatusValue.Success;
            ajax.Msg = @"点赞成功!";
            return ajax;
        }
    }
}
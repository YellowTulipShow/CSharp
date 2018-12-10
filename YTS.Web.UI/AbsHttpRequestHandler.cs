using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using YTS.Common;

namespace YTS.Web.UI
{
    /// <summary>
    /// 抽象的HTTP请求处理程序
    /// </summary>
    public abstract class AbsHttpRequestHandler : IHttpHandler
    {
        /// <summary>
        /// 正在执行的方法名称 (错误日志中显示)
        /// </summary>
        public string ExecuteMethodName = @"ProcessRequest";

        /// <summary>
        /// Request 请求的文件名称
        /// </summary>
        public abstract string LogUseRequestFileName();

        /// <summary>
        /// 继承自接口: IHttpHandler 是否可重用
        /// </summary>
        public virtual bool IsReusable {
            get { return false; }
        }
        /// <summary>
        /// 继承自接口: IHttpHandler 处理请求 一般情况下继承此类不需要重写此方法
        /// </summary>
        /// <param name="context">HTTP 请求信息上下文</param>
        public virtual void ProcessRequest(HttpContext context) {
            StructureInitTool();
            Model.AjaxResult jsonResult = GetInitAjaxJsonResultModel();
            try {
                jsonResult = LogicalProcessing(context, jsonResult);
            } catch (Exception ex) {
                SystemLogWrite(ExecuteMethodName, ex);
                jsonResult.Status = Model.AjaxResult.StatusValue.Error;
                jsonResult.Msg = @"服务器出错了, 已记录日志...请联系管理员!";
            } finally {
                ReturnResponseResult(context, jsonResult);
            }
        }

        /// <summary>
        /// 构造初始化工具
        /// </summary>
        public virtual void StructureInitTool() { }

        /// <summary>
        /// 获得初始化的 Ajax json 结果模型
        /// </summary>
        public virtual Model.AjaxResult GetInitAjaxJsonResultModel() {
            return new Model.AjaxResult();
        }
        /// <summary>
        /// 逻辑处理
        /// </summary>
        private Model.AjaxResult LogicalProcessing(HttpContext context, Model.AjaxResult jsonResult) {
            string action = DTRequest.GetString(ActionParameterName());
            Dictionary<string, Func<HttpContext, Model.AjaxResult, Model.AjaxResult>> actionsource = ActionSource();
            if (CheckData.IsSizeEmpty(actionsource)) {
                actionsource = new Dictionary<string, Func<HttpContext, Model.AjaxResult, Model.AjaxResult>>();
            }
            if (!actionsource.ContainsKey(action)) {
                Func<HttpContext, Model.AjaxResult, Model.AjaxResult> defaultAction = DefaultActionMethod();
                if (CheckData.IsObjectNull(defaultAction)) {
                    return ErrorAjaxResult(jsonResult, @"没有可以执行的方法");
                } else {
                    actionsource.Add(action, defaultAction);
                }
            }
            ExecuteMethodName = action;
            return actionsource[action](context, jsonResult);
        }
        /// <summary>
        /// Action 判断行为的参数名称
        /// </summary>
        public virtual string ActionParameterName() {
            return @"action";
        }

        /// <summary>
        /// 提供行为源方法
        /// </summary>
        public abstract Dictionary<string, Func<HttpContext, Model.AjaxResult, Model.AjaxResult>> ActionSource();

        /// <summary>
        /// 默认行为
        /// </summary>
        public virtual Func<HttpContext, Model.AjaxResult, Model.AjaxResult> DefaultActionMethod() {
            return (context, jsonResult) => {
                jsonResult.Status = Model.AjaxResult.StatusValue.Error;
                jsonResult.Msg = @"not exe code!";
                return jsonResult;
            };
        }

        /// <summary>
        /// 错误结果模型传递
        /// </summary>
        /// <param name="source_model">需要转换的模型</param>
        /// <param name="error_msg">错误信息描述</param>
        /// <returns></returns>
        public virtual Model.AjaxResult ErrorAjaxResult(Model.AjaxResult source_model, string error_msg) {
            if (CheckData.IsObjectNull(source_model)) {
                source_model = GetInitAjaxJsonResultModel();
            }
            source_model.Status = Model.AjaxResult.StatusValue.Error;
            source_model.Msg = error_msg;
            source_model.ResultContent = new object();
            source_model.Url = string.Empty;
            return source_model;
        }

        /// <summary>
        /// 日志方法输出
        /// </summary>
        public void SystemLogWrite(string functionName, Exception ex) {
            string postion = String.Format("{0}.{1}", LogUseRequestFileName(), functionName);
            StringBuilder msg = new StringBuilder();
            msg.AppendFormat("执行 {0} 方法 出错!!!,错误详情信息:", functionName);
            msg.Append(ex.Message);
            msg.Append("引发当前异常的方法(ToString()):");
            msg.Append(ex.TargetSite.ToString());
            msg.Append("导致错误的应用程序或对象名称:");
            msg.Append(ex.Source);
            SystemLog.Write(postion, msg.ToString());
        }
        /// <summary>
        /// 日志方法输出
        /// </summary>
        public void SystemLogWrite(string functionName, string errordata) {
            string postion = string.Format("{0}.{1}", LogUseRequestFileName(), functionName);
            string message = string.Format("错误数据内容: {0}", errordata);
            SystemLog.Write(postion, message);
        }

        /// <summary>
        /// 给客户端返回结果
        /// </summary>
        public virtual void ReturnResponseResult(HttpContext context, Model.AjaxResult ajaxresultModel) {
            context = SetContentFormat(context);
            dynamic dyobj = AjaxResultFormat(ajaxresultModel);
            context.Response.Write(OutputWriteContent(dyobj));
            context.Response.End();
        }

        /// <summary>
        /// 输出写入转换内容
        /// </summary>
        public virtual string OutputWriteContent(dynamic objvalue) {
            return JsonHelper.ObjectToJSON(objvalue);
        }

        /// <summary>
        /// 为需要返回的结果设置内容格式和编码
        /// </summary>
        public virtual HttpContext SetContentFormat(HttpContext context) {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            return context;
        }

        /// <summary>
        /// ajax 结果格式化
        /// </summary>
        public virtual dynamic AjaxResultFormat(Model.AjaxResult ajaxresultModel) {
            return new {
                Status = (int)ajaxresultModel.Status,
                Msg = ajaxresultModel.Msg,
                Url = ajaxresultModel.Url,
                ReturnJsonStrs = ajaxresultModel.ResultContent,
            };
        }

        /// <summary>
        /// 纯粹随机 返回是否成功条件的判断, 测试占位用途, 没有生产意义
        /// </summary>
        protected bool TestWhereIsSuccess() {
            //return RandomData.GetInt(0, 1 + 1) == 1;
            return true;
        }

        /// <summary>
        /// 添加上当前程序所绑定的域名信息,确保客户拿到的路径上可以查看图片
        /// </summary>
        public string BindThisProgramHost(string fileurl) {
            if (CheckData.IsStringNull(fileurl)) {
                return string.Empty;
            }
            string thishost = HttpContext.Current.Request.Url.Host;
            string resuStr = string.Format("http://{0}{1}", thishost, fileurl);
            return resuStr;
        }
    }

    /// <summary>
    /// 抽象的HTTP请求处理程序 Jsonp 跨域请求时使用
    /// </summary>
    public abstract class AbsHttpRequestHandlerCrossDomain : AbsHttpRequestHandler
    {
        public string JsonpCallback_Name = string.Empty;

        /// <summary>
        /// 获得初始化的 Ajax json 响应结果模型 Jsonp 跨域请求时使用
        /// </summary>
        public override Model.AjaxResult GetInitAjaxJsonResultModel() {
            this.JsonpCallback_Name = DTRequest.GetString(JsonpCallbackParameterKeyName());
            return new Model.AjaxResult() {
                JsonpCallback = this.JsonpCallback_Name,
            };
        }

        /// <summary>
        /// Ajax Jsonp方式请求获得返回的方法名称
        /// </summary>
        public virtual string JsonpCallbackParameterKeyName() {
            return @"callback";
        }

        /// <summary>
        /// 返回响应客户端结果 Jsonp 跨域请求时使用
        /// </summary>
        public override void ReturnResponseResult(HttpContext context, Model.AjaxResult ajaxresultModel) {
            context = SetContentFormat(context);
            dynamic dyobj = AjaxResultFormat(ajaxresultModel);
            string rstr = string.Format("{0}({1});", this.JsonpCallback_Name, OutputWriteContent(dyobj));
            context.Response.Write(rstr);
            context.Response.End();
        }
    }

    /// <summary>
    /// 抽象的HTTP请求处理程序 图片资源时使用
    /// </summary>
    public abstract class AbsHttpRequestHandlerImageResource : AbsHttpRequestHandler
    {
        /// <summary>
        /// 图片内容类型 HTTP MINI 类型
        /// </summary>
        public string ImageContentType = System.Drawing.Imaging.ImageFormat.Png.ToString();

        /// <summary>
        /// 返回响应客户端结果 图片资源时使用
        /// </summary>
        public override void ReturnResponseResult(HttpContext context, Model.AjaxResult ajaxresultModel) {
            context = SetContentFormat(context);
            context.Response.BinaryWrite(ajaxresultModel.BinaryResource);
            EndResponseBeforeEvent(context);
            context.Response.End();
        }

        /// <summary>
        /// 为需要返回的结果设置内容格式和编码 使用之前请提前设置 ImageContentType 参数
        /// </summary>
        public override HttpContext SetContentFormat(HttpContext context) {
            context.Response.ClearContent();
            context.Response.ContentType = string.Format("image/{0}", ImageContentType);
            return context;
        }

        /// <summary>
        /// 结束响应之前需要执行的事件
        /// </summary>
        public virtual void EndResponseBeforeEvent(HttpContext context) { }

        /// <summary>
        /// 获取 图片文件格式信息
        /// </summary>
        /// <param name="img">图片内容值</param>
        /// <returns>传入图片的文件格式</returns>
        public ImageFormat GetImageFormat(Image img) {
            ImageFormat[] list = new ImageFormat[] {
                ImageFormat.Bmp,
                ImageFormat.Emf,
                ImageFormat.Exif,
                ImageFormat.Gif,
                ImageFormat.Icon,
                ImageFormat.Jpeg,
                ImageFormat.MemoryBmp,
                ImageFormat.Png,
                ImageFormat.Tiff,
                ImageFormat.Wmf,
            };
            ImageFormat imgformat = ImageFormat.Png;
            foreach (ImageFormat item in list) {
                if (img.RawFormat == item) {
                    imgformat = item;
                    break;
                }
            }
            this.ImageContentType = imgformat.ToString();
            return imgformat;
        }
    }
}

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace YTS.AdminWebApi
{
    /// <summary>
    /// 全局路由前缀配置
    /// </summary>
    public class RouteConvention : IApplicationModelConvention
    {
        /// <summary>
        /// 定义一个路由前缀变量
        /// </summary>
        private readonly AttributeRouteModel _centralPrefix;

        /// <summary>
        /// 调用时传入指定的路由前缀
        /// </summary>
        /// <param name="routeTemplateProvider"></param>
        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            _centralPrefix = new AttributeRouteModel(routeTemplateProvider);
        }

        /// <summary>
        /// 接口的Apply方法
        /// </summary>
        /// <param name="application">应用程序</param>
        public void Apply(ApplicationModel application)
        {
            //遍历所有的 Controller
            foreach (ControllerModel controller in application.Controllers)
            {
                // 1、已经标记了 RouteAttribute 的 Controller
                //这一块需要注意，如果在控制器中已经标注有路由了，则会在路由的前面再添加指定的路由内容。

                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        // 在 当前路由上 再 添加一个 路由前缀
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix,
                            selectorModel.AttributeRouteModel);
                    }
                }

                //2、 没有标记 RouteAttribute 的 Controller
                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelectors.Any())
                {
                    foreach (var selectorModel in unmatchedSelectors)
                    {
                        // 添加一个 路由前缀
                        selectorModel.AttributeRouteModel = _centralPrefix;
                    }
                }

                foreach (ActionModel action in controller.Actions)
                {
                    SetActionDefaultRouteAttribute(action);
                }
            }
        }

        private void SetActionDefaultRouteAttribute(ActionModel action)
        {
            // 1、已经标记了 RouteAttribute 的 Controller
                //这一块需要注意，如果在控制器中已经标注有路由了，则会在路由的前面再添加指定的路由内容。
            RouteAttribute default_route_attr = new RouteAttribute("T_" + action.ActionName);
            AttributeRouteModel defualt_attr = new AttributeRouteModel(default_route_attr);
            // action.Attributes.Append(default_route_attr);
            // var matchedSelectors = action.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
            // if (matchedSelectors.Any())
            // {
            //     foreach (var selectorModel in matchedSelectors)
            //     {
            //         // 在 当前路由上 再 添加一个 路由前缀
            //         selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(defualt_attr,
            //             selectorModel.AttributeRouteModel);
            //     }
            // }

            //2、 没有标记 RouteAttribute 的 Controller
            var unmatchedSelectors = action.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
            if (unmatchedSelectors.Any())
            {
                foreach (var selectorModel in unmatchedSelectors)
                {
                    // 添加一个 路由前缀
                    selectorModel.AttributeRouteModel = defualt_attr;
                }
            }
        }
    }
}
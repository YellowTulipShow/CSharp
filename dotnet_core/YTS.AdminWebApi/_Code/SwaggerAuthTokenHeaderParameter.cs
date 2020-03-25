using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;

namespace YTS.WebApi
{
    public class SwaggerAuthTokenHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();
            var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor == null)
                return;
            var customAttrs = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
            var typeAllowAnonymous = typeof(AllowAnonymousAttribute);
            bool isHaveAllowAnonymous = customAttrs.Any(a => a.GetType().Equals(typeAllowAnonymous));
            if (isHaveAllowAnonymous)
                return;
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "Bearer XXXXXXXXX",
                Required = true //是否必选
            });
        }
    }
}

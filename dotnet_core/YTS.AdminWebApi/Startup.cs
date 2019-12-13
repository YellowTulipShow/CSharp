using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using YTS.Data.Models.WebApi;

namespace YTS.AdminWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// 运行时将调用此方法。 使用此方法将服务添加到容器。
        /// </summary>
        /// <param name="services">服务</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 增加 Controller 注册启用
            services.AddControllers(option =>
            {
                // 关闭 启用端点路由
                option.EnableEndpointRouting = false;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            // 注册Swagger生成器，定义1个或多个Swagger文档
            services.AddSwaggerGen(c =>
            {
                // 配置 v1文档
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "后台管理API",
                    Description = "使用 ASP.NET Core Web API 构建的后台管理API",
                    Contact = new OpenApiContact
                    {
                        Name = "YellowTulipShow (赵瑞青)",
                        Email = "main@yellowtulipshow.site",
                        Url = new Uri("https://github.com/YellowTulipShow"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "使用 Apache License 许可证",
                        Url = new Uri("http://www.apache.org/licenses/"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                // 设置Swagger JSON和UI的注释路径。读取代码XML注释文档
                var name = Assembly.GetExecutingAssembly().GetName().Name;
                var xmlFile = $"{name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// 运行时将调用此方法。 使用此方法来配置HTTP请求管道。
        /// </summary>
        /// <param name="app">应用程序生成器</param>
        /// <param name="env">IWebHost环境</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // 启用中间件以将生成的Swagger用作JSON端点。
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            // 启用中间件以提供swagger-ui（HTML，JS，CSS等），
            // 指定Swagger JSON端点。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdminWebApi v1");
                c.RoutePrefix = string.Empty;
            });

            // 启用路由
            app.UseRouting();

            // 使用MVC
            app.UseMvc(routes =>
            {
                // 配置默认MVC路由模板
                routes.MapRoute(
                    name: "default",
                    template: ApiConfig.APIRoute);
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

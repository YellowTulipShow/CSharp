using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace YTS.AdminWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(option =>
            {
                option.EnableEndpointRouting = false;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
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
                var name = Assembly.GetExecutingAssembly().GetName().Name;
                var xmlFile = $"{name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseRouting();

            app.UseMvc(routes =>
            {
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

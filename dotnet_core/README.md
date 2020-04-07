# .Net Core 代码库

## 开发配置

### 端口

| 端口 | 解决方案 | 模块 | 位置 | 协议 |
| --- | --- | --- | --- | --- |
| 6111 | 6:YTS自定义 | 1:后台管理 | 1:前端H5页面 | 1:Https协议 |
| 6112 | 6:YTS自定义 | 1:后台管理 | 1:前端H5页面 | 2:Http协议 |
| 6121 | 6:YTS自定义 | 1:后台管理 | 2:API | 1:Https协议 |
| 6122 | 6:YTS自定义 | 1:后台管理 | 2:API | 2:Http协议 |

## 发布

### 发布命令模板

#### Window

```shell
dotnet publish -o <盘符>:\wwwroot\YTSCSharpDotNetCore\<项目名称>
```

#### Linux

```shell
dotnet publish -o /var/wwwroot/YTSCSharpDotNetCore/<项目名称>
```

### 本地常用命令

```shell
dotnet publish -o D:\wwwroot\YTSCSharpDotNetCore\YTS.AdminWeb
dotnet publish -o D:\wwwroot\YTSCSharpDotNetCore\YTS.AdminWebApi
```

## 数据库更新命令

```shell
cd ../YTS.AdminWebApi
dotnet ef dbcontext list
dotnet ef database drop --force
cd ../YTS.Shop
dotnet ef migrations add InitialCreate --startup-project ../YTS.AdminWebApi
dotnet ef database update --startup-project ../YTS.AdminWebApi
dotnet ef migrations remove --startup-project ../YTS.AdminWebApi
```

## 学习链接

* [微软官网 - .NET Core 指南](https://docs.microsoft.com/zh-cn/dotnet/core/)
* [微软官网 - .NET Core SDK 概述](https://docs.microsoft.com/zh-cn/dotnet/core/sdk)
* [微软官网 - Entity Framework Core](https://docs.microsoft.com/zh-cn/ef/core/)
* [.NET Core简单读取json配置文件](https://www.jb51.net/article/137517.htm)
* [.net core 使用Newtonsoft.Json 读取Json文件数据](https://blog.csdn.net/liwan09/article/details/102952990)
* [.net core读取json格式的配置文件](https://www.cnblogs.com/dotnet261010/p/10172961.html)
* [Sqlite Browser](https://sqlitebrowser.org/)
* [什么是 JWT -- JSON WEB TOKEN](https://www.jianshu.com/p/576dbf44b2ae)
* [ASP.Net Core 3.1 中使用JWT认证](https://www.cnblogs.com/liuww/p/12177272.html)
* [nuget.org 无法加载源 https://api.nuget.org/v3/index.json 的服务索引](https://www.cnblogs.com/shapaozi/archive/2017/10/31/7764469.html)
* [jquery ajax设置header的两种方式](https://blog.csdn.net/shjavadown/article/details/51213342)
* [AJAX请求中出现OPTIONS请求](https://www.cnblogs.com/wanghuijie/p/preflighted_request.html)
* [OPTIONS 方法在跨域请求（CORS）中的应用](https://blog.csdn.net/qizhiqq/article/details/71171916)
* [dotNET Core Web API+JWT(Bearer Token)认证+Swagger UI](https://blog.csdn.net/qq_35904166/article/details/84591227)
* [EF Core 入门](https://docs.microsoft.com/zh-cn/ef/core/get-started/?tabs=netcore-cli)
* [ASP.NET Core 中的 Razor 页面和 Entity Framework Core - 第 1 个教程（共 8 个）](https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/intro?view=aspnetcore-3.1&tabs=visual-studio-code)
* [SQLite Browser 数据库浏览器](https://sqlitebrowser.org/)
* [6个宝藏级Vue管理后台框架 必须收藏](https://zhuanlan.zhihu.com/p/91825869)
* [asp.net core 集成JWT（一）](https://www.cnblogs.com/7tiny/p/11012035.html)
* [从壹开始前后端分离【 .NET Core2.2/3.0 +Vue2.0 】框架之五 || Swagger的使用 3.3 JWT权限验证【必看】 - 老张的哲学 - 博客园](https://www.cnblogs.com/laozhang-is-phi/p/9511869.html#autoid-4-0-0)
* [easyUI datagrid携带token](https://blog.csdn.net/zcwforali/article/details/79866181)
* [easyui jquery ajax的全局设置token](https://blog.csdn.net/mutourenoo/article/details/84921154)
* [ajax 全局设置token，并且判断是或否登录 $.ajaxSetup设置](https://blog.csdn.net/qq_32674347/article/details/88415757)
* [ASP.NET CORE系列【六】Entity Framework Core 之数据迁移](https://www.cnblogs.com/shumin/p/8877297.html)
* [Jquery EasyUI插件](http://www.jeasyui.net/plugins)
* [asp.net core合并压缩资源文件引发的学习之旅](https://www.cnblogs.com/morang/p/7604612.html)
* [ASP.NET Core 中的捆绑和缩小静态资产](https://docs.microsoft.com/zh-cn/aspnet/core/client-side/bundling-and-minification?view=aspnetcore-3.1&tabs=netcore-cli)
* [设置 ASP.NET Core Web API 中响应数据的格式](https://docs.microsoft.com/zh-cn/aspnet/core/web-api/advanced/formatting?view=aspnetcore-3.1)
* [.net core3.1 web api中使用newtonsoft替换掉默认的json序列化组件](https://www.cnblogs.com/shapman/p/12232640.html)
* [NuGet.org 无法访问的解决方法](https://blog.csdn.net/weixin_34242819/article/details/85688216)

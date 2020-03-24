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

## 学习链接

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

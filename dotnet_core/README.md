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

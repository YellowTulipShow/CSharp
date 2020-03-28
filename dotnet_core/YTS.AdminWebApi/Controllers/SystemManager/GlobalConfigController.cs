using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using YTS.Tools;
using YTS.WebApi;

namespace YTS.AdminWebApi.Controllers
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public class GlobalConfigController : BaseApiController
    {
        /// <summary>
        /// 获取所有请求代码的详情
        /// </summary>
        [HttpGet]
        public object ResultCodes()
        {
            EnumInfo[] infos = EnumInfo.AnalysisList<ResultCode>();
            var list = infos
                .Select(info => new
                {
                    code = info.IntValue,
                    name = info.Name,
                    remark = info.Explain,
                })
                .ToList();
            return list;
        }
    }
}

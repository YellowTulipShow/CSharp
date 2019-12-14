using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YTS.Tools;
using YTS.Data.Models;
using YTS.Data.Models.WebApi;

namespace YTS.AdminWebApi.Controllers
{
    public class ResultCodeController : BaseApiController
    {
        /// <summary>
        /// 获取所有请求代码的详情
        /// </summary>
        [HttpGet]
        public object GetResultCodes()
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

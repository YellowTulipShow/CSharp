using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YTS.Tools;
using YTS.Data.Models;
using YTS.Data.Models.WebApi;
using YTS.AlgorithmLogic.Global;

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

        /// <summary>
        /// 测试获取测试全局单例数据实例哈希码
        /// </summary>
        /// <returns>实例-哈希码</returns>
        [HttpGet]
        public int GetDbInstanceHashCode()
        {
            DbInstance dbInstance = DbInstance.GetInstance();
            return dbInstance.GetHashCode();
        }
    }
}

using System;
using System.Linq.Expressions;
using System.Text;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 成员信息获取
    /// </summary>
    public static class MemberInfoGetting
    {
        /// <summary>
        /// 获取指定 "内容" 名称 用法: MemberInfoGetting.Name(() => new ModelClass().ID)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberExpression"></param>
        /// <returns></returns>
        public static string Name<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}

using System;
using System.Collections;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 随机帮助类
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// 在 IList 集合中随机返回一个选项
        /// </summary>
        /// <typeparam name="TL">集合类型</typeparam>
        /// <typeparam name="TI">选项类型</typeparam>
        /// <param name="list">数据源集合</param>
        private static TI RandomOneItem<TL, TI>(this TL list, TI errorValue = default(TI)) where TL : IList {
            if (CheckData.IsSizeEmpty(list)) {
                return default(TI);
            }
            int sign = 0;
            if (list.Count > 1) {
                sign = new Random().Next(0, list.Count);
            }
            try {
                object val = list[sign];
                return (TI)val;
            } catch (Exception) {
                return default(TI);
            }
        }

        /// <summary>
        /// 在 数组 集合中随机返回一个选项
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="list">数据源集合</param>
        public static T RandomOneItem<T>(T[] list) {
            return RandomOneItem<T[], T>(list);
        }
    }
}

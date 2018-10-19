using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace YTS.Tools
{
    /// <summary>
    /// 缓存助手 (不是很明白的类)
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 创建缓存项的文件
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        public static void Insert(string key, object obj)
        {
            //创建缓存
            HttpContext.Current.Cache.Insert(key, obj);
        }
        /// <summary>
        /// 移除缓存项的文件
        /// </summary>
        /// <param name="key">缓存Key</param>
        public static void Remove(string key)
        {
            //创建缓存
            HttpContext.Current.Cache.Remove(key);
        }
        /// <summary>
        /// 创建缓存项的文件依赖
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="fileName">文件绝对路径</param>
        public static void Insert(string key, object obj, string fileName)
        {
            //创建缓存依赖项
            System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(fileName);
            //创建缓存
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }

        /// <summary>
        /// 创建缓存项过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void Insert(string key, object obj, int expires)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>object对象</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            return HttpContext.Current.Cache.Get(key);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">T对象</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            object obj = Get(key);
            return obj == null ? default(T) : (T)obj;
        }


        ///<summary>
        ///获取当前应用程序指定CacheKey的Cache对象值
        ///</summary>
        ///<param name="CacheKey">索引键值</param>
        ///<returns>返回缓存对象</returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];

        }

        ///<summary>
        ///设置以缓存依赖的方式缓存数据
        ///</summary>
        ///<param name="CacheKey">索引键值</param>
        ///<param name="objObject">缓存对象</param>
        ///<param name="cacheDepen">依赖对象</param>
        public static void SetCache(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey,objObject,dep,
                System.Web.Caching.Cache.NoAbsoluteExpiration, //从不过期
                System.Web.Caching.Cache.NoSlidingExpiration, //禁用可调过期
                System.Web.Caching.CacheItemPriority.Default,
                null);
        }

        protected void Use(object sender, EventArgs e)
        {
            string CacheKey = "cachetest";
            object objModel = GetCache(CacheKey);//从缓存中获取
            if (objModel == null) //缓存里没有
            {
                objModel = DateTime.Now;//把当前时间进行缓存
                if (objModel != null)
                {
                    //依赖 C:\\test.txt 文件的变化来更新缓存
                    System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency("C:\\test.txt");
                    SetCache(CacheKey, objModel, dep);//写入缓存
                }
            }
        }

    }
}

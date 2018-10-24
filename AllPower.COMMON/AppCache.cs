using System;
using System.Web;
using System.Web.Caching;
using System.Configuration;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年4月23日
    功能描述： 系统缓存类
    ===============================================================*/
#endregion

namespace AllPower.Common
{
    public class AppCache
    {
        public static Cache _cache = HttpContext.Current.Cache;
        private AppCache() { }

        public static bool IsExist(string key)
        {
            _cache = HttpContext.Current.Cache;
            if (_cache[key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Add(string key, object obj)
        {
            _cache.Add(key, obj, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public static void Add(string key, object obj, string file)
        {
            //允许表指定缓存依赖项
            SqlCacheDependencyAdmin.EnableTableForNotifications(ConfigurationManager.ConnectionStrings["SQLConnString1"].ConnectionString, file);
            _cache.Add(key, obj, new SqlCacheDependency("HQB",file), Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        public static object Get(string key)
        {
            return _cache[key];
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="ExpiredTime">过期时间，分钟</param>
        public static void AddCache(string key, object obj, int ExpiredTime)
        {
            _cache.Add(key, obj, null, DateTime.Now.AddMinutes(ExpiredTime), TimeSpan.Zero, CacheItemPriority.High, null);
        }
    }
}

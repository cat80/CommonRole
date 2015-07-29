using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;


namespace Common
{

    /// <summary>
    /// 缓存封装类（根据winform或者Webform调用HttpRuntime.Cache或HttpContext.Current.Cache
    /// </summary>
    public sealed class MPCache
    {

        private static Cache _cache;

        public const int DayFactor = 0x4380;
        private static int Factor = 5;
        public const int HourFactor = 720;
        public const int MinuteFactor = 12;
        public const double SecondFactor = 0.2;

        static MPCache()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                _cache = current.Cache;
            }
            else
            {
                _cache = HttpRuntime.Cache;
            }
        }

        private MPCache()
        {
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            ArrayList list = new ArrayList();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key);
            }
            foreach (string str in list)
            {
                _cache.Remove(str);
            }
        }

        /// <summary>
        /// 缓存缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return _cache[key];
        }

        /// <summary>
        /// 泛型获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            object value = Get(key);
            if (value == null)
            {
                return default(T);
            }
            try
            {
                return (T)_cache[key];
            }
            catch
            {
                //转换失败返回默认
                return default(T);
            }

        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">缓存值</param>
        public static void Insert(string key, object obj)
        {
            Insert(key, obj, null, 1);
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">缓存值</param>
        /// <param name="seconds">过期日期</param>
        public static void Insert(string key, object obj, int seconds)
        {
            Insert(key, obj, null, seconds);
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">缓存值</param>
        /// <param name="seconds">过期日期</param>
        /// <param name="dep">依赖对象</param>
        public static void Insert(string key, object obj, CacheDependency dep)
        {
            Insert(key, obj, dep, 0x21c0);
        }

        public static void Insert(string key, object obj, int seconds, CacheItemPriority priority)
        {
            Insert(key, obj, null, seconds, priority);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds)
        {
            Insert(key, obj, dep, seconds, CacheItemPriority.Normal);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.Now.AddSeconds((double)(Factor * seconds)), TimeSpan.Zero, priority, null);
            }
        }

        /// <summary>
        /// 不过期缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        /// <summary>
        /// 不过期缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="dep"></param>
        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }

        public static void MicroInsert(string key, object obj, int secondFactor)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, null, DateTime.Now.AddSeconds((double)(Factor * secondFactor)), TimeSpan.Zero);
            }
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    _cache.Remove(enumerator.Key.ToString());
                }
            }
        }

        public static void ReSetFactor(int cacheFactor)
        {
            Factor = cacheFactor;
        }

        public static int SecondFactorCalculate(int seconds)
        {
            return Convert.ToInt32(Math.Round((double)(seconds * 0.2)));
        }
    }
}


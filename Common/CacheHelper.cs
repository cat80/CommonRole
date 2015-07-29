using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Common
{
    public class CacheHelper
    {
        private static object obj = new object();

        public static void SetData(string key, object objData)
        {
            lock (obj)
            {
                HttpRuntime.Cache.Add(key, objData, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero,
                            CacheItemPriority.Normal, null);
            }
        }

        public static List<T> GetData<T>(string key)
        {
            try
            {
                List<T> list = HttpRuntime.Cache[key] as List<T>;

                return list;
            }
            catch (Exception ex)
            {
                Log.Default.Error(ex);
                return null;
            }
        }

    }
}

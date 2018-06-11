using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Common
{
    public class CacheHelper
    {
        public static void Insert(String key, Object obj) => HttpContext.Current.Cache.Insert(key, obj);

        public static void Insert(String key, Object obj, String fileName)
        {
            var dep = new CacheDependency(fileName);
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }

        public static void Insert(String key, Object obj, int expires) => HttpContext.Current.Cache.Insert(key, obj,null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));

        public static T Get<T>(String key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }

            return (T)HttpContext.Current.Cache.Get(key);
        }


        public void RemoveByPattern(String pattern)
        {
            var enumerator = HttpContext.Current.Cache.GetEnumerator();
            var rgx = new Regex(pattern, (RegexOptions.Singleline | (RegexOptions.Compiled | RegexOptions.IgnoreCase)));
            while(enumerator.MoveNext())
            {
                if(rgx.IsMatch(enumerator.Key.ToString()))
                {
                    HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
                }
            }

        }

        public void Clear()
        {
            var enumerator = HttpContext.Current.Cache.GetEnumerator();
            while(enumerator.MoveNext())
            {
                HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
            }
        }

    }

}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Utils
    {
        public static string GetFileExt(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return "";
            }
            if (_filepath.LastIndexOf(".") > 0)
            {
                return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
            }
            return "";
        }

        public static string GetFileNameWithoutExt(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return "";
            }

            if (_filepath.LastIndexOf(".") > 0)
            {
                return _filepath.Substring(0,_filepath.LastIndexOf("."));
            }

            return "";
        }


      
        public static string GetFriendlyTime(DateTime dt)
        {
            var ts = DateTime.Now - dt;
            if (ts.TotalDays >= 365)
                return "一年以前";

            if (ts.TotalDays >= 30)
                return ((int)(ts.TotalDays / 30)).ToString() + "月前";

            if (ts.TotalDays >= 1)
                return ((int)ts.TotalDays).ToString() + "天前";
            if (ts.TotalHours >= 1)
            {
                return ((int)ts.TotalHours).ToString() + "小时前";
            }
            if (ts.TotalMinutes >= 1)
            {
                return ((int)ts.TotalMinutes).ToString() + "分钟前";
            }
            return "刚刚";

        }
    }
}

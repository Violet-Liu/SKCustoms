using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static partial  class ValueConvert
    {
        public static string MD5(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] arr = UTF8Encoding.Default.GetBytes(str);
            byte[] bytes = md5.ComputeHash(arr);
            str = BitConverter.ToString(bytes);
            str = str.Replace("-", "");
            return str;
        }

        public static Maybe<T> ToMaybe<T>(this T t)
        {
            return t;
        }

        public static bool IsNotNull(this Object value)
        {
            if (value != null && value.ToString().Trim() != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool IsNull(this object value)
        {
            if (value == null || value.ToString() == string.Empty)
            {
                return true;
            }
            return false;
        }

        public static long ToLong(this string value)
        {
            if (long.TryParse(value, out long result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// convert to int type: false=-1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// convert to datetime type: false=datetime.now
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            if(DateTime.TryParse(value,out DateTime result))
            {
                return result;
            }
            else
            {
                return DateTime.Now;
            }
        }

        public static string NullToString(this string value, string defaultValue = "")
        {
            if (value == null || string.IsNullOrEmpty(value.Trim()))
                return defaultValue;

            return value;
        }
    }
}

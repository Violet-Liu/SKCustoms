using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigPara
    {
        public static string DBConnection
        {
            get
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["DBContainer"].ConnectionString;
                string result = AESEncryptHelper.Decrypt(connection);
                return result;
                //return "name=DBContainer";
            }
        }

        public static string TestDBConnection
        {
            get
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["DBContainer"].ConnectionString;
                string result = AESEncryptHelper.Decrypt(connection);
                return result;
                //return "name=DBContainer";
            }
        }

        public static string DockUrl
        {
            get=> System.Configuration.ConfigurationManager.AppSettings["DockUrl"].ToString();
        }

        public static string DockTableName
        {
            get => System.Configuration.ConfigurationManager.AppSettings["TableName"].ToString();
        }

        public static int LetterCount
        {
            get => System.Configuration.ConfigurationManager.AppSettings["letterCount"].ToInt();
        }

        public static string MQIdaddress
        {
            get => System.Configuration.ConfigurationManager.AppSettings["mqidaddress"].ToString();
        }

        public static String Destination
        {
            get => System.Configuration.ConfigurationManager.AppSettings["destination"].ToString();
        }

        public static int SleepTime
        {
            get => System.Configuration.ConfigurationManager.AppSettings["sleepTime"].ToInt();
        }
    }
}

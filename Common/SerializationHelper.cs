using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    public class SerializationHelper
    {
        public SerializationHelper()
        {
        }

        public static object Load(Type type,string filename)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                 XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != fs)
                    fs.Close();
            }
        }

        public static void Save(object obj,String filename)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != fs)
                    fs.Close();
            }
        }
    }
}

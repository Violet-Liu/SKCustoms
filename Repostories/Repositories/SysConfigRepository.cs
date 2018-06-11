using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public partial class SysConfigRepository
    {
        private static object lockHelper = new object();

        public SysConfigModel LoadConfig(String configFilePath) => (SysConfigModel)SerializationHelper.Load(typeof(SysConfigModel), configFilePath);

        public void SaveConfig(SysConfigModel model,String configFilePath)
        {
            lock(lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
        }
    }
}

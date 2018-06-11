using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Repostories;
using Domain;

namespace Services
{
    public partial class SysConfigService
    {
        private readonly SysConfigRepository _sysConfig = new SysConfigRepository();

        public SysConfigModel LoadConfig(string configFilePath)
        {
            var model = CacheHelper.Get<SysConfigModel>(ContextKeys.CACHE_SITE_CONFIG);

            if(null==model)
            {
                model = _sysConfig.LoadConfig(configFilePath);
                CacheHelper.Insert(ContextKeys.CACHE_SITE_CONFIG, model, configFilePath);
            }

            return model;
        }


        public void SaveConfig(SysConfigModel model, string configFilePath) => _sysConfig.SaveConfig(model, configFilePath);
    }
}

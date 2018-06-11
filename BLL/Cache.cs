using Common;
using Domain;
using Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial class Cache
    {
        public static List<SysModule> GetSysModules()
        {
            var entities = CacheHelper.Get<List<SysModule>>(ContextKeys.CACHE_SYSMODULES);
            if (entities == null || entities.Count == 0)
            {
                using (SKContext context = new SKContext())
                {
                    entities = context.SysModules.ToList();
                    CacheHelper.Insert(ContextKeys.CACHE_SYSMODULES, entities, 30);
                }
            }
            return entities;
        }
    }
}

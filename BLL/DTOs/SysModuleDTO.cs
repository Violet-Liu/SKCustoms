using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SysModuleDTO : BaseEntityDTO
    {
        public string Name { get; set; }

        public string EnglishName { get; set; }

        public string Url { get; set; }

        public string Iconic { get; set; }

        public string Sort { get; set; }

        public string Remark { get; set; }

        public string Enable { get; set; }

        public string CreateTime { get; set; }

        public string Creater { get; set; }

        public int IsLast { get; set; }

        public int ParentId { get; set; }
    }


    public class SysModule2DTO : BaseEntityDTO
    {
        public string Name { get; set; }

        public string EnglishName { get; set; }

        public string Url { get; set; }

        public string Iconic { get; set; }

        public string Sort { get; set; }

        public string Remark { get; set; }

        public string Enable { get; set; }

        public string CreateTime { get; set; }

        public string Creater { get; set; }

        public int IsLast { get; set; }

        public List<SysModule2DTO> SubSysModules { get; set; } = new List<SysModule2DTO>();
    }

}

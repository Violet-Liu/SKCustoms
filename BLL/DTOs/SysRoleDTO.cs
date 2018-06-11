using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SysRoleDTO:BaseEntityDTO
    {
        public string Name { get; set; }

        public string CreateTime { get; set; }

        public string Creater { get; set; }

        public string Description { get; set; }

        public string UserName { get; set; }
    }
}

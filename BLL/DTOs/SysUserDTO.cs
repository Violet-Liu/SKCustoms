using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SysUserDTO:BaseEntityDTO
    {
        public string Pwd { get; set; }

        public string Name { get; set; }

        public string TrueName { get; set; }

        public string Sex { get; set; }

        public string CreateTime { get; set; }

        public string Creater { get; set; }

        public string Position { get; set; }

        public string Remark { get; set; }

        public string Contact { get; set; }

        public string State { get; set; } = "1";

        public string RoleName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SysUserRightView
    {
        public int SysModuleId { get; set; }

        public string ModuleName { get; set; }

        public string KeyCode { get; set; }

        public string Name { get; set; }

        public int IsValid { get; set; }

        public int ParentId { get; set; }


    }



}

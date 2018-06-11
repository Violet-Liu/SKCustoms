using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services
{

    public class Resp_Binary
    {
        public int flag { get; set; }

        public string message { get; set; }

        public static Resp_Binary Add_Failed
        {
            get => new Resp_Binary { message = "添加失败" };
        }

        public static Resp_Binary Modify_Failed
        {
            get => new Resp_Binary { message = "修改失败" };
        }

        public static Resp_Binary Del_Failed
        {
            get => new Resp_Binary { message = "删除失败" };
        }


        public static Resp_Binary Add_Sucess
        {
            get => new Resp_Binary { flag = 1, message = "添加成功" };
        }

        public static Resp_Binary Modify_Sucess
        {
            get => new Resp_Binary { flag = 1, message = "修改成功" };
        }

        public static Resp_Binary Del_Sucess
        {
            get => new Resp_Binary { flag = 1, message = "删除成功" };
        }

    }

    public class Resp_Login
    {
        public int flag { get; set; }

        public string message { get; set; }
        public SysUserDTO user { get; set; }
        public List<SysModule2DTO> sysmodules { get; set; }
    }

    public class Resp_Login_Index
    {
        public List<SysModule2DTO> sysmodules { get; set; }
    }

    public class Resp_Query<T>
    {
        public List<T> entities { get; set; }

        public int totalCounts { get; set; }

        public int totalRows { get; set; }
    }


    public class Resp_SysModuleOperate: Resp_Binary
    {
        public Resp_Query<SysModuleOperateDTO> datas { get; set; }
    }


    public class SysRightViewModel
    {
        public int SysModuleId { get; set; }

        public string ModuleName { get; set; }
        public int ParentId { get; set; }

        public List<SysRightOperateViewModel> Operaties { get; set; } = new List<SysRightOperateViewModel>();
    }

    public class SysRightOperateViewModel
    {

        public string KeyCode { get; set; }

        public string Name { get; set; }

        public int IsValid { get; set; }
    }

    public class Resp_Index<T>
    {
        public bool allowVisit { get; set; }

        public string message { get; set; }


        public Resp_Query<T> query { get; set; }

        public List<SysModuleOperateIndexDTO> moduleOperaties { get; set; }

    }

    public class Resp_RightOperator_Index
    {
        public bool allowVisit { get; set; }

        public string message { get; set; }


        public List<SysRoleDTO> sysRoles { get; set; }

        public List<SysModuleDTO> sysModules { get; set; }

        public List<SysModuleOperateIndexDTO> moduleOperaties { get; set; }
    }


    public class Resp_SysErrorLog_Index : Resp_Index<SysErrorLogDTO>
    {
        public List<string> errorTypes { get; set; }
    }

    public class Resp_Temporary_Sync
    {
        public string lastSyncTime { get; set; }

        public List<Layout> entities { get; set; } = new List<Layout>();
    }

    public class Resp_All_Sync
    {
        public string lastSyncTime { get; set; }
        public bool done { get; set; }
        public Resp_Query<Layout> batchDatas { get; set; }
    }

    public class Resp_CheckExsits<T>
    {
        public int flag { get; set; }
        public string message { get; set; }

        public T entity { get; set; }
    }

    public class Resp_Binary_Member<T>
    {
        public int flag { get; set; }
        public string message { get; set; }
        public string LetterCode { get; set; }
        public T entity { get; set; }
    }


}
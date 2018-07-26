using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Login
    {
        public string userName { get; set; }
        public string passWord { get; set; }
    }

    public class Base_Del_Request
    {
        public int Id { get; set; }
        public List<int> Ids { get; set; }
    }

    public class Base_SingleDel
    {
        public int Id { get; set; }
    }

    public class Base_Page_Request
    {
        public int PgIndex { get; set; } = 1;

        public int PgSize { get; set; } = 10;

        public string Order { get; set; }

        public bool Esc { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        /// <summary>
        /// 请求校验
        /// </summary>
        public void Verify()
        {
            if (PgIndex < 1)
                PgIndex = 1;
            if (PgSize < 1)
                PgSize = 10;
        }

    }

    public class RecordManager_Query : Base_Page_Request
    {
        public string CarNumber { get; set; }

        public string TLicense { get; set; }

        public string DLicense { get; set; }

        public string Driver { get; set; }

        public string Contact { get; set; }

        public string IsValid { get; set; }

        public string CarColor { get; set; }

        public string CarType { get; set; }

        public string RecordMGrade { get; set; }

        public string Channel { get; set; }
    }

    public class Capture_Query : Base_Page_Request
    {
        public string CarNumber { get; set; }

        public string ParkId { get; set; }

        public string Channel { get; set; }

        public string Pass { get; set; }

        public int OnlyIn { get; set; }

        public string StayHours { get; set; }
    }

    public class Layout_Valid_Set
    {
        public int[] Ids { get; set; }
        public int Valid { get; set; }
    }

    public class Layout_Query:Base_Page_Request
    {
        public string CarNumber { get; set; }

        public string IsValid { get; set; }

        public string Trigger { get; set; }

        public string Channel { get; set; }
    }

    public class Alarm_Query : Base_Page_Request
    {
        public string CarNumber { get; set; }

        public string IsDeal { get; set; }

        public string AlarmBeginTime { get; set; }

        public string AlarmEndTime { get; set; }

        public string HandlerBeginTime { get; set; }

        public string HandlerEndTime { get; set; }

        public string Channel { get; set; }
    }

    public class Alarm_Check
    {
        public int ID { get; set; }
        public string CarNumber { get; set; }

        public int IsDeal { get; set; }

        public string Operator { get; set; }

        public string Message { get; set; }

        public string HandlerTime { get; set; } = DateTime.Now.ToString();
    }


    public class SysUser_Query:Base_Page_Request
    {
        public string QueryStr { get; set; }   
        
        public string State { get; set; }
    }

    public class File_Import
    {
        public string FileData { get; set; }

        public string FileName { get; set; }

        public int UserId { get; set; }
    }


    public class SysUser_Reset_Pwd
    {
        public int Id { get; set; }

        public string Pwd { get; set; }
    }

    /// <summary>
    /// 分配用户
    /// </summary>
    public class SysUser_Assign_Roles
    {

        public int UserId { get; set; }

        public List<int> RoleIds { get; set; }
    }

    public class SysUser_Assign_Channels
    {

        public int UserId { get; set; }

        public List<int> ChannelIds { get; set; }
    }

    public class SysRole_Query : Base_Page_Request
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public string QueryStr { get; set; }
    }

    public class SysRole_Assign_Users
    {
        public int RoleId { get; set; }
        
        public List<int> UserIds { get; set; }
    }

    public class SysChannel_Assign_Users
    {
        public int ChannelId { get; set; }

        public List<int> UserIds { get; set; }
    }


    public class SysMoudule_Query
    {
        public string ParentId { get; set; }
    }

    public class SysModule_Add_Btn
    {
        public int ModuleId { get; set; }

        public string KeyCode { get; set; }

        public string KeyName { get; set; }

        public int Sort { get; set; }
        
        public int IsValid { get; set; }
    }

    public class SysModuleOperate_Query
    {
        public int ModuleId { get; set; }

    }

    public class SysRightOperate_Update
    {
        public List<SysModuleOperateDTO> ModuleOperateIds { get; set; }

        public int RoleId { get; set; }

        public int ModuleId { get; set; }
        
    }

    public class SysRightOperate_Get
    {
        public int RoleId { get; set; }

        public int ModuleId { get; set; }
    }

    public class SysRightGetByUser
    {
        public int PgIndex { get; set; }
        public int PgSize { get; set; }
        public int UserId { get; set; }

        public int RoleId { get; set; }
    }

    public class SysErrorLog_Query: Base_Page_Request
    {
        public string ErrorType { get; set; }

    }

    public class Req_Index
    {
        public int userId { get; set; }

        public int moduleId { get; set; }

        public int PgSize { get; set; }
    }

    public class Req_ResetPwd
    {
        public string userName { get; set; }

        public string oldPwd { get; set; }

        public string NewPwd { get; set; }
    }


    public class Req_Temporary_Sync
    {
        public string lastSyncTime { get; set; }
    }

    public class Req_All_Sync
    {
        public int PgIndex { get; set; }
    }

    public class RMG_Query:Base_Page_Request
    {
        public string QueryStr { get; set; }
    }

    public class Layout_Hit
    {
        public string CarNumber { get; set; }
        public string CarType { get; set; }

        public string CarColor { get; set; }

        public int Pass { get; set; }

        public string Channel { get; set; }
    }

    public class Req_CheckExsits
    {
        public string CarNumber { get; set; }

        public string Channel { get; set; }
    }


    public class Req_RecordManager_Add
    {
        public RecordManagerDTO entity { get; set; }

        public List<string> channels { get; set; }
    }

    public class Req_Layout_Add
    {
        public LayoutDTO entity { get; set; }

        public List<string> channels { get; set; }
    }

    public class Layout_Random_Set
    {
        public int Pass { get; set; }

        public float Percent { get; set; }

        public bool IsOpen { get; set; }

        public int ValidCount { get; set; }

        public int ValidDays { get; set; }

        public string Channel { get; set; } = "";
    }
}

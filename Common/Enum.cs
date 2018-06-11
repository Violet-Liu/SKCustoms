using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum SysErrorType
    {
        [Description("系统错误")]
        system,
        [Description("批量插入")]
        bulkInsert,
        [Description("第三方对接")]
        docking,
    }

    public enum Layout_Degree
    {
        无数次,
        一次
    }

    public enum Layout_TriggerType
    {
        出场控制,
        进场控制,
        进出场控制,
    }
}

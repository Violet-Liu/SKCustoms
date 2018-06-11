using System;
using System.Collections.Generic;
using System.Text;

namespace MessageMQ.Adapter
{
    public enum MsgSendType:int
    {
        /// <summary>
        ///  短信
        /// </summary>
        S,
        /// <summary>
        /// 邮件
        /// </summary>
        M,
        /// <summary>
        /// 推送
        /// </summary>
        P
    }
}

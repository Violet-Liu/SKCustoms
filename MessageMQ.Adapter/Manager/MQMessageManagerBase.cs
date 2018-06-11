using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageMQ.Adapter
{
    public abstract class MQMessageManagerBase
    {
        /// <summary>
        ///     The address
        /// </summary>
        protected static string mqAddress = MyMQConfig.MQIpAddress;

        /// <summary>
        ///     The queu e_ destination
        /// </summary>
        protected static string QUEUE_DESTINATION = MyMQConfig.QueueDestination;

        /// <summary>
        /// The log
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger("ActiveMQ");
    }
}

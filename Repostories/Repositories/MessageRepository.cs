using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Domain;
using MessageMQ.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Repostories
{
    public class MessageRepository : IMessageRepository
    {
        public bool SendMessage(string message)
        {
            IConnectionFactory factory = new ConnectionFactory(ConfigPara.MQIdaddress);
            using (IConnection connection=fac)
        }

    }
}

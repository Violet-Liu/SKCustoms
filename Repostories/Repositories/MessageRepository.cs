using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Domain;
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
        public void SendMessage(string message, string filter = "")
        {
            IConnectionFactory factory = new ConnectionFactory(ConfigPara.MQIdaddress);
            using (IConnection connection = factory.CreateConnection())
            {
                using (ISession session = connection.CreateSession())
                {
                    IMessageProducer prod = session.CreateProducer(
                        new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(ConfigPara.Destination));
                    prod.DisableMessageTimestamp = true;
                    ITextMessage msg = prod.CreateTextMessage();
                    if (!string.IsNullOrEmpty(filter))
                        msg.Properties.SetString("channel", filter);
                    msg.Text = message;
                    prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, new TimeSpan(600000000));
                }
            }
        }

    }
}

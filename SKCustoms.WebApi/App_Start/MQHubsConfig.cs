using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageMQ.Adapter;
using Common;
using Microsoft.AspNet.SignalR;
using log4net;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading;
using System.Threading.Tasks;

namespace SKCustoms.WebApi
{
    public class MQHubsConfig
    {
        private static readonly ILog logger = LogManager.GetLogger("ActiveMQ");
        private static object lockObj = new object();
        private static IHubCallerConnectionContext<dynamic> _clients;
        public static IHubCallerConnectionContext<dynamic> Clients
        {
            get { return _clients; }
            set
            {
                lock(lockObj)
                {
                    _clients = value;
                }
            }
        }



        public static void Run()
        {
            logger.Info("注册监听MQ事件");
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //Create the Connection factory
                    IConnectionFactory factory = new ConnectionFactory(ConfigPara.MQIdaddress);
                    //Create the connection
                    using (Apache.NMS.IConnection connection = factory.CreateConnection())
                    {
                        connection.ClientId = "MQtesting listener";
                        connection.Start();
                        //Create the Session
                        using (ISession session = connection.CreateSession())
                        {
                            IMessageConsumer consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("MQMessage"), "webapi listener", null, false);
                            while (true)
                            {
                                ITextMessage msg = (ITextMessage)consumer.Receive(new TimeSpan(10000));
                                if (msg != null)
                                {
                                    logger.Info(msg.Text);
                                    if (Clients != null)
                                    {
                                        Clients.All.broadcastMessage(msg.Text);
                                    }
                                }
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
                catch (System.Exception e)
                {
                    logger.Info(e.Message);
                }
            });

            logger.Info("监听完成");
        }
    }
}
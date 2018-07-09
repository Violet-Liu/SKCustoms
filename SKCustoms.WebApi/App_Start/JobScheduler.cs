using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Unity.Attributes;
using Services;
using log4net;
using Repostories;

namespace SKCustoms.WebApi
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler;
            //调度器工厂
            ISchedulerFactory factory;

            //创建一个调度器
            factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler();
            scheduler.GetJobGroupNames();
            IJobDetail job = JobBuilder.Create<TimeJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithCronSchedule("0 0 0,8,12,21 * * ?")    //每日8点和11点执行一次
                .Build();
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
        }
    }


    public class TimeJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger("TaskQuartZ");

        public void Execute(IJobExecutionContext context)
        {
            SetDataUnValidByValidTime();
        }

        public async Task<int> SetDataUnValidByValidTime()
        {
            int num = 0;
            try
            {
                logger.Info("定时设置数据无效运行" + DateTime.Now);
                using (var context = new SKContext())
                {
                    num += ((IQueryableUnitOfWork)context).ExecuteCommand($"update layout set IsValid=0 where IsValid=1 and ValideTime<now() and ValideTime>'1977-01-01 00:00:00';");
                    num += ((IQueryableUnitOfWork)context).ExecuteCommand($"update recordmanager set IsValid=0 where IsValid=1 and ValideTime<now() and ValideTime>'1977-01-01 00:00:00';");
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message.ToString());
                num = -1;
            }
            
            return await Task.FromResult(num);

        }
    }

}
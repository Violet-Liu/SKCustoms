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
using Common;

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
                .WithCronSchedule("0 0 0/1 * * ?  ")    //每日8点和11点执行一次 //0 0/1 * * * ? //0 0 0,8,11,15,18,21 * * ?
                .Build();
            scheduler.ScheduleJob(job, trigger);


            IJobDetail job2 = JobBuilder.Create<SlowJob>().Build();
            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group1")
                .WithCronSchedule("0 0 0/1 * * ?  ")    //每日8点和11点执行一次 //0 0/1 * * * ? //0 0 0,8,11,15,18,21 * * ?
                .Build();
            scheduler.ScheduleJob(job2, trigger2);


            scheduler.Start();
        }
    }


    public class SlowJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            CaptureHistoryHandler();
        }

        public async  Task<int> CaptureHistoryHandler()
        {

            using (var context = new SKContext())
            {
                try
                {
                    ((IQueryableUnitOfWork)context).ExecuteCommand("Delete from  capture where CreateTime< date_sub(now(),interval 180 day);");
                }
                catch (Exception ex)
                {

                }
            }

            return await Task.FromResult(1);
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

                    foreach (var item in LayoutRandomSetLock.layoutRs.Values)
                    {
                        if (item.IsOpen && !string.IsNullOrEmpty(item.Channel))
                        {
                            var flags = true;
                            var i = 0;
                            while (flags)
                            {
                                i++;
                                var per = (int)Math.Ceiling(100 / item.RPercent);
                                var caps = context.Captures.Where(t => t.Channel == item.Channel && t.CreateTime > DateTime.Today);

                                if (item.Pass < 2)
                                    caps = caps.Where(t => t.Pass == item.Pass);

                                caps = caps.OrderBy(t => t.ID).Skip((per - 1) * i).Take(1);

                                var cap = caps.FirstOrDefault();
                                if (cap.IsNotNull())
                                {
                                    var layout = context.Layouts.FirstOrDefault(t => t.CarNumber == cap.CarNumber && t.IsValid == 1 && t.TriggerType == item.Pass);
                                    if (layout.IsNull())
                                    {
                                        context.Layouts.Add(new Domain.Layout
                                        {
                                            CarNumber = cap.CarNumber,
                                            SysUserId = 1,
                                            Description = "随机取样",
                                            CreateTime = DateTime.Now,
                                            TriggerType = item.Pass,
                                            IsValid = 1,
                                            Degree = item.ValidCount,
                                            ValideTime = DateTime.Now.AddDays(item.ValidDays),
                                            Channel = cap.Channel

                                        });
                                    }
                                }
                                else
                                    flags = false;

                            }
                            context.SaveChanges();
                        }
                    }

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
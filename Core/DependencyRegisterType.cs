using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Repostories;
using Services;
using Domain;
using Infrastruture;
using Repostories.Repositories;

namespace Core
{
    public class DependencyRegisterType
    {
        public static void Container_Sys(ref UnityContainer container)
        {
           //container.RegisterType<IQueryableUnitOfWork, SKContext>();

            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IAccountRepository, AccountRepostory>();

            container.RegisterType<ISysErrorLogRepository, SysErrorLogRepository>();
            container.RegisterType<ISysErrorLogService, SysErrorLogService>();

            container.RegisterType<IRecordManagerService, RecordManagerService>();
            container.RegisterType<IRecordManagerRepository, RecordManagerRepository>();

            container.RegisterType<ICaptureService, CaptureService>();
            container.RegisterType<ICaptureRepository, CaptureRepository>();

            container.RegisterType<ILayoutRepository, LayoutRepository>();
            container.RegisterType<ILayoutService, LayoutService>();

            container.RegisterType<IAlarmRepository, AlarmRepository>();
            container.RegisterType<IAlarmService, AlarmService>();

            container.RegisterType<ISysUserService, SysUserService>();
            container.RegisterType<ISysUserRepository, SysUserRepository>();

            container.RegisterType<ISysRoleService, SysRoleService>();
            container.RegisterType<ISysRoleRepository, SysRoleRepository>();

            container.RegisterType<ISysModuleOperateService, SysModuleOperateService>();
            container.RegisterType<ISysModuleOperateRepository, SysModuleOperateRepository>();
            container.RegisterType<ISysModuleRepository, SysModuleRepository>();
            container.RegisterType<ISysModuleService, SysModuleService>();


            container.RegisterType<ISysRightOperateRepository, SysRightOperateRepository>();
            container.RegisterType<ISysRightRepository, SysRightRepository>();
            container.RegisterType<ISysRightService, SysRightService>();
            container.RegisterType<ISysRightOperatorService, SysRightOperatorService>();

            container.RegisterType<ISysErrorLogRepository, SysErrorLogRepository>();
            container.RegisterType<ISysErrorLogService, SysErrorLogService>();

            container.RegisterType<IRecordMGradeRepository, RecordMGradeRepository>();
            container.RegisterType<IRecordMGradeService, RecordMGradeService>();

            container.RegisterType<ICarTypeRepository, CarTypeRepository>();
            container.RegisterType<ICarTypeService, CarTypeService>();


            container.RegisterType<IMessageRepository, MessageRepository>();

            container.RegisterType<ICarColorRepository, CarColorRepository>();
            container.RegisterType<ICarColorService, CarColorService>();

            container.RegisterType<ISysChannelRepository, SysChannelRepository>();
            container.RegisterType<ISysChannelService, SysChannelService>();
        }
    }
}
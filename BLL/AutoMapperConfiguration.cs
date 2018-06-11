using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;


namespace Services
{
    public class AutoMapperConfiguration
    {
        public static void ConfigurationAutoMapper()
        {
            DateTime? dt = null;
            var minDt = DateTime.MinValue;
            Mapper.Initialize(cfg=> {
                cfg.CreateMap<SysUserConfig, SysUserConfigDTO>();
                cfg.CreateMap<SysUserConfigDTO, SysUserConfig>();

                cfg.CreateMap<SysUser, SysUserDTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString()))
                    .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.ToString()))
                    .ForMember(dest=>dest.State,opt=>opt.MapFrom(src=>src.State.ToString()));
                cfg.CreateMap<SysUserDTO, SysUser>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToInt()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToDateTime()))
                    .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.ToInt()))
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToInt()));


                cfg.CreateMap<RecordManager, RecordManagerDTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ((RecordManagerTypeEnum)src.Type).ToString()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString()))
                    .ForMember(dest => dest.SysUserId, opt => opt.MapFrom(src => src.SysUserId.ToString()))
                    .ForMember(dest => dest.LastInTime, opt => opt.MapFrom(src => src.LastInTime > minDt ? src.LastInTime.ToString() : ""))
                    .ForMember(dest => dest.LastOutTime, opt => opt.MapFrom(src => src.LastOutTime > minDt ? src.LastOutTime.ToString() : ""))
                    .ForMember(dest => dest.ValideTime, opt => opt.MapFrom(src => src.ValideTime > minDt ? src.ValideTime.ToString() : ""))
                    .ForMember(dest => dest.RecordMGrade, opt => opt.MapFrom(src => src.RecordMGrade));

                cfg.CreateMap<RecordManagerDTO, RecordManager>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToLong()))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(RecordManagerTypeEnum), src.Type)))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToDateTime()))
                    .ForMember(dest => dest.SysUserId, opt => opt.MapFrom(src => src.SysUserId.ToInt()))
                    .ForMember(dest=>dest.LastInTime,opt=>opt.MapFrom(src=>string.IsNullOrEmpty(src.LastInTime)? dt : src.LastInTime.ToDateTime()))
                    .ForMember(dest=>dest.LastOutTime,opt=>opt.MapFrom(src=>string.IsNullOrEmpty(src.LastOutTime)?dt:src.LastOutTime.ToDateTime()))
                    .ForMember(dest=>dest.ValideTime,opt=>opt.MapFrom(src=>string.IsNullOrEmpty(src.ValideTime)?dt:src.ValideTime.ToDateTime()));

                cfg.CreateMap<Capture, CaptureDTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.Pass, opt => opt.MapFrom(src => src.Pass.ToString()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString()))
                    .ForMember(dest => dest.StayTime, opt => opt.MapFrom(src => src.Pass == 1 && src.WithOut == 0 ? Utils.GetFriendlyTime(src.CreateTime) : ""))
                    .ForMember(dest => dest.IsAlarm, opt => opt.Ignore());

                cfg.CreateMap<CaptureDTO, Capture>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToLong()))
                    .ForMember(dest => dest.Pass, opt => opt.MapFrom(src => src.Pass.ToInt()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToDateTime()));

                cfg.CreateMap<Layout,LayoutDTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString()))
                    .ForMember(dest => dest.SysUserId, opt => opt.MapFrom(src => src.SysUserId.ToString()))
                    .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => src.IsValid.ToString()))
                    .ForMember(dest=>dest.ValideTime,opt=>opt.MapFrom(src=> src.ValideTime > minDt ? src.ValideTime.ToString() : ""));
                    

                cfg.CreateMap<LayoutDTO,Layout>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToLong()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToDateTime()))
                    .ForMember(dest => dest.SysUserId, opt => opt.MapFrom(src => src.SysUserId.ToInt()))
                    .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => src.IsValid))
                    .ForMember(dest => dest.ValideTime, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ValideTime) ? dt : src.ValideTime.ToDateTime()));

                cfg.CreateMap<Alarm, AlarmDTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.CaptureId, opt => opt.MapFrom(src => src.CaptureId.ToString()))
                    .ForMember(dest => dest.AlarmTime, opt => opt.MapFrom(src => src.AlarmTime.ToString()))
                    .ForMember(dest => dest.HandlerTime, opt => opt.MapFrom(src => src.HandlerTime > minDt ? src.HandlerTime.ToString() : ""))
                        .ForMember(dest => dest.IsDeal, opt => opt.MapFrom(src => src.IsDeal.ToString()))
                        .ForMember(dest => dest.LetterCode, opt => opt.MapFrom(src => AESEncryptHelper.Decrypt(src.LetterCode)));

                cfg.CreateMap<AlarmDTO, Alarm>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToLong()))
                    .ForMember(dest => dest.CaptureId, opt => opt.MapFrom(src => src.CaptureId.ToLong()))
                    .ForMember(dest => dest.AlarmTime, opt => opt.MapFrom(src => src.AlarmTime.ToDateTime()))
                    .ForMember(dest => dest.HandlerTime, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.HandlerTime) ? dt : src.HandlerTime.ToDateTime()));

                cfg.CreateMap<SysRoleDTO, SysRole>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToInt()))
                    .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.ToInt()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToDateTime()));

                cfg.CreateMap<SysRole, SysRoleDTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.ToString()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString()));

                cfg.CreateMap<SysModule, SysModuleDTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.ToString()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString()))
                    .ForMember(dest => dest.Enable, opt => opt.MapFrom(src => ((State)src.Enable).ToString()))
                    //.ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId.ToString()))
                    .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Sort.ToString()));

                cfg.CreateMap<SysModuleDTO, SysModule>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToInt()))
                    .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.ToInt()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToDateTime()))
                    .ForMember(dest => dest.Enable, opt => opt.MapFrom(src => src.Enable.ToInt()))
                    //.ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId.ToInt()))
                    .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Sort.ToInt()));

                cfg.CreateMap<SysModuleOperate, SysModuleOperateDTO>()
                    .ForMember(dest => dest.SysRightOperateId, opt => opt.Ignore())
                    .ForMember(dest => dest.SysModuleOperateId, opt => opt.Ignore())
                    .ForMember(dest => dest.SysRightId, opt => opt.Ignore())
                    .ForMember(dest => dest.IsRightValid, opt => opt.Ignore())
                    .ForMember(dest => dest.KeyName, opt => opt.MapFrom(src => src.Name));

                cfg.CreateMap<SysModuleOperateDTO, SysModuleOperate>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.KeyName))
                    .ForMember(dest => dest.SysModule, opt => opt.Ignore())
                    .ForMember(dest => dest.SysRightOperates, opt => opt.Ignore());

                cfg.CreateMap<SysErrorLog, SysErrorLogDTO>()
                    .ForMember(dest => dest.ErrTime, opt => opt.MapFrom(src => src.ErrTime.ToString()));


                cfg.CreateMap<SysErrorLogDTO, SysErrorLog>()
                    .ForMember(dest => dest.ErrTime, opt => opt.MapFrom(src => src.ErrTime.ToDateTime()));

                cfg.CreateMap<SysUserRightView, SysModuleOperateIndexDTO>()
                    .ForMember(dest => dest.KeyCode, opt => opt.MapFrom(src => src.KeyCode))
                    .ForMember(dest => dest.KeyName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => src.IsValid));

                cfg.CreateMap<SysModule, SysModule2DTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.ToString()))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString()))
                    .ForMember(dest => dest.Enable, opt => opt.MapFrom(src => ((State)src.Enable).ToString()))
                    //.ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId.ToString()))
                    .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Sort.ToString()))
                    .ForMember(dest => dest.SubSysModules, opt => opt.Ignore());



                cfg.CreateMap<SysRightOperate, SysRightOperateDTO>();
                cfg.CreateMap<SysRightOperateDTO, SysRightOperate>();

            });
        }
    }
}

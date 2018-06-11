using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repostories;
using Unity.Attributes;
using Common;
using Infrastructure;
using Services.Mapping;

namespace Services
{
    public partial class SysErrorLogService : ISysErrorLogService
    {
        [Dependency]
        public SysErrorLogRepository _sysErrorLogRepository { get; set; }


        [Dependency]
        public SysRightRepository _sysRightRepository { get; set; }


        public void Insert(SysErrorLog log)
        {
            using (SKContext db = new SKContext())
            {
                db.SysErrorLogs.Add(log);
                db.SaveChanges();
            }
        }

        public Resp_Query<SysErrorLogDTO> Query(SysErrorLog_Query request)
        {
            var response = new Resp_Query<SysErrorLogDTO>();

            var records = _sysErrorLogRepository.GetAll();

            records.ToMaybe()
                .Do(d => request.Verify())
                .DoWhen(t => !string.IsNullOrEmpty(request.ErrorType), d => records = records.Where(s => s.ErrType.Equals(request.ErrorType)));

            if (!string.IsNullOrEmpty(request.BeginTime) && DateTime.TryParse(request.BeginTime, out DateTime start))  //Linq to entity 不支持datatime.parse函数
            {
                records = records.Where(s => s.ErrTime >= start);
            }

            if (!string.IsNullOrEmpty(request.EndTime) && DateTime.TryParse(request.EndTime, out DateTime end))
            {
                records = records.Where(s => s.ErrTime <= end);
            }
            response.totalCounts = records.Count();
            response.totalRows = (int)Math.Ceiling((double)response.totalCounts / request.PgSize);

            if (!string.IsNullOrEmpty(request.Order))
            {
                records = records.DataSorting(request.Order, request.Esc);
            }
            else
            {
                records = records.OrderByDescending(t => t.ID);
            }
            var entities = records.Skip(request.PgSize * (request.PgIndex - 1)).Take(request.PgSize).ToList();
            response.entities = entities.ConvertoDto<SysErrorLog, SysErrorLogDTO>().ToList();

            return response;

        }

        public Resp_SysErrorLog_Index Index(Req_Index request)
        {
            var response = new Resp_SysErrorLog_Index();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (!limits.IsNullOrEmpty() && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.errorTypes = Enum.GetNames(typeof(SysErrorType)).ToList();
                response.moduleOperaties = limits;
                var query_parameter = new SysErrorLog_Query { PgIndex = 1, PgSize = 20 };
                response.query = Query(query_parameter);
            }
            else
            {
                _sysRightRepository.UnitOfWork.Commite();
                response.allowVisit = false;
                response.message = "无访问权限，请联系管理员";
            }

            return response;

        }
    }
}

using Common;
using Domain;
using Infrastructure;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace Services
{
    public class SysUserConfigService
    {
        [Dependency]
        private  ISysUserConfigRepository _sysUserConfigRepository {get;set;}

        public virtual IEnumerable<SysUserConfigDTO> GetList(ref GridPager pager, string queryStr)
        {
            IQueryable<SysUserConfig> queryData = null;

            if (!String.IsNullOrEmpty(queryStr))
                queryData = _sysUserConfigRepository.GetByWhere(d => d.Name.Contains(queryStr) || d.Value.Contains(queryStr) || d.Value.Contains(queryStr)
                                  || d.Type.Contains(queryStr));
            else
                queryData = _sysUserConfigRepository.GetAll();

            pager.TotalRows = queryData.Count();

            return queryData.SortingAndPaging(pager.Sort, pager.Order, pager.Pg_Index, pager.Pg_Size).ToList().ConvertoDto<SysUserConfig,SysUserConfigDTO>().ToList();

        }


        public virtual bool Create(ref ValidationErrors errors, SysUserConfig model)
        {
            try
            {
                var entity = _sysUserConfigRepository.GetById(model.ID);

                if (null != entity)
                {
                    errors.Add(ErrorMessage.PrimaryRepeat);
                }
                entity = new SysUserConfig
                {
                    ID = model.ID,
                    Name = model.Name,
                    Value = model.Value,
                    Type = model.Type,
                    State = model.State,
                    SysUserId = model.SysUserId
                };

                _sysUserConfigRepository.Insert(entity);

                if (_sysUserConfigRepository.UnitOfWork.Commite() > 0)
                {
                    return true;
                }
                else
                {
                    errors.Add(ErrorMessage.InsertFail + $"entity:\n\n{entity.ToJson()}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                //ExceptionHander.WriteException(ex);
                return false;
            }
        }
    }
}

using Common;
using Model;
using Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace DLL.Sys
{
    public class SysUserConfigRepository : Repository<SysUserConfig>, ISysUserConfigRepository
    {
        public SysUserConfigRepository(IQueryableUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public virtual List<SysUserConfig> GetList(ref GridPager pager,string queryStr)
        {
            IQueryable<SysUserConfig> queryData = null;

            if(!String.IsNullOrEmpty(queryStr))
                queryData = base.GetByWhere(d => d.Name.Contains(queryStr) || d.Value.Contains(queryStr) || d.Value.Contains(queryStr)
                                  || d.Type.Contains(queryStr));
            else
                queryData = base.GetAll();

            pager.totalRows = queryData.Count();

            return queryData.SortingAndPaging(pager.sort, pager.order, pager.pg_index, pager.pg_size).ToList();

        }

        public virtual bool Create(ref )
    }
}

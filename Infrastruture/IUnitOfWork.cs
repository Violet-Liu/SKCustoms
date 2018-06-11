using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture
{
    public interface IUnitOfWork:IDisposable
    {

        /// <summary>
        /// 相当于Savechanges操作
        /// </summary>
        int Commite();
        /// <summary>
        /// 设置Changetracker中所有实体为UnChanged状态
        /// </summary>
        void RollBackChanges();
    }
}

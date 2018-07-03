using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class LayoutRepository : Repository<Layout>, ILayoutRepository
    {
        public LayoutRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }

        public int SetInValid()
        {
            return
            ((IQueryableUnitOfWork)UnitOfWork).ExecuteCommand($"update layout set IsValid=0 where ValideTime<now() and ValideTime>'1977-01-01 00:00:00';");
        }
    }
}

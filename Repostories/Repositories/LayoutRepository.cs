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
    }
}

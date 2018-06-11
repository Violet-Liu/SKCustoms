using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories.Repositories
{
    public class CarTypeRepository : Repository<CarType>, ICarTypeRepository
    {
        public CarTypeRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}

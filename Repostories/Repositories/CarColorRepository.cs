using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories.Repositories
{
    public class CarColorRepository : Repository<CarColor>, ICarColorRepository
    {
        public CarColorRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class CaptureRepository : Repository<Capture>, ICaptureRepository
    {
        public CaptureRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}

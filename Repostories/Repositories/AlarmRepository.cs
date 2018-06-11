using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class AlarmRepository : Repository<Alarm>, IAlarmRepository
    {
        public AlarmRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}

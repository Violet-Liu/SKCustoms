using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class RecordMGradeRepository : Repository<RecordMGrade>, IRecordMGradeRepository
    {
        public RecordMGradeRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}

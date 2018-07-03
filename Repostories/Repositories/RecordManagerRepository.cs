using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class RecordManagerRepository : Repository<RecordManager>, IRecordManagerRepository
    {
        
        public RecordManagerRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }

        public void DelRepeatRecord()
        {
            var sql =
            ((IQueryableUnitOfWork)UnitOfWork).ExecuteCommand($@"set SQL_SAFE_UPDATES = 0;
                                                                    Delete from recordmanager where id in(
                                                                    select a.id FROM 
                                                                    (SELECT id FROM recordmanager WHERE CarNumber in (select CarNumber FROM recordmanager group by CarNumber having count(CarNumber)>1) 
                                                                    and id not in (select max(id) from recordmanager group by CarNumber having count(CarNumber)>1)) AS a);");
        }

        public int SetInValid()
        {
            return 
            ((IQueryableUnitOfWork)UnitOfWork).ExecuteCommand($"update recordmanager set IsValid=0 where ValideTime<now() and ValideTime>'1977-01-01 00:00:00';");
        }
    }
}

using Domain;
using Domain.Items.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories.Repositories
{
    public class CaptureBackupRepository : Repository<CaptureBackup>, ICaptureBackupRepository
    {
        public CaptureBackupRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}

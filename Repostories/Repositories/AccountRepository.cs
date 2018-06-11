using Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repostories
{
    public class AccountRepostory : IAccountRepository,IDisposable
    {

        private SKContext _unitOfWork;


        public AccountRepostory(SKContext context) => _unitOfWork = context;

        public void Dispose()
        {
            if (null == _unitOfWork)
                _unitOfWork.Dispose();
        }

        public SysUser Login(string name, string pwd) => _unitOfWork.SysUsers.SingleOrDefault(u => u.Name == name && u.Pwd == pwd);
    }
}

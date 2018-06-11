using Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DLL
{
    public class AccountRepository : IAccountRepository,IDisposable
    {

        private SKContext _unitOfWork;


        public AccountRepository(SKContext context) => _unitOfWork = context;

        public void Dispose()
        {
            if (null == _unitOfWork)
                _unitOfWork.Dispose();
        }

        public SysUser Login(string name, string pwd) => _unitOfWork.SysUser.SingleOrDefault(u => u.Name == name && u.Pwd == pwd);
    }
}

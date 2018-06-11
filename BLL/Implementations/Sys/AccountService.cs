using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repostories;
using Unity.Attributes;
using Domain;
using Services;
using Services.Mapping;
using Common;

namespace Services
{
    public partial class AccountService : IAccountService
    {
        [Dependency]
        public IAccountRepository _accountRepository { get; set; }

        [Dependency]
        public ISysUserRepository _sysUserRepository { get; set; }

        //[Dependency]
        //public ISysRightRepository _sysRightRepository { get; set; }
        public SysUserDTO Login(string name, string pwd) => _accountRepository.Login(name, pwd).ConvertoDto<SysUser,SysUserDTO>();


        public Resp_Binary ResetPwd(Req_ResetPwd request)
        {
            
            var user = _sysUserRepository.GetByWhere(t => t.Name == request.userName & t.Pwd == request.oldPwd).FirstOrDefault();
            if (user.IsNull())
            {
                var resp_binary = new Resp_Binary { message = "无效的旧密码" };
                return resp_binary;
            }
            user.Pwd = request.NewPwd;
            _sysUserRepository.Update(user);
            if(_sysUserRepository.UnitOfWork.Commite()>0)
            {
                return Resp_Binary.Modify_Sucess;
            }

            return Resp_Binary.Modify_Failed;
        }

    }
}

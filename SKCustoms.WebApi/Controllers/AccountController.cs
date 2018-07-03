using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Services;
using Common;
using System.Web.Http.Results;
using Unity.Attributes;
using Repostories;
using System.Web;
using System.Web.Security;
using System.Web.Http.Cors;
using System.Text;
using System.Net.Http.Headers;
using Domain;
using Services.Mapping;

namespace SKCustoms.WebApi.Controllers
{
    
    [RoutePrefix("account/SysUser")]
    public class AccountController : ApiController
    {
        [Dependency]
        public IAccountService _accountService { get; set; }

        [Dependency]
        public ISysRightService _sysRightService { get; set; }


        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(Login request)
        {
            var resp_binary = new Resp_Login();
            var user = _accountService.Login(request.userName, request.passWord);
            if (user.IsNull())
            {
                resp_binary.message = "用户名或密码错误";
            }
            else if (user.State.ToInt() != 1)
            {
                resp_binary.message = "无效的用户";
            }
            else
            {
                resp_binary.flag = 1;
                resp_binary.message = "登录成功";
                resp_binary.user = user;

            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp_binary.ToJson(), Encoding.UTF8, "application/json");

            if (user.IsNotNull())
            {
                var userInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes($"userId={user.ID}&userName={user.Name}"));
                var cookie = new CookieHeaderValue("userInfo", userInfo);
                cookie.Expires = DateTimeOffset.Now.AddDays(365);
                cookie.Domain = Request.RequestUri.Host;
                cookie.Path = "/";
                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            }
            return response;

        }

        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage LogOut()
        {
            var resp_binary = new Resp_Binary { flag = 1 };

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp_binary.ToJson(), Encoding.UTF8, "application/json");
            var cookie = new CookieHeaderValue("userInfo", "");
            cookie.Expires = DateTimeOffset.Now.AddMinutes(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return response;

        }

        [HttpPost]
        [Route("reset_pwd")]
        public Resp_Binary ResetPwd(Req_ResetPwd request) => _accountService.ResetPwd(request);

        [HttpPost]
        [Route("index")]
        public Resp_Login_Index Index(Req_Index request)
        {
            var response = new Resp_Login_Index();
            response.sysmodules = _sysRightService.GetRightModuleByUser(request.userId);
            using (var context = new SKContext())
            {
                var sysUser = context.SysUsers.SingleOrDefault(t => t.ID == request.userId);
                response.syschannels = sysUser.SysChannels.ConvertoDto<SysChannel,SysChannelDTO>().ToList();
            }
            return response;
        }
    }
}

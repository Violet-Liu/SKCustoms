using Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using Unity.Attributes;
using System.Web.Http.Controllers;
using System.Web.Security;
using System.Text;

namespace SKCustoms.WebApi
{
    public class LogExceptionAttribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (!actionExecutedContext.ActionContext.ModelState.IsValid)
            {
                var _service = new SysErrorLogService();
                var errorlog = Constructor.CreateLogError(actionExecutedContext.Exception);
                _service.Insert(errorlog);

                actionExecutedContext.Response= actionExecutedContext.Request
                    .CreateResponse(new { flag = 0, mesage = actionExecutedContext.ActionContext.ModelState.ToJson() });
            }
            else if (actionExecutedContext.IsNotNull())
            {
                var _service = new SysErrorLogService();
                var errorlog = Constructor.CreateLogError(actionExecutedContext.Exception);
                _service.Insert(errorlog);

                actionExecutedContext.Response = actionExecutedContext.Request
                    .CreateResponse(new { flag = 0, mesage = "系统错误，请联系管理员" });
            }
        }

    }

    public class JsonCallbackAttribute : ActionFilterAttribute
    {
        private bool ValidateTicket(string encryptToken)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptToken).UserData;

            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string userName = strTicket.Substring(0, index);
            string password = strTicket.Substring(index + 1);
            //取得session，不通过说明用户退出，或者session已经过期
            var token = HttpContext.Current.Session[userName];
            if (token == null)
            {
                return false;
            }
            //对比session中的令牌
            if (token.ToString() == encryptToken)
            {
                return true;
            }

            return false;

        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            var request = actionContext.Request;

            if (request.Method.Method.ToUpper().Equals("OPTIONS"))
            {
                request.CreateResponse(HttpStatusCode.OK);
                return;
            }

            var noFilerMethods = new List<string>();
            noFilerMethods.Add("login");
            noFilerMethods.Add("temporary_sync");
            noFilerMethods.Add("all_sync");
            if (noFilerMethods.Contains(actionContext.ActionDescriptor.ActionName.ToLower()))
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            var cookie = actionContext.Request.Headers.GetCookies();
            if (cookie == null || cookie.Count < 1)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            var perCookie = cookie[0].Cookies.FirstOrDefault(t => t.Name.ToLower() == "userinfo");

            if (!(perCookie.IsNull() || string.IsNullOrEmpty(perCookie.Value)))
            {
                var islogin = false;
                var userinfo = Encoding.UTF8.GetString(Convert.FromBase64String(perCookie.Value));
                if (!string.IsNullOrEmpty(userinfo))
                {
                    var userid = userinfo.Split('&')[0];
                    if (!string.IsNullOrEmpty(userid))
                    {
                        var s = userid.Split('=');
                        if (s.Length == 2)
                        {
                            if (s[1].ToInt() > 0)
                            {
                                islogin = true;
                            }
                        }
                    }
                }
                if (!islogin)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //if (actionExecutedContext.Exception.IsNull())
            //{
            //    var response = actionExecutedContext.Response.Headers;
            //    if (response != null)
            //    {
            //        response.Add("Access-Control-Allow-Origin", actionExecutedContext.Request.RequestUri.Authority);
            //        response.Add("Access-Control-Allow-Credentials", "true");
            //        response.Add("Access-Control-Allow-Methods", "*");
            //        response.Add("Access-Control-Allow-Headers", "Content-Type,Access-Token");
            //        response.Add("Access-Control-Expose-Headers", "*");
            //    }
            //}
            base.OnActionExecuted(actionExecutedContext);
        }
    }

    public class GZipCompressionAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Filter = new System.IO.Compression.GZipStream(Response.Filter,
                                              System.IO.Compression.CompressionMode.Compress);
            #region II6不支持此方法,(实际上此值默认为null 也不需要移除)
            //Response.Headers.Remove("Content-Encoding");
            #endregion
            Response.AppendHeader("Content-Encoding", "gzip");
        }
    }


    internal class Constructor
    {
        public static SysErrorLog CreateLogError(Exception e)
        {
            var log = new SysErrorLog
            {
                ErrReferrer = HttpContext.Current.Request.UserAgent,
                ErrSource = e.Source,
                ErrTime = DateTime.Now,
                ErrTimestr = DateTime.Now.ToString("yyyyMMdd"),
                ErrStack = e.StackTrace,
                ErrType = SysErrorType.system.ToString(),
                ErrUrl = HttpContext.Current.Request.Url.OriginalString,
                ErrIp = HttpContext.Current.Request.UserHostAddress,
                ErrMessage = e.Message
            };
            return log;
        }
    }
}
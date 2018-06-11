using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace SKCustoms.Controllers
{
    public class AccountController : Controller
    {
        [Dependency]
        public IAccountService _accountBLL { get; set; }

        public SysConfigService _configBLL { get; set; }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}
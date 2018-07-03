using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SKCustoms.WebApi.Controllers;

namespace SKCustoms.WebApi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // 排列
            AccountController controller = new AccountController();

            // 操作
            var result = controller.Login("admin","123");

            // 断言
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result);
        }
    }
}

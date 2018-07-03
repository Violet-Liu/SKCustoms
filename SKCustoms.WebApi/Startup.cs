using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(SKCustoms.WebApi.Startup))]

namespace SKCustoms.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);   //跨域处理，一句代码就解决了
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            //app.MapSignalR();
            //MQHubsConfig.Run();
        }
    }
}

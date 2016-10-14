using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(JoinAndDo.Startup))]

namespace JoinAndDo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}

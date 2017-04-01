using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HungryWeb.Startup))]
namespace HungryWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

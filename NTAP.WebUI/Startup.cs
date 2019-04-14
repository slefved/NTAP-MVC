using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NTAP.WebUI.Startup))]
namespace NTAP.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

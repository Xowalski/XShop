using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XShop.WebUI.Startup))]
namespace XShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

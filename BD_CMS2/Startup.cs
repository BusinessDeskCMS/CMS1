using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BD_CMS2.Startup))]
namespace BD_CMS2
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

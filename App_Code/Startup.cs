using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SDET_Project.Startup))]
namespace SDET_Project
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

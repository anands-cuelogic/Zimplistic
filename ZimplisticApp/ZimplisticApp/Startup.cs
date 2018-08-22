using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZimplisticApp.Startup))]
namespace ZimplisticApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

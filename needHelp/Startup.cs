using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(needHelp.Startup))]
namespace needHelp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

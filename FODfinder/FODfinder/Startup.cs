using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FODfinder.Startup))]
namespace FODfinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

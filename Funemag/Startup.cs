using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Funemag.Startup))]
namespace Funemag
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

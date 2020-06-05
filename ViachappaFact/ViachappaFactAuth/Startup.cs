using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViachappaFactAuth.Startup))]
namespace ViachappaFactAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

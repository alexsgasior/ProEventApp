using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProEventApp.Startup))]
namespace ProEventApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeVein_Web_MVC.Startup))]
namespace CodeVein_Web_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

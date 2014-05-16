using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSchool.Startup))]
namespace WebSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

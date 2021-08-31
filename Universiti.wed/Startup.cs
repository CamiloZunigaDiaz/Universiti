using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Universiti.wed.Startup))]
namespace Universiti.wed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

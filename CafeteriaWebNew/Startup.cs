using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CafeteriaWebNew.Startup))]
namespace CafeteriaWebNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

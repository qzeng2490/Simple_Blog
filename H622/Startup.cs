using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(H622.Startup))]
namespace H622
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

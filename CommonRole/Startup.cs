using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CommonRole.Startup))]
namespace CommonRole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}

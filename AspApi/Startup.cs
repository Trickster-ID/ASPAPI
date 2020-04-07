using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspApi.Startup))]
namespace AspApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

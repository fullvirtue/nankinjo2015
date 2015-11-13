using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NankinjoKey.Startup))]
namespace NankinjoKey
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

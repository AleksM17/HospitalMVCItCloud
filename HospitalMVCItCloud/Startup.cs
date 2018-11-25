using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalMVCItCloud.Startup))]
namespace HospitalMVCItCloud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

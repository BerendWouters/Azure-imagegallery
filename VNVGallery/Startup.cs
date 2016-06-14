using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VnVGallery.Startup))]
namespace VnVGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

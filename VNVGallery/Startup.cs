using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VNVGallery.Startup))]
namespace VNVGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

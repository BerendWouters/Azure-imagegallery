using System.Data.Entity.Migrations;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImageGallery.Startup))]

namespace ImageGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            UpdateDatabase();
            ConfigureAuth(app);
        }

        private static void UpdateDatabase()
        {
            var configuration = new ImageGallery.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}

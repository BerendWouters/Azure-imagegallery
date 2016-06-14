using System.Configuration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VnVGallery.Data
{
    public class GalleryContext : DbContext
    {
        public GalleryContext(string connectionString):
            base(connectionString)
        {
        }

        public GalleryContext()
            : this(ConfigurationManager.ConnectionStrings["GalleryConnection"].ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            
        }

        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityUser> Users { get; set; }
    }
}
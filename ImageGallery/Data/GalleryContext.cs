using System.Configuration;
using System.Data.Entity;

namespace ImageGallery.Data
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
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }
    }
}
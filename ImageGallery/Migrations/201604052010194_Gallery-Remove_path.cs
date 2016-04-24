namespace ImageGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalleryRemove_path : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Galleries", "Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Galleries", "Path", c => c.String());
        }
    }
}

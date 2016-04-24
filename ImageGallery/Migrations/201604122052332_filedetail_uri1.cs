namespace ImageGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filedetail_uri1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileDetails", "BlobUri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileDetails", "BlobUri");
        }
    }
}

namespace ImageGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rework_fileupload : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pictures", "GalleryId", "dbo.Galleries");
            DropIndex("dbo.Pictures", new[] { "GalleryId" });
            CreateTable(
                "dbo.FileDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Extension = c.String(),
                        GalleryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.GalleryId, cascadeDelete: true)
                .Index(t => t.GalleryId);
            
            DropTable("dbo.Pictures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GalleryId = c.Int(nullable: false),
                        Filename = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.FileDetails", "GalleryId", "dbo.Galleries");
            DropIndex("dbo.FileDetails", new[] { "GalleryId" });
            DropTable("dbo.FileDetails");
            CreateIndex("dbo.Pictures", "GalleryId");
            AddForeignKey("dbo.Pictures", "GalleryId", "dbo.Galleries", "Id", cascadeDelete: true);
        }
    }
}

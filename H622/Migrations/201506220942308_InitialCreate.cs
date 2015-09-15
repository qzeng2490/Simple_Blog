namespace H622.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        CreateTime = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Post_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Post_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPosts", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_ID", "dbo.Tags");
            DropIndex("dbo.TagPosts", new[] { "Post_ID" });
            DropIndex("dbo.TagPosts", new[] { "Tag_ID" });
            DropTable("dbo.TagPosts");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
        }
    }
}

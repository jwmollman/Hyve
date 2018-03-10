namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "PostType_Id", c => c.Int());
            CreateIndex("dbo.Posts", "PostType_Id");
            AddForeignKey("dbo.Posts", "PostType_Id", "dbo.PostTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostType_Id", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "PostType_Id" });
            DropColumn("dbo.Posts", "PostType_Id");
            DropTable("dbo.PostTypes");
        }
    }
}

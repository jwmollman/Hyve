namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePostTypeEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "PostType_Id", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "PostType_Id" });
            DropColumn("dbo.Posts", "PostType_Id");
            DropTable("dbo.PostTypes");
        }
        
        public override void Down()
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
    }
}

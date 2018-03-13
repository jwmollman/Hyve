namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityFieldCleanup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "LinkUrl", c => c.String());
            DropColumn("dbo.Posts", "ExternalLink");
            DropColumn("dbo.Posts", "Content");
            DropColumn("dbo.Posts", "Enabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Enabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Posts", "Content", c => c.String());
            AddColumn("dbo.Posts", "ExternalLink", c => c.String());
            DropColumn("dbo.Posts", "LinkUrl");
        }
    }
}

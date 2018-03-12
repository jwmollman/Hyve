namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExternalLinkToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ExternalLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ExternalLink");
        }
    }
}

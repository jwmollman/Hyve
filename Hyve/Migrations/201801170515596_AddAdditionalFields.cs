namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdditionalFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "DateCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "DateUpdatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "DateCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "DateUpdatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "Enabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateUpdatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Enabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Profiles", "DateCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Profiles", "DateUpdatedUtc", c => c.DateTime(nullable: false));
            DropColumn("dbo.Comments", "DateCreated");
            DropColumn("dbo.Comments", "DateUpdated");
            DropColumn("dbo.Posts", "DateCreated");
            DropColumn("dbo.Posts", "DateUpdated");
            DropColumn("dbo.AspNetUsers", "DateCreated");
            DropColumn("dbo.AspNetUsers", "DateUpdated");
            DropColumn("dbo.Profiles", "DateCreated");
            DropColumn("dbo.Profiles", "DateUpdated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Profiles", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "DateCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.Profiles", "DateUpdatedUtc");
            DropColumn("dbo.Profiles", "DateCreatedUtc");
            DropColumn("dbo.AspNetUsers", "Enabled");
            DropColumn("dbo.AspNetUsers", "DateUpdatedUtc");
            DropColumn("dbo.AspNetUsers", "DateCreatedUtc");
            DropColumn("dbo.Posts", "Enabled");
            DropColumn("dbo.Posts", "DateUpdatedUtc");
            DropColumn("dbo.Posts", "DateCreatedUtc");
            DropColumn("dbo.Comments", "DateUpdatedUtc");
            DropColumn("dbo.Comments", "DateCreatedUtc");
        }
    }
}

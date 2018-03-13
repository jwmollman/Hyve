namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Rating");
        }
    }
}

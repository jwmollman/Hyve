namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Rating");
        }
    }
}

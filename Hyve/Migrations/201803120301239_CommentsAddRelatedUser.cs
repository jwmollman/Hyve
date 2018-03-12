namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsAddRelatedUser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "CreatedBy_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_User_Id", newName: "IX_CreatedBy_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_CreatedBy_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Comments", name: "CreatedBy_Id", newName: "User_Id");
        }
    }
}

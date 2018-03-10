namespace Hyve.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserToProfileRelationship : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Profiles", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Profiles", name: "IX_User_Id", newName: "IX_UserId");
            DropPrimaryKey("dbo.Profiles");
            AddPrimaryKey("dbo.Profiles", "UserId");
            DropColumn("dbo.Profiles", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Profiles");
            AddPrimaryKey("dbo.Profiles", "Id");
            RenameIndex(table: "dbo.Profiles", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Profiles", name: "UserId", newName: "User_Id");
        }
    }
}

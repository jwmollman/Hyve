namespace HackerNewsClone.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class RenameModelIdColumns : DbMigration {
        public override void Up() {
            RenameColumn("Comments", "ID", "Id");
            RenameColumn("Comments", "Comment_ID", "Comment_Id");
            RenameColumn("Comments", "Post_ID", "Post_Id");
            RenameColumn("Posts", "ID", "Id");
            RenameColumn("Profiles", "ID", "Id");
            DropIndex("dbo.Comments", new[] { "Comment_ID" });
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            CreateIndex("dbo.Comments", "Comment_Id");
            CreateIndex("dbo.Comments", "Post_Id");
            DropForeignKey("Comments", "FK_dbo.Comments_dbo.Comments_Comment_ID");
            DropForeignKey("Comments", "FK_dbo.Comments_dbo.Posts_Post_ID");
            AddForeignKey("Comments", "Comment_Id", "Comments", "Id", false, "FK_dbo.Comments_dbo.Comments_Comment_Id");
            AddForeignKey("Comments", "Post_Id", "Posts", "Id", false, "FK_dbo.Comments_dbo.Posts_Post_Id");
        }

        public override void Down() {
            RenameColumn("Comments", "Id", "ID");
            RenameColumn("Comments", "Comment_Id", "Comment_ID");
            RenameColumn("Comments", "Post_Id", "Post_ID");
            RenameColumn("Posts", "Id", "ID");
            RenameColumn("Profiles", "Id", "ID");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            CreateIndex("dbo.Comments", "Post_ID");
            CreateIndex("dbo.Comments", "Comment_ID");
            //DropForeignKey("Comments", "FK_dbo.Comments_dbo.Comments_Comment_ID");
            //DropForeignKey("Comments", "FK_dbo.Comments_dbo.Posts_Post_ID");
            //AddForeignKey("Comments", "Comment_Id", "Comments", "Id");
            //AddForeignKey("Comments", "Post_Id", "Posts", "Id");
        }
    }
}

namespace Hyve.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration {
        public override void Up() {
            CreateTable(
                "dbo.Comments",
                c => new {
                    ID = c.Int(nullable: false, identity: true),
                    DateCreated = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    Content = c.String(),
                    Comment_ID = c.Int(),
                    Post_ID = c.Int(),
                    Member_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Comments", t => t.Comment_ID)
                .ForeignKey("dbo.Posts", t => t.Post_ID)
                .ForeignKey("dbo.Members", t => t.Member_ID)
                .Index(t => t.Comment_ID)
                .Index(t => t.Post_ID)
                .Index(t => t.Member_ID);

            CreateTable(
                "dbo.Posts",
                c => new {
                    ID = c.Int(nullable: false, identity: true),
                    DateCreated = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    Content = c.String(),
                    CreatedByMember_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.CreatedByMember_ID)
                .Index(t => t.CreatedByMember_ID);

            CreateTable(
                "dbo.Members",
                c => new {
                    ID = c.Int(nullable: false, identity: true),
                    DateCreated = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    Username = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Profiles",
                c => new {
                    ID = c.Int(nullable: false),
                    DateCreated = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    Bio = c.String(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.ID)
                .Index(t => t.ID);

        }

        public override void Down() {
            DropForeignKey("dbo.Profiles", "ID", "dbo.Members");
            DropForeignKey("dbo.Posts", "CreatedByMember_ID", "dbo.Members");
            DropForeignKey("dbo.Comments", "Member_ID", "dbo.Members");
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Comment_ID", "dbo.Comments");
            DropIndex("dbo.Profiles", new[] { "ID" });
            DropIndex("dbo.Posts", new[] { "CreatedByMember_ID" });
            DropIndex("dbo.Comments", new[] { "Member_ID" });
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            DropIndex("dbo.Comments", new[] { "Comment_ID" });
            DropTable("dbo.Profiles");
            DropTable("dbo.Members");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}

namespace HackerNewsClone.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddAspNetIdentity : DbMigration {
        public override void Up() {
            DropForeignKey("dbo.Comments", "Member_ID", "dbo.Members");
            DropForeignKey("dbo.Posts", "CreatedByMember_ID", "dbo.Members");
            DropForeignKey("dbo.Profiles", "ID", "dbo.Members");
            DropIndex("dbo.Comments", new[] { "Member_ID" });
            DropIndex("dbo.Posts", new[] { "CreatedByMember_ID" });
            DropIndex("dbo.Profiles", new[] { "ID" });
            DropPrimaryKey("dbo.Profiles");
            CreateTable(
                "dbo.AspNetUsers",
                c => new {
                    Id = c.String(nullable: false, maxLength: 128),
                    DateCreated = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    Description = c.String(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            AddColumn("dbo.Comments", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Posts", "CreatedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Profiles", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Profiles", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Profiles", "ID");
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.Posts", "CreatedBy_Id");
            CreateIndex("dbo.Profiles", "User_Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Profiles", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comments", "Member_ID");
            DropColumn("dbo.Posts", "CreatedByMember_ID");
            DropTable("dbo.Members");
        }

        public override void Down() {
            CreateTable(
                "dbo.Members",
                c => new {
                    ID = c.Int(nullable: false, identity: true),
                    DateCreated = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    Username = c.String(),
                })
                .PrimaryKey(t => t.ID);

            AddColumn("dbo.Posts", "CreatedByMember_ID", c => c.Int());
            AddColumn("dbo.Comments", "Member_ID", c => c.Int());
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profiles", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Profiles", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Posts", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropPrimaryKey("dbo.Profiles");
            AlterColumn("dbo.Profiles", "ID", c => c.Int(nullable: false));
            DropColumn("dbo.Profiles", "User_Id");
            DropColumn("dbo.Posts", "CreatedBy_Id");
            DropColumn("dbo.Comments", "User_Id");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            AddPrimaryKey("dbo.Profiles", "ID");
            CreateIndex("dbo.Profiles", "ID");
            CreateIndex("dbo.Posts", "CreatedByMember_ID");
            CreateIndex("dbo.Comments", "Member_ID");
            AddForeignKey("dbo.Profiles", "ID", "dbo.Members", "ID");
            AddForeignKey("dbo.Posts", "CreatedByMember_ID", "dbo.Members", "ID");
            AddForeignKey("dbo.Comments", "Member_ID", "dbo.Members", "ID");
        }
    }
}

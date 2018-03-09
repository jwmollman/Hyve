namespace Hyve.Migrations {
    using Hyve.Enums;
    using Hyve.Models;
    using Hyve.Models.Contexts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Hyve.Models.Contexts.HyveDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Hyve.Models.Contexts.HyveDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!System.Diagnostics.Debugger.IsAttached) {
                System.Diagnostics.Debugger.Launch();
            }

            AddRoles();
            AddUsers();
        }

        private void AddRoles() {
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(new HyveDbContext());
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists(Roles.Administrator)) {
                roleManager.Create(new IdentityRole() {
                    Name = Roles.Administrator,
                });
            }
            if (!roleManager.RoleExists(Roles.Moderator)) {
                roleManager.Create(new IdentityRole() {
                    Name = Roles.Moderator,
                });
            }
            if (!roleManager.RoleExists(Roles.Normal)) {
                roleManager.Create(new IdentityRole() {
                    Name = Roles.Normal,
                });
            }
        }

        private void AddUsers() {
            UserStore<User> userStore = new UserStore<User>(new HyveDbContext());
            UserManager<User> userManager = new UserManager<User>(userStore);

            string user1Username = "user1";
            User user1 = userManager.FindByName(user1Username);
            if (user1 == null) {
                IdentityResult result = userManager.Create(new User() {
                    UserName = user1Username,
                    Email = "me@johnmollman.com",
                    PasswordHash = new PasswordHasher().HashPassword("password"),
                    DateCreatedUtc = DateTime.Now,
                    DateUpdatedUtc = DateTime.Now,
                });
                if (result.Succeeded) {
                    User user = userManager.FindByName(user1Username);
                    userManager.AddToRole(user.Id, Roles.Administrator);
                }
            }

            string user2Username = "user2";
            User user2 = userManager.FindByName(user2Username);
            if (user2 == null) {
                IdentityResult result = userManager.Create(new User() {
                    UserName = user2Username,
                    Email = "test@email.com",
                    PasswordHash = new PasswordHasher().HashPassword("password"),
                    DateCreatedUtc = DateTime.Now,
                    DateUpdatedUtc = DateTime.Now,
                });
                if (result.Succeeded) {
                    User user = userManager.FindByName(user2Username);
                    userManager.AddToRole(user.Id, Roles.Normal);
                }
            }
        }
    }
}

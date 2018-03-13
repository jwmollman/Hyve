namespace Hyve.Migrations {
    using Hyve.Enums;
    using Hyve.Models;
    using Hyve.Models.Contexts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hyve.Models.Contexts.HyveDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Hyve.Models.Contexts.HyveDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached) {
                System.Diagnostics.Debugger.Launch();
            }
#endif

            AddRoles();
            AddUsers();
            AddPosts();
            AddComments();
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
                    Email = "user1@email.com",
                    PasswordHash = new PasswordHasher().HashPassword("password"),
                    DateCreatedUtc = DateTime.Now,
                    DateUpdatedUtc = DateTime.Now,
                    Profile = null,
                    Posts = new List<Post>(),
                    Comments = new List<Comment>(),
                    Enabled = true,
                });
                if (result.Succeeded) {
                    User user = userManager.FindByName(user1Username);
                    userManager.AddToRole(user.Id, Roles.Administrator);
                    using (HyveDbContext db = new HyveDbContext()) {
                        db.Profiles.Add(new Profile() {
                            UserId = user.Id,
                            DateCreatedUtc = DateTime.Now,
                            DateUpdatedUtc = DateTime.Now,
                            Bio = $"This is the bio for {user1Username}.",
                        });
                        db.SaveChanges();
                    }
                }
            }

            string user2Username = "user2";
            User user2 = userManager.FindByName(user2Username);
            if (user2 == null) {
                IdentityResult result = userManager.Create(new User() {
                    UserName = user2Username,
                    Email = "user2@email.com",
                    PasswordHash = new PasswordHasher().HashPassword("password"),
                    DateCreatedUtc = DateTime.Now,
                    DateUpdatedUtc = DateTime.Now,
                    Enabled = true,
                });
                if (result.Succeeded) {
                    User user = userManager.FindByName(user2Username);
                    userManager.AddToRole(user.Id, Roles.Normal);
                    using (HyveDbContext db = new HyveDbContext()) {
                        db.Profiles.Add(new Profile() {
                            UserId = user.Id,
                            DateCreatedUtc = DateTime.Now,
                            DateUpdatedUtc = DateTime.Now,
                            Bio = $"This is the bio for {user2Username}.",
                        });
                        db.SaveChanges();
                    }
                }
            }
        }

        private void AddPosts() {
            using (HyveDbContext db = new HyveDbContext()) {
                User user1 = db.Users.Where(x => x.UserName == "user1").First();

                for (int i = 0; i < 30; i++) {
                    db.Posts.Add(new Post() {
                        DateCreatedUtc = DateTime.Now,
                        DateUpdatedUtc = DateTime.Now,
                        Title = $"This is post #{i}",
                        LinkUrl = "https://www.google.com",
                        Rating = 1,
                        CreatedBy = user1,
                        Comments = new List<Comment>(),
                    });
                }

                db.SaveChanges();
            }
        }

        private void AddComments() {
            using (HyveDbContext db = new HyveDbContext()) {
                User user1 = db.Users.Where(x => x.UserName == "user1").First();
                User user2 = db.Users.Where(x => x.UserName == "user2").First();
                Post post = db.Posts.Where(x => x.Id == 1).First();
                
                Comment commentChild = new Comment() {
                    DateCreatedUtc = DateTime.Now,
                    DateUpdatedUtc = DateTime.Now,
                    Content = "This is your reply...",
                    Rating = 1,
                    Post = post,
                    CreatedBy = user2,
                    Comments = null,
                };

                Comment parentComment1 = new Comment() {
                    DateCreatedUtc = DateTime.Now,
                    DateUpdatedUtc = DateTime.Now,
                    Content = "Someone reply to me...",
                    Rating = 1,
                    Post = post,
                    CreatedBy = user1,
                    Comments = new List<Comment>() {
                        commentChild
                    },
                };

                Comment parentComment2 = new Comment() {
                    DateCreatedUtc = DateTime.Now,
                    DateUpdatedUtc = DateTime.Now,
                    Content = "A comment without a reply...",
                    Rating = 1,
                    Post = post,
                    CreatedBy = user2,
                    Comments = null,
                };

                List<Comment> comments = new List<Comment>() {
                    commentChild,
                    parentComment1,
                    parentComment2
                };

                db.Comments.AddRange(comments);
                post.Comments.Concat(comments);
                db.SaveChanges();
            }
        }
    }
}

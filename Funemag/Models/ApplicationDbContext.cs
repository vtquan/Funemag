using Funemag.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funemag.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }

    public class ApplicationDbContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            context.Platforms.Add(new Platform() { PlatformId = 1, Maker = "First Maker", Name = "First Platform" });
            context.Platforms.Add(new Platform() { PlatformId = 2, Maker = "First Maker", Name = "Second Platform" });
            context.Platforms.Add(new Platform() { PlatformId = 3, Maker = "Second Maker", Name = "Third Platform" });
            context.Platforms.Add(new Platform() { PlatformId = 4, Maker = "Third Maker", Name = "Fourth Platform" });

            context.News.Add(new News() { PostId = 1, Title = "First News", Content = "First Content", Date = DateTime.Parse("11/01/90"), SourceLink = "http://www.funemag.us", Platforms = new Collection<Platform>(), ViewCount = 0 });
            context.News.Add(new News() { PostId = 2, Title = "Second News", Content = "Second Content", Date = DateTime.Parse("11/02/90"), Platforms = new Collection<Platform>(), ViewCount = 0 });
            context.News.Add(new News() { PostId = 3, Title = "Third News", Content = "Third Content", Date = DateTime.Parse("11/03/90"), Platforms = new Collection<Platform>(), ViewCount = 0 });
            context.News.Add(new News() { PostId = 3, Title = "Fourth News", Content = "Fourth Content", Date = DateTime.Parse("11/04/99"), Platforms = new Collection<Platform>(), ViewCount = 0 });

            context.Reviews.Add(new Review() { PostId = 4, Title = "First Review", Content = "First Content", Date = DateTime.Parse("12/01/90"), InfoLink = "http://www.funemag.us", Platforms = new Collection<Platform>(), ViewCount = 0 });
            context.Reviews.Add(new Review() { PostId = 5, Title = "Second Review", Content = "Second Content", Date = DateTime.Parse("12/02/90"), Platforms = new Collection<Platform>(), ViewCount = 0 });
            context.Reviews.Add(new Review() { PostId = 6, Title = "Third Review", Content = "Third Content", Date = DateTime.Parse("12/03/90"), InfoLink = "http://www.funemag.us", AffiliateLink = "http://www.funemag.us", Platforms = new Collection<Platform>(), ViewCount = 0 });

            context.Reviews.Find(4).Platforms.Add(context.Platforms.Find(1));
            context.Reviews.Find(4).Platforms.Add(context.Platforms.Find(2));
            context.Reviews.Find(5).Platforms.Add(context.Platforms.Find(2));
            context.Reviews.Find(6).Platforms.Add(context.Platforms.Find(2));
            context.Reviews.Find(6).Platforms.Add(context.Platforms.Find(1));
            context.Reviews.Find(6).Platforms.Add(context.Platforms.Find(3));

            context.Roles.Add(new IdentityRole() { Name = "Admin" });
            
            var user = new ApplicationUser() { UserName = "test@email.com", Email = "test@email.com" };
            manager.Create(user, "Admin@1234");
            manager.AddToRole(user.Id, "Admin");
        }
    }
}
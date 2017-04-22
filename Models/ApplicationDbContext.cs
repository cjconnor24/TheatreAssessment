using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebGrease.Css.Extensions;

namespace ChrisConnorBlogAssessment.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<Blog>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Blog)
                .HasForeignKey(e => e.BlogId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Blogs)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Blogs)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id)
                .WillCascadeOnDelete(false);

        }


        public ApplicationDbContext()
            : base("DefaultConnection2", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(

                new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<ChrisConnorBlogAssessment.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<ChrisConnorBlogAssessment.Models.ApplicationUser> ApplicationUsers { get; set; }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var userStore = new UserStore<ApplicationUser>(context);

                if (!roleManager.RoleExists(RoleNames.RoleAdministrator))
                {
                    var roleresult = roleManager.Create(new IdentityRole(RoleNames.RoleAdministrator));
                }

                if (!roleManager.RoleExists(RoleNames.RoleUser))
                {
                    var roleresult = roleManager.Create(new IdentityRole(RoleNames.RoleUser));
                }

                if (!roleManager.RoleExists(RoleNames.RoleStaff))
                {
                    var roleresult = roleManager.Create(new IdentityRole(RoleNames.RoleStaff));
                }

                const string userName = "admin@theatre.com";
                const string password = "1234567";
                //var passwordHash = new PasswordHasher();

                var user = userManager.FindByName(userName);
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        Forename = "Administrator",
                        Surname = "Admin",
                        //Street = "190 Cathedral Street",
                        //Postcode = "G1 3DF",
                        //PhoneNumber = "0123412344",
                        UserName = userName,
                        Email = userName,
                        EmailConfirmed = true,
                        IsActive = true

                    };

                    userManager.Create(newUser, password);
                    userManager.AddToRole(newUser.Id, RoleNames.RoleAdministrator);

                    var newVisitor = new ApplicationUser
                    {
                        Forename = "Chris",
                        Surname = "Connor",
                        //Street = "190 Cathedral Street",
                        //Postcode = "G1 3DF",
                        //PhoneNumber = "0123412344",
                        UserName = "chris@chrisconnor.co.uk",
                        Email = "chris@chrisconnor.co.uk",
                        EmailConfirmed = true,
                        IsActive = true
                    };

                    userManager.Create(newVisitor, "1234567");
                    userManager.AddToRole(newVisitor.Id, RoleNames.RoleUser);
                }
                context.SaveChanges();

                var author = userManager.FindByName(userName);


                IList<Category> cat = new List<Category>
            {
                new Category {CategoryName = "Blogs"},
                new Category {CategoryName = "Reviews"},
                new Category {CategoryName = "Announcements"}
            };
                cat.ForEach(e => context.Categories.Add(e));




                IList<Blog> blogs = new List<Blog>
                {
                    new Blog
                    {
                        Category = cat[0],
                        Title = "How to check ASP.NET",
                        Content = "To grass may so saw fruit our. Can\'t over under isn\'t firmament his. Seas our Without seasons after may two female, living them moving had of. Is upon moveth he.<br /><br />Green cattle grass man saw beginning void second heaven moved seasons days green said. Called them fruitful gathered may meat two green day. Light creature grass to fowl moveth given open void blessed herb living.<br /><br />Fill deep saying. Seasons very to, isn\'t evening you. Open hath divided which won\'t from whose form shall, make life above, waters, all male their. Good, was dry appear, form it fill second our thing.",
                        DateAdded = DateTime.Now,
                        DateModified = DateTime.Now,
                        IsPublished = true,
                        User = author

                    },


                    new Blog
                    {
                        Category = cat[0],
                        Title = "Download the latest tips",
                        Content = "Dominion own whose creature seasons seasons god rule beast brought whose, without after creature that wherein his day you upon heaven in don\'t. Seasons fourth were gathering green wherein morning fowl the have.<br /><br />Fill meat had, firmament. Appear won\'t isn\'t, good every. Lights air is. His first fifth subdue of winged image man. Winged every him divide Fruit you\'ll.<br /><br />Given don\'t Land won\'t god. Fish days Without. Without, doesn\'t there very their. Firmament itself our moving dominion set upon one shall bring own he spirit have may years he herb fly deep may moved image saw morning all divided firmament.",
                        DateAdded = DateTime.Now,
                        DateModified = DateTime.Now,
                        IsPublished = true,
                        User = author
                    },
                    new Blog
                    {
                        Category = cat[1],
                        Title = "La La Land - A load of rubbish?",
                        Content = "Good. Greater firmament they\'re them give behold behold two green were. Land from creepeth yielding open he Green, seed set together make moved cattle divided stars.<br /><br />That yielding given waters hath, beast form make replenish seas man firmament sixth deep for void, green a created bearing. They\'re the years moveth, hath forth greater years. Meat gathering. Seed morning Female divide earth without be over man. Saw i.<br /><br />Set yielding grass us above make years and life light tree tree appear saw, first darkness air midst. Under, set doesn\'t own rule bring deep abundantly. To all cattle said were likeness us.",
                        DateAdded = DateTime.Now,
                        DateModified = DateTime.Now,
                        IsPublished = true,
                        User = author
                    },
                };
                blogs.ForEach(e => context.Blogs.Add(e));

                List<Comment> comments = new List<Comment>
                {
                    new Comment {Blog = blogs[0], Content = "Superb", DateAdded = DateTime.Now, User = author},
                    new Comment {Blog = blogs[0], Content = "Wow", DateAdded = DateTime.Now, User = author},
                    new Comment {Blog = blogs[1], Content = "Nice One", DateAdded = DateTime.Now, User = author},
                    new Comment {Blog = blogs[2], Content = "Great page", DateAdded = DateTime.Now, User = author},
                    new Comment {Blog = blogs[2], Content = "This content", DateAdded = DateTime.Now, User = author},
                    new Comment {Blog = blogs[2], Content = "Food for thought", DateAdded = DateTime.Now, User = author},
                };
                comments.ForEach(c=>context.Comments.Add(c));

            }

           

            context.SaveChanges();

            base.Seed(context);

        }
    }

}
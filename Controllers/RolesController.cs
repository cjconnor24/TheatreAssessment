using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ChrisConnorBlogAssessment.Models;
using ChrisConnorBlogAssessment.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChrisConnorBlogAssessment.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Roles
        public ActionResult Index()
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userStore = new UserStore<ApplicationUser>(db);

            return View();
        }

        public void LockUser(string email)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userStore = new UserStore<ApplicationUser>(db);

            var user = userManager.FindByEmail(email);
            if (user != null)
            {
                user.LockoutEnabled = true;
            }

            RedirectToAction("Details");
        }

        public void EnableUser(string email)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userStore = new UserStore<ApplicationUser>(db);

            var user = userManager.FindByEmail(email);
            if (user != null)
            {
                user.LockoutEnabled = false;
            }

            RedirectToAction("Details");
        }

        public ActionResult Details()
        {
            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //var UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(db));
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userStore = new UserStore<ApplicationUser>(db);

            var users = new List<ApplicationUser>();
            var staff = new List<ApplicationUser>();
            var admins = new List<ApplicationUser>();

            foreach (var u in userManager.Users.ToList())
            {
                
                if (userManager.IsInRole(u.Id, RoleNames.RoleAdministrator))
                {
                    admins.Add(u);
                } else if (userManager.IsInRole(u.Id, RoleNames.RoleUser))
                {
                    users.Add(u);
                } else if (userManager.IsInRole(u.Id, RoleNames.RoleStaff))
                {
                    staff.Add(u);
                }
            }
            

            RoleUserViewModel vm = new RoleUserViewModel();
            vm.Users = users;
            vm.Staff = staff;
            vm.Admin = admins;

            return View(vm);

        }
    }
}
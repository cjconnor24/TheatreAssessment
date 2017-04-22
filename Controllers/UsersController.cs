using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChrisConnorBlogAssessment.Models;
using ChrisConnorBlogAssessment.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChrisConnorBlogAssessment.Controllers
{
    [Authorize(Roles = "Theatre_Administrator")]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Users
        public ActionResult Index(string category)
        {

            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userStore = new UserStore<ApplicationUser>(db);

            RolesAndUsersViewModel model = new RolesAndUsersViewModel();
            
            model.Roles = roleManager.Roles.ToList();
            model.Users = new List<UserAndRolesViewModel>();

            foreach (var u in userManager.Users.ToList())
            {
                
                var temp = new UserAndRolesViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    FullName = u.Forename + " " + u.Surname,
                    Email = u.Email,
                    IsActive = !u.LockoutEnabled,
                    Roles = new List<RoleViewModel>()
                };

                foreach (var r in roleManager.Roles.ToList())
                {
                    if (userManager.IsInRole(u.Id, r.Name))
                    {
                        temp.Roles.Add(new RoleViewModel
                        {
                            RoleId = r.Id,
                            RoleName = r.Name
                        });
                    }
                }

                model.Users.Add(temp);
            }
           
            return View(model);
        }
        public ActionResult PromoteUser(string email)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userStore = new UserStore<ApplicationUser>(db);

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            email.Replace("-", "@");

            var user = userManager.FindByEmail(email);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var promoteResult = userManager.AddToRole(user.Id, RoleNames.RoleStaff);
            var demoteResult = userManager.RemoveFromRole(user.Id, RoleNames.RoleUser);

            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult DemoteUser(string email)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userStore = new UserStore<ApplicationUser>(db);

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = userManager.FindByEmail(email);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (userManager.IsInRole(user.Id, RoleNames.RoleStaff))
            {
                var demoteResult = userManager.RemoveFromRole(user.Id, RoleNames.RoleStaff);
                var userresult = userManager.AddToRole(user.Id, RoleNames.RoleUser);
            }

            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult ToggleState(string email)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userStore = new UserStore<ApplicationUser>(db);

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            email.Replace("-", "@");

            var user = userManager.FindByEmail(email);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!userManager.IsLockedOut(user.Id))
            {
                
                userManager.SetLockoutEnabled(user.Id, true);
                userManager.SetLockoutEndDate(user.Id, DateTime.Now.AddYears(5));
                user.IsActive = false;
            }
            else
            {
                userManager.SetLockoutEnabled(user.Id, false);
                userManager.SetLockoutEndDate(user.Id, DateTime.Now.AddDays(-1));
                user.IsActive = true;
            }
            //user.LockoutEnabled = !user.LockoutEnabled;

            return RedirectToAction("Index");
            //return View();
        }
    }

}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChrisConnorBlogAssessment.Models;
using ChrisConnorBlogAssessment.ViewModels;
using Microsoft.AspNet.Identity;

namespace ChrisConnorBlogAssessment.Controllers
{
    public class BlogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Display list of blogs
        /// </summary>
        /// <returns>Blog index view</returns>
        [Authorize(Roles = "Theatre_Administrator,Theatre_Staff")]
        [Route("Blogs/Manage")]
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.Category).Include(b => b.User);
            return View(blogs.ToList());
        }

        /// <summary>
        /// Display blog based on URL ID parameter
        /// </summary>
        /// <param name="id">ID of Blog to Display</param>
        /// <returns>ViewBlog views</returns>
        public ActionResult ViewBlog(int id)
        {
            BlogDisplayViewModel model = new BlogDisplayViewModel();
            model.Blog = db.Blogs.Find(id);
            model.Blog.Comments.OrderByDescending(comment=>comment.DateAdded);
            model.BlogId = id;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddComment(CommentViewModel model)
        {

            if (ModelState.IsValid)
            {
                Comment comment = new Comment
                {
                    Content = model.Content,
                    BlogId = model.BlogId,
                    DateAdded = DateTime.Now,
                    Id = User.Identity.GetUserId()
                };
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("ViewBlog", new {id = comment.BlogId});
            }

            return RedirectToAction("ViewBlog", new { id = model.BlogId });

        }

        [Route("Blogs/")]
        [Route("Blogs/Category/{category?}")]
        public ActionResult BlogsAndCategories(string category)
        {
            BlogsCategoriesViewModel model = new BlogsCategoriesViewModel();
            //model.Blogs = db.Blogs.ToList();
            if (category == null)
            {
                model.Blogs =
                    db.Blogs.Include(b => b.Category).Include(b => b.User).Where(b => b.IsPublished == true).ToList();
            }
            else
            {
                model.Blogs =
                    db.Blogs.Include(b => b.Category).Include(b => b.User).Where(b => b.IsPublished == true).Where(b => b.Category.CategoryName==category).ToList();
            }
            model.Categories = db.Categories.ToList();
            ViewBag.UserId = (string)User.Identity.GetUserId();

            return View(model);
        }

        // GET: Blogs/Details/5
        [Authorize(Roles = "Theatre_Administrator,Theatre_Staff")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        [Authorize(Roles = "Theatre_Administrator,Theatre_Staff")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            //ViewBag.Id = new SelectList(db.Users, "Id", "Forename");
            return View("CreateBlog");
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Theatre_Administrator,Theatre_Staff")]
        public ActionResult Create(CreateBlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    IsPublished = model.IsPublished,
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now,
                    Id = User.Identity.GetUserId(),
                    CategoryId = model.CategoryId
                };
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", model.CategoryId);
            //ViewBag.Id = new SelectList(db.Users, "Id", "Forename", blog.Id);
            return View(model);
        }

        // GET: Blogs/Edit/5
        [Authorize(Roles = "Theatre_Administrator,Theatre_Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            CreateBlogViewModel model = new CreateBlogViewModel
            {
                CategoryId = blog.CategoryId,
                Content = blog.Content,
                IsPublished = blog.IsPublished,
                Title = blog.Title,
                BlogId = blog.BlogId
            };
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", model.CategoryId);
            //ViewBag.Id = new SelectList(db.Users, "Id", "Forename", blog.Id);
            return View(model);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Theatre_Administrator,Theatre_Staff")]
        public ActionResult Edit(CreateBlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blog = db.Blogs.Find(model.BlogId);
                
                blog.Title = model.Title;
                blog.Content = model.Content;
                blog.DateModified = DateTime.Now;
                blog.IsPublished = model.IsPublished;
                blog.CategoryId = model.CategoryId;
                
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", model.CategoryId);
            //ViewBag.Id = new SelectList(db.Users, "Id", "Forename", blog.Id);
            return View(model);
        }

        // GET: Blogs/Delete/5
        [Authorize(Roles = "Theatre_Administrator,Theatre_Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Theatre_Administrator,Theatre_Staff")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

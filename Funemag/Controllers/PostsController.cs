using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Funemag.Models;
using Funemag.ViewModels;
using Funemag.Models.Repository;

namespace Funemag.Controllers
{
    [Authorize(Roles = "Admin, Reviewer")]
    public class PostsController : Controller
    {
        // TODO: Update all reference to ApplicationDbContext to repository instead
        private ApplicationDbContext db = new ApplicationDbContext();
        PostRepository repository = new PostRepository();

        // GET: Posts
        public ActionResult Index(int? page, string searchString = "")
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var model = repository.GetBySearch(searchString);

            ViewData["NumPosts"] = model.Count();             

            return View(model.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = repository.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            var viewModel = new PostViewModel<Post>()
            {
                AssignedPlatforms = new List<Platform>().PopulatePlatformData(db.Platforms.ToList())
            };

            viewModel.Post.Date = DateTime.Now;

            return View(viewModel);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel<Post> viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Post.Platforms.AddOrUpdatePlatforms(viewModel.AssignedPlatforms, db.Platforms.ToList());
                db.Posts.Add(viewModel.Post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = repository.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "postId,gameTitle,content,date,affiliateLink,viewCount")] Post post)
        {
            if (ModelState.IsValid)
            {
                repository.Entry(post).State = EntityState.Modified;
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = repository.Get(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteById(id);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

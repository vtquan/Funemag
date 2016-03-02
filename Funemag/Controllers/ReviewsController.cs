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

namespace Funemag.Controllers
{
    [Authorize(Roles = "Admin, Reviewer")]
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        public ActionResult Index(int? page, string searchString)
        {
            return RedirectToAction("Index", "Posts", new { page = page, searchString = searchString });
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            var viewModel = new PostViewModel<Review>()
            {
                Post = new Review(), AssignedPlatforms = new List<Platform>().PopulatePlatformData(db.Platforms.ToList())
            };

            viewModel.Post.Date = DateTime.Now;

            return View(viewModel);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel<Review> viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Post.Platforms.AddOrUpdatePlatforms(viewModel.AssignedPlatforms, db.Platforms.ToList());
                db.Reviews.Add(viewModel.Post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Review review = db.Reviews.Include("Platforms").FirstOrDefault(r => r.PostId == id);
            if (review == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PostViewModel<Review>()
            {
                Post = review, AssignedPlatforms = review.Platforms.PopulatePlatformData(db.Platforms.ToList())
            };

            return View(viewModel);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel<Review> viewModel)
        {
            if (ModelState.IsValid)
            {
                var originalReview = db.Reviews.Find(viewModel.Post.PostId);

                originalReview.Platforms.AddOrUpdatePlatforms(viewModel.AssignedPlatforms, db.Platforms.ToList());

                db.Entry(originalReview).CurrentValues.SetValues(viewModel.Post);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Posts.Remove(review);
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

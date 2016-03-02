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
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index(int? page, string searchString)
        {
            return RedirectToAction("Index", "Posts", new { page = page, searchString = searchString });
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            var newsViewModel = new NewsViewModel()
            {
                AssignedPlatforms = new List<Platform>().PopulatePlatformData(db.Platforms.ToList())
            };
            newsViewModel.News.Date = DateTime.Now;

            return View(newsViewModel);
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsViewModel newsViewModel)
        {
            if (ModelState.IsValid)
            {
                newsViewModel.News.Platforms.AddOrUpdatePlatforms(newsViewModel.AssignedPlatforms, db.Platforms.ToList());
                db.News.Add(newsViewModel.News);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newsViewModel);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var allDbPlatforms = db.Platforms.ToList();

            News news = db.News.Include("Platforms").FirstOrDefault(n => n.PostId == id);
            if (news == null)
            {
                return HttpNotFound();
            }

            var newsViewModel = new NewsViewModel()
            {
                News = news, AssignedPlatforms = news.Platforms.PopulatePlatformData(db.Platforms.ToList())
            };

            return View(newsViewModel);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsViewModel newsViewModel)
        {
            if (ModelState.IsValid)
            {
                var originalNews = db.News.Find(newsViewModel.News.PostId);

                originalNews.Platforms.AddOrUpdatePlatforms(newsViewModel.AssignedPlatforms, db.Platforms.ToList());

                db.Entry(originalNews).CurrentValues.SetValues(newsViewModel.News);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(newsViewModel);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.Posts.Remove(news);
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

using Funemag.Models;
using Funemag.Models.Repository;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Funemag.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        PostRepository postRepository = new PostRepository();

        public ActionResult Index(int? page = 1, string searchString = "", string platform = "")
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (!String.IsNullOrEmpty(searchString))
            {
                var model = postRepository.GetBySearch(searchString)
                    .Where(post => post.IsHidden.Equals(false)).ToList()
                    .OrderByDescending(post => post.Date).ToPagedList(pageNumber, pageSize);                
                return View(model);
            }
            else if (!String.IsNullOrEmpty(platform))
            {
                var model = postRepository.GetByPlatform(platform)
                    .OrderByDescending(post => post.Date)
                    .Where(post => post.IsHidden.Equals(false))                    
                    .ToPagedList(pageNumber, pageSize);
                return View(model);
            }
            else
            {
                var model = postRepository.GetAll()
                    .OrderByDescending(post => post.Date)
                    .Where(post => post.IsHidden.Equals(false)).ToPagedList(pageNumber, pageSize);
                return View(model);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Post model = postRepository.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            if(!User.IsInRole("Admin") & !User.IsInRole("Reviewer"))
            {
                model.ViewCount++;
                postRepository.SaveChanges();
            }

            return View(model);
        }

        // GET: Feedbacks/Create
        public ActionResult Contact()
        {
            Feedback model = new Feedback();
            model.Date = DateTime.Now;

            return View(model);
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "FeedbackId,Content,Date")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feedback);
        }
    }
}
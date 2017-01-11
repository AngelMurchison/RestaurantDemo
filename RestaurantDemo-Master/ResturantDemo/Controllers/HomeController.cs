using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResturantDemo.Models;

namespace ResturantDemo.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var menu = HttpContext.Cache["menu"] as IEnumerable<Category>;
            if (menu == null)
            {
                menu = db.Categories.Include("Menu").OrderBy(o => o.Name).ToList();
                HttpContext.Cache.Add(
                    "menu",
                    menu,
                    null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 5, 0),
                    System.Web.Caching.CacheItemPriority.Default,
                    null
                    );
            }

            return View(menu);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult Create(Category category)
        {
            HttpContext.Cache.Remove("menu");
            db.Categories.Add(category);
            db.SaveChanges();
            var menu = db.Categories.Include("Menu").OrderBy(o => o.Name).ToList();
            HttpContext.Cache.Add(
                "menu",
                menu,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                new TimeSpan(0, 5, 0),
                System.Web.Caching.CacheItemPriority.Default,
                null
                );
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var category = db.Categories.FirstOrDefault(f => f.Id == id);
            return PartialView("_EditPartial", category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)

        {
            HttpContext.Cache.Remove("menu");
            var toedit = db.Categories.FirstOrDefault(f => f.Id == category.Id);
            toedit.Name = category.Name;
            db.SaveChanges();
            var menu = db.Categories.Include("Menu").OrderBy(o => o.Name).ToList();
            HttpContext.Cache.Add(
                "menu",
                menu,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                new TimeSpan(0, 5, 0),
                System.Web.Caching.CacheItemPriority.Default,
                null
                );
            return RedirectToAction("Index");
        }

    

    public ActionResult Delete(int id)
        {
            var category = db.Categories.FirstOrDefault(f => f.Id == id);
            return PartialView("Delete", category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            // var category = db.Categories.FirstOrDefault(f => f.Id == id);
            HttpContext.Cache.Remove("menu");
            var todelete = db.Categories.FirstOrDefault(f => f.Id == category.Id);
            db.Categories.Remove(todelete);
            db.SaveChanges();
            var menu = db.Categories.Include("Menu").OrderBy(o => o.Name).ToList();
            HttpContext.Cache.Add(
                "menu",
                menu,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                new TimeSpan(0, 5, 0),
                System.Web.Caching.CacheItemPriority.Default,
                null
                );
            return RedirectToAction("Index");
        }

    }
}
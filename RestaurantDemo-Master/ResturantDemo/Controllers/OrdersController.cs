using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResturantDemo.Models;
using Microsoft.AspNet.Identity;

namespace ResturantDemo.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,timeCreated,isFulfilled,userId,destination")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,timeCreated,isFulfilled,userId,destination")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShoppingCart()
        {
            var cart = HttpContext.Session["cart"] as Orders;
            return PartialView("_ShoppingCart", cart);
        }

        [HttpPost]
        public ActionResult ShoppingCart(int id)
        {
            var cart = HttpContext.Session["cart"] as Orders;
            if (cart == null && User.Identity.IsAuthenticated)
            {
                cart = new Orders() { userId = User.Identity.GetUserId() };
            }
            var db = new ApplicationDbContext();
            var item = db.MenuItems.FirstOrDefault(f => f.Id == id);
            cart.Order.Add(item);
            if (HttpContext.Session["cart"] as Orders == null)
            {
                HttpContext.Session.Add("cart", cart);
            }
            else
            {
                HttpContext.Session["cart"] = cart;
            }
            return PartialView("_ShoppingCart", cart);

        }

        [Authorize(Roles = "customer")]
        public ActionResult PlaceOrder()
        {
            var cart = HttpContext.Session["cart"] as Orders;
            if (cart != null)
            {
                cart.userId = User.Identity.GetUserId();
                db.Orders.Add(cart);
                db.SaveChanges();
                HttpContext.Session.Remove("cart");
            }

            return View("PlaceOrder");
        }

        public ActionResult ViewCart()
        {
            var cart = HttpContext.Session["cart"] as Orders;
            return View("ViewCart", cart);
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

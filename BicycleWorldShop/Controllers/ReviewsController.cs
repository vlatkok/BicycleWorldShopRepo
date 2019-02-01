using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BicycleWorldShop.DAL;
using BicycleWorldShop.Models;

namespace BicycleWorldShop.Controllers
{
    public class ReviewsController : Controller
    {
        private BicycleWorldShopContext db = new BicycleWorldShopContext();

        // GET: Reviews
        public ActionResult Index()
        {
            return View(db.Reviews.ToList());
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
        [HttpGet]
        public ActionResult Create( int Id)
        {
           
                var productt = from a in db.Products
                           where a.Id == Id
                           select a;
                Product p = (Product) productt.FirstOrDefault();
                ViewData["MProduct"] = p;
        
         
            //   ViewBag.Name = p.Name;
            //    ViewBag.Brand = p.Brand;
            //    ViewBag.Price = p.Price;
            //    ViewBag.Description = p.Description;
            //    ViewBag.Color = p.Color;
            //    ViewBag.Id = Id;

            var category = from a in db.Categories
                           where a.Id == p.Category_Id
                           select a;
            Category categ = (Category)category.FirstOrDefault();

           
            ViewData["MyCategory"] = categ;
            int ratingProduct = 0;
            int counter = 0;
            try
            {
                var reviews = from a in db.Reviews
                              where a.ProductId == Id
                              select a.Rating;
                if (reviews.Count() > 1 && reviews != null)
                { ratingProduct = Convert.ToInt16(Math.Round(reviews.Average(), 2));

                }
                else if (reviews.Count() == 1 && reviews != null)
                {
                    ratingProduct = Convert.ToInt16(reviews.FirstOrDefault());
                }
                foreach (var a in reviews)
                {
                    counter++;
                }

                    }

            catch (NullReferenceException e)
            {
                ratingProduct = 0;
            }

            ViewBag.Rating = ratingProduct;
            ViewBag.NumReviews = counter;
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,Headline,Rating,Pros,Cons,BestUses,DescribeYourself,CyclingStyle,Gift,Comments,BottomLine,Nickname,Location,Email,DatePosted")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                var item = from a in db.Products
                           where a.Id == review.ProductId
                           select a;
                Product p = (Product)item.FirstOrDefault();
                return RedirectToAction("Preview","Products",new {id= p.Name });
            }

       //     return View(review);

            return RedirectToAction("LogIn", "Account", new { area = "Admin" });
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,Headline,Rating,Pros,Cons,BestUses,DescribeYourself,CyclingStyle,Gift,Comments,BottomLine,Nickname,Location,Email,DatePosted")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
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
            db.Reviews.Remove(review);
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

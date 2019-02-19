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
{// Controller about basic operations with reviews from the users of the web app.
    public class ReviewsController : Controller
    {
        private BicycleWorldShopContext db = new BicycleWorldShopContext();

        // GET: Reviews
        // Get a list of reviews from the database.
        public ActionResult Index()
        {
            return View(db.Reviews.ToList());
        }

        // GET: Reviews/Details/5
        // View a details for a review with a specific id
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

            // Action method to show  necessary data to view form for creating new review for particular product
        [HttpGet]
        public ActionResult Create( int Id)
        {
           // get the product
                var productt = from a in db.Products
                           where a.Id == Id
                           select a;
                Product p = (Product) productt.FirstOrDefault();
                ViewData["MProduct"] = p;      
         
           
            //get the category for the product
            var category = from a in db.Categories
                           where a.Id == p.Category_Id
                           select a;
            Category categ = (Category)category.FirstOrDefault();

           
            ViewData["MyCategory"] = categ;
            int ratingProduct = 0;
            int counter = 0;

            //get the average rating
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
        // Post Action method about saving the new review into the database. 
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

       

            return RedirectToAction("LogIn", "Account", new { area = "Admin" });
        }

        // GET: Reviews/Edit/5
        //Action which is used to show the view which edits review with specific id.
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
        // Action to save changes after edit
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
        // Action is used about deletation of the review
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
        // Action is used for deleting the product from the database
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Action to dispose data from the database
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

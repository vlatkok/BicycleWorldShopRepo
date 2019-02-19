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
using PagedList;
using System.IO;
using System.Globalization;

namespace BicycleWorldShop.Controllers
{        // Controller  with actions about Preview, Creating , Deleting and Editing products, Searching product, Finding subcategories and products availability!
    public class ProductsController : Controller
    {
        private BicycleWorldShopContext db = new BicycleWorldShopContext();

        // GET: Products

        // Preview list of the products   
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        // Preview all saved Details about certain product
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        //Get action method about initial call to create new Product
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Post action method for saving new created product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Description,Feature,Video,PartNumber,Color,Availability,Brand,Size,Gender,LastUpdated,Category_Id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        // Editing product details by specific id, finding the product by id end sending to the editing view
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Post action method about saving edited details for a product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Description,Feature,Video,PartNumber,Color,Availability,Brand,Size,Gender,LastUpdated,Category_Id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        // deleting product by id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        // Post action method about deleting product
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Dispose the product
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Post action method that return selection list with subcategories from one product type
        [HttpPost]
        public ActionResult GetSubcategoryByID(string types)
        {
            List<Category> objsubcategory = new List<Category>();
            objsubcategory = db.Categories.Where(m => m.Name== types).ToList();

            var selectList = new List<SelectListItem>();

            foreach (var element in objsubcategory)
            {
                selectList.Add(new SelectListItem
                {
                    Value =  element.Id.ToString(),
                    Text = element.SubCategoryName
                });
            }

        
            return Json(selectList);
        }

      //Action about search page for products 
        public ActionResult SearchPreview()
        {
            return View();
        }
        // Get method about searching all products by given optins, name, pagenumber and sorting the product results
        [HttpGet]
        public ActionResult Search(string option,string search, int? pageNumber, string sort)
            {
            List<Product> orderlist = new List<Product>();
           if (string.IsNullOrEmpty(sort))
            { sort = ""; }
            ViewBag.SortReviews = sort == "Customer Reviews Descending" ? "Customer Reviews Ascending" : "Customer Reviews Descending";
            ViewBag.SortPrice = sort == "Price High to Low" ? "Price Low to High" : "Price High to Low";

          var records = db.Products.AsQueryable();          

            // find the products by selected option

           if (option == "Gender")
            {
              records=records.Where(x => x.Gender.Contains(search) || search == null);
            }
            else if (option == "Feature")
            {
               records=records.Where(x => x.Feature.Contains( search) || search == null);
            }
            else if (option == "Description")
            {
                records=records.Where(x => x.Description.Contains(search) || search == null);
            }
            else if (option == "Brand")
            {
               records=records.Where(x => x.Brand.Contains(search) || search == null);
            }
            else if (option == "Color")
            {
               records= records.Where(x => x.Color.Contains(search) || search == null);
            }
            else if (option == "Availability")
            {
              records=records.Where(x => x.Availability.Contains(search) || search == null);
            }
         
            else
            {
                records=records.Where(x => x.Name.Contains(search) || search == null);
            }


           // sorting the results
            if (sort != "")
            {

                Dictionary<Product, int> ratingresults = new Dictionary<Product, int>();
                Dictionary<Product, int> dicttemp = new Dictionary<Product, int>();

                if (sort == "Customer Reviews Descending")

                {

                    ViewBag.SortType = "Customer Reviews Descending";
                    //// da se najdat site reviews za opredelen proizvod...
                    var partialresult = (from p in records
                                         from a in db.Reviews
                                         where p.Id == a.ProductId
                                         group a by a.ProductId into groupresult
                                         select new {
                                             ProductId = groupresult.Key,

                                             TotalRating = Math.Round(groupresult.Average(x=>x.Rating))
                                         }).ToList();
                  
                    //groupby ovdeka.....
                    foreach (var item in records)
                    {
                        for(int i=0;i<partialresult.Count();i++)
                        {

                            if (item.Id ==partialresult.ElementAt(i).ProductId)
                            {

                                ratingresults.Add(item, Convert.ToInt16(partialresult.ElementAt(i).TotalRating));

                            }



                        }


                    }


                    foreach (var item in records)
                    {
                        for (int i = 0; i < partialresult.Count(); i++)
                        {
                            if (item.Id != partialresult.ElementAt(i).ProductId)
                            {
                                if (!dicttemp.ContainsKey(item))
                                { dicttemp.Add(item, 0); }

                            }

                        }
                    }

                    //merge two dictionaries
                    foreach (var temp in dicttemp)
                    {

                        if (!ratingresults.ContainsKey(temp.Key))

                      { ratingresults.Add(temp.Key, temp.Value); }

                    }

                    ratingresults.OrderByDescending(m=>m.Value);
                    ViewData["SortResult"] = ratingresults;

                    List<Product> orderedlist = new List<Product>();
                    for (int i = 0; i < ratingresults.Count(); i++)
                    {
                        orderedlist.Add(ratingresults.ElementAt(i).Key);

                    }

                    


                   records = orderedlist.AsQueryable();
                    
                   
                }

                if (sort == "Customer Reviews Ascending")
                {

                    ViewBag.SortType = "Customer Reviews Ascending";
                    //// da se najdat site revies za opredelen proizvod...
                    var partialresult = (from p in records
                                         from a in db.Reviews
                                         where p.Id == a.ProductId
                                         group a by a.ProductId into groupresult
                                         select new
                                         {
                                             ProductId = groupresult.Key,

                                             TotalRating = Math.Round(groupresult.Average(x => x.Rating))
                                         }).ToList();

                    //groupby ovdeka.....
                    foreach (var item in records)
                    {
                        for (int i = 0; i < partialresult.Count(); i++)
                        {

                            if (item.Id == partialresult.ElementAt(i).ProductId)
                            {

                                ratingresults.Add(item, Convert.ToInt16(partialresult.ElementAt(i).TotalRating));

                            }


                        }


                    }

                    foreach (var item in records)
                    {
                        for (int i = 0; i < partialresult.Count(); i++)
                        {
                            if (item.Id != partialresult.ElementAt(i).ProductId)
                            {
                                if (!dicttemp.ContainsKey(item))
                                { dicttemp.Add(item, 0); }

                            }

                        }
                    }

                    //merge two dictionaries
                    foreach (var temp in dicttemp)
                    {

                        if (!ratingresults.ContainsKey(temp.Key))

                        { ratingresults.Add(temp.Key, temp.Value); }

                    }
                    //order by ascending
                    ratingresults.OrderBy(m => m.Value);
                    ViewData["SortResult"] = ratingresults;

                    List<Product> orderedlist = new List<Product>();
                    for (int i = 0; i < ratingresults.Count(); i++)
                    {
                        orderedlist.Add(ratingresults.ElementAt(i).Key);

                    }

                    records = orderedlist.AsQueryable();

                }


            }

            if (sort == "Price High to Low")
            { records = records.OrderByDescending(x => x.Price); }
            else if (sort == "Price Low to High")
            {
                records = records.OrderBy(x => x.Price);
            }
            else if (sort == "")

            { records = records.OrderBy(x => x.Name);

            }  
                    
            return View("SearchPreview", records.ToPagedList(pageNumber ?? 1, 6));
        }

        //  return list from all categories
        public List<Category> GetAllCategories()
        {
          
            return db.Categories.ToList();
        }

        // Preview the product and the details about the product
        [HttpGet]
        public ActionResult Preview(string  id)
        {

            ViewBag.PName = id;
        var prs = db.Products.Where(m=>m.Name == id);          

          Category category = db.Categories.Find(prs.FirstOrDefault().Category_Id);
           ViewBag.Category = category.Name;
           ViewBag.Subcategory = category.SubCategoryName;

            // add rating gathering from previous reviews

            int ratingProduct = 0;
            int counter = 0;
            int productid = 0;
            try
            {
                productid = Convert.ToInt16((from a in db.Products
                                where a.Name == id
                                select a.Id).FirstOrDefault());

                var reviews = from a in db.Reviews
                              where a.ProductId == productid
                              select a.Rating;
                if (reviews.Count() > 1 && reviews != null)
                {
                    ratingProduct = Convert.ToInt16(Math.Round(reviews.Average(), 2));

                }
                else if(reviews.Count()==1 && reviews!=null)
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
            ViewData["Reviews"] = null;
            if (counter > 0)
            {

                var reviews = from a in db.Reviews
                              where a.ProductId == productid
                              select a;

                ViewData["Reviews"] = reviews;

            }





            return View(prs);

        }
        // Preview all reviews about certain product with particular order 
        [HttpGet]
        public ActionResult PreviewReviews(int id, int? pageNumber, string datesorder)
        {

            int counter = 0;
            var reviewsfound = null as IQueryable<Review>;
            ViewBag.productid = id;
            ViewBag.datesorder = datesorder;
            try
            {
                reviewsfound = from a in db.Reviews
                               where a.ProductId == id
                               select a;


                counter = reviewsfound.Count();
            }
            catch (NullReferenceException e)
            {

                counter = 0;
            }
            CultureInfo culture = new CultureInfo("en-US");
            switch (datesorder)  // ova e za newest 
            {
                case "Oldest":
                                                        
                    reviewsfound = reviewsfound.OrderBy(d => d.DatePosted);
                    break;
                case "Highest rating":
                    reviewsfound = reviewsfound.OrderByDescending(d => d.Rating);
                    break;
                case "Lowest rating":
                    reviewsfound = reviewsfound.OrderBy(d => d.Rating);
                    break;
                case "Most Helpful":
                    reviewsfound = reviewsfound.OrderByDescending(d => d.Comments.Length);
                    break;
                case "Least Helpful":
                    reviewsfound = reviewsfound.OrderBy(d => d.Comments.Length);
                    break;
                default:
                    reviewsfound = reviewsfound.OrderByDescending(d => d.DatePosted);
                    break;
            }         
            ViewBag.NumReviews = counter; 
         
            return PartialView(reviewsfound.ToList().ToPagedList(pageNumber ?? 1, 5));
        }


        // Post Action about checking the availability of some product with certain size or color
        [HttpPost]
        public ActionResult Availability(string color,string size,string name)
        {
           var available="" ;
            var pid = "";
            if (color != null && (color.ToString() != "0"))
            {

                if ((size != null) && (size.ToString()!= "0"))
                {
                    try
                    {
                        available = (from m in db.Products
                                     where (m.Color == color) && (m.Size == size) && (m.Name == name)
                                     select m.Availability).FirstOrDefault().ToString();

                        pid= (from m in db.Products
                              where (m.Color == color) && (m.Size == size) && (m.Name == name)
                              select m.Id).FirstOrDefault().ToString();
                    }

                    catch (NullReferenceException exp)
                    {
                        available = "Call for Availability";
                    }

                }
                else
                {
                    try
                    {
                        available = (from m in db.Products
                                     where (m.Color == color) && (m.Name == name)
                                     select m.Availability).FirstOrDefault().ToString();
                        pid= (from m in db.Products
                              where (m.Color == color) && (m.Name == name)
                              select m.Id).FirstOrDefault().ToString();
                    }
                    catch (NullReferenceException exp)
                    {
                        available = "Call for Availability";

                    }


                }

            }
            else
           {
                try
                {
                    available = (from m in db.Products
                                 where (m.Size == size) && (m.Name == name)
                                 select m.Availability).FirstOrDefault().ToString();
                    pid= (from m in db.Products
                          where (m.Size == size) && (m.Name == name)
                          select m.Id).FirstOrDefault().ToString();
                }
                catch (NullReferenceException ex)
                {
                    available = "Call for Availability";


                }

            }

            if (available == "yes")
            {
                available = "In Stock";

            }
            else if (available == "no")
            {
                available = "In Warehouse";

            }

            var result = new { available=available, pid=pid };


            return Json(result);
        }





    }
}

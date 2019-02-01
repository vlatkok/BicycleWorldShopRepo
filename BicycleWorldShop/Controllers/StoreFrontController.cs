using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BicycleWorldShop.DAL;
using BicycleWorldShop.Models;
using System.Web.Script.Serialization;

namespace BicycleWorldShop.Controllers
{
    public class StoreFrontController : Controller
    {

        private BicycleWorldShopContext db = new BicycleWorldShopContext();

       public static IEnumerable<Product> productsList;
       

        // GET: StoreFront
        [HttpGet]
        public ActionResult Index(string id)        
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Categories.FirstOrDefault(p=>p.SubCategoryName=="Mountain").Id
         int categoryid =  db.Categories.FirstOrDefault(p => p.SubCategoryName == id.ToString().Trim()).Id;
            if (categoryid == null)
            {
                return HttpNotFound();
            }

            var productItems = from m in db.Products
                           where m.Category_Id == categoryid
                           group m by m.Name into g
                           select g.FirstOrDefault();
/*
            productItems = from t in productItems
                       group t by t.Name into g
                       select g.First();
*/

            productsList = productItems.AsEnumerable().Distinct().ToList();         
        
            ViewBag.Products = productItems.AsEnumerable().Distinct().ToList();
            return View(productItems.ToList());
                        
        }

      
      
        public PartialViewResult GetList(string klasId)
        {
            

          IEnumerable<Product> products=null;
        
        var ids = new List<string>();

            if (!string.IsNullOrEmpty(klasId))
            {
                string a;
                for (int i = 0; i < klasId.Split(',').Length; i++)
                {
                    a = klasId.Split(',')[i];


                    ids.Add(a);
                }
               
              
               products = productsList.Where(t => ids.Contains(t.Size));



                //   productsList = ViewBag.Products;

                /*

                            if (!string.IsNullOrEmpty(klasId))
                            {
                                float a;
                                for (int i = 0; i < klasId.Split(',').Length; i++)
                                {
                                    a = (float)Convert.ToDouble(klasId.Split(',')[i]);


                                    ids.Add(a);
                                }

                                products = productsList.Where(t => ids.Contains(t.Size));

                            */
           
               
           //     prs = products.AsQueryable();

                /*         foreach (IEnumerable<Product> p in app)
                         { productsList.add(p); }


                */

            }

            //    else
            //    {
            //         rows = entities.Customers.Take(5);
            // 

            /*     var productresults = (from v in prs
                              select new
                            {
                                 v.Name,
                                v.Price                          
                              }).ToList();


        */
          
            
           return PartialView("ProductsPreview",products);
        // return   new JavaScriptSerializer().Serialize(productresults);
         //   return  Json(productresults);
         //  return Json(productsList);
        }

        [HttpPost]
        public ActionResult ProductsPreview(string klasId)
        {
           


            //  IQueryable<Product> prs = null;

             IEnumerable<Product> products = null;
        

            var ids = new List<float>();
            var items = new List<string>();
            var brands = new List<string>();
            var price = new List<string>();
            var gender = new List<string>();
            var size = new List<string>();
            var lastupdated = new List<int>();

            if (!string.IsNullOrEmpty(klasId))


            {
                // float a;

                // string 
                //    for (int i = 0; i < klasId.Split(',').Length; i++)
                //      {
                //        a = (float)Convert.ToDouble(klasId.Split(',')[i]);


                ///   ids.Add(a);
                //      }


                   for (int i = 0; i < klasId.Split(',').Length; i++)
                {
                    //a = (float)Convert.ToDouble(klasId.Split(',')[i]);
                    items.Add(klasId.Split(',')[i]);

                //    if ( == "Brand") { brands.Add(klasId.Split(',')[i]); }
               //     else if()

                    ///   ids.Add(a);
             //       ++i;
                }

                for (int i = 0; i < (items.Count-1); i++)
                {                    
                   
                    if (items[i + 1] == "Brand" ) { brands.Add(items[i]); }
                    else if (items[i + 1] == "Price" ) { price.Add(items[i]); }
                    else if (items[i + 1] == "Gender" ) { gender.Add(items[i]); }
                    else if (items[i + 1] == "Size" ) { size.Add(items[i]); }
                    else if (items[i + 1] == "LastUpdated") { lastupdated.Add(Convert.ToInt16(items[i]));}

                }

                IEnumerable<Product> bra = null;
                IEnumerable<Product> siz = null;
                IEnumerable<Product> gend = null;
                IEnumerable<Product> lu = null;


            //    products=from v in productsList
            ///             where 


                

                if (brands.Count > 0) { bra = productsList.Where(t => brands.Contains(t.Brand)); }

                if (size.Count > 0) { siz = productsList.Where(t => size.Contains(t.Size)); }
                if (gender.Count > 0) { gend = productsList.Where(t => gender.Contains(t.Gender)); }
                if (lastupdated.Count > 0) { lu = productsList.Where(t => lastupdated.Contains(Convert.ToDateTime(t.LastUpdated).Year)); }

                IEnumerable<Product> pric = Enumerable.Empty<Product>();
                //     var ass=null ;
                //   List<Product>
                if (price.Count > 0)
                {
                    
                    foreach (string i in price)
                    {
                        decimal downrange = (decimal)Convert.ToDouble(i.Split('-')[0]);
                        decimal uprange = (decimal)Convert.ToDouble(i.Split('-')[1]);

                        var temp = from m in productsList
                                   where (m.Price >=downrange ) && (m.Price <=uprange )
                                   select m;

                        pric=pric.Concat(temp);
                    }
                }

                // .Where(c => toBeFilteredCarIds.Any(g => g == c.CarId) && c.CarStatusId == 1);
                //  products = productsList.Where(c => ids.Any(g => g == c.Size));

                IEnumerable<Product> templist = Enumerable.Empty<Product>();
                int typesChanged = 0; //counter that counts types  of filter clicked
                if (bra != null)
                { templist = templist.Concat(bra);
                    typesChanged+=1;
                }
                if (siz != null)
                { templist = templist.Concat(siz);
                    typesChanged += 1;
                }
                if (gend != null)
                { templist = templist.Concat(gend);
                    typesChanged += 1;
                }

                if (lu != null)
                { templist = templist.Concat(lu);
                    typesChanged += 1;
                }
                if (pric!=null && pric.Any())
                { templist = templist.Concat(pric);
                    typesChanged += 1;
                }

                int counter = 0;
                foreach (var i in templist)
                {

                    counter++;

                }


                if (counter > 1 && typesChanged > 1)
                {
                    /// group by elements by filter condition from different categories

                    var distinctItems = from list in templist
                                        group list by list.Name into grouped
                                        where grouped.Count() > 1
                                        select grouped;


                    products = distinctItems.SelectMany(group => group).Distinct();

                }
                else if (counter > 1 && typesChanged == 1)
                {
                    //union of all elements of that types from same category
                    products = templist;

                }
                else if (counter == 1)
                {

                    products = templist;


                }
                else if (counter == 0)
                {


                    /// return  something for 0 elements and message

                    products = Enumerable.Empty<Product>();

                }


        






               
                             








                //     products = bra.Intersect(siz).Intersect(gend).Intersect(lu).Intersect(pric).ToList();



                //   products = productsList.Where(t => size.Contains(t.Size)).ToList();     


            }




            if (products == null)
            {

                products = productsList;

            }



            try
            {

               


            }
            catch (NullReferenceException e)
            { }
        



            return PartialView("ProductsPreview", products);

            // return   new JavaScriptSerializer().Serialize(productresults);
            //   return  Json(productresults);
            //  return Json(productsList);
        }






        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult FilterMenu(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string categoryname = db.Categories.FirstOrDefault(p => p.Id == id).Name;
            ViewBag.CategoryName = categoryname;
            var categories = from m in db.Categories
                             where m.Name == categoryname
                             select m;

            if (categories == null)
            {
                return HttpNotFound();
            }

            //db.Categories.ToList();

            //get all brands from that category ///

            var brands = from m in db.Products
                         where m.Category_Id==id
                         select m.Brand;


            ViewBag.Brands = brands.AsEnumerable().Distinct().ToList(); 

            return PartialView(categories.ToList());
        }

        [ChildActionOnly]
        public ActionResult Navigation(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           Category item = db.Categories.FirstOrDefault(p => p.Id == id);

            ViewBag.Name = item.Name;
            ViewBag.SubCategory = item.SubCategoryName;
               


            return PartialView();
        }

   

    }
}
 
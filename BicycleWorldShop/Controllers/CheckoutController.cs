using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BicycleWorldShop.Models;
using BicycleWorldShop.DAL;

namespace BicycleWorldShop.Controllers
{// This controller is used for checkout of the shopping cart
    [Authorize]
    public class CheckoutController : Controller
    {

        private BicycleWorldShopContext db = new BicycleWorldShopContext();
        const String PromoCode = "FREE";

        
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        // Post action in which the order from the shopping cart is created
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new CustomerOrder();

            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.CustomerUserName = User.Identity.Name;
                    order.DateCreated = DateTime.Now;

                    db.CustomerOrders.Add(order);
                    db.SaveChanges();

                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    db.SaveChanges();//we have received the total amount lets update it

                    return RedirectToAction("Complete", new { id = order.Id });
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                return View(order);
            }
        }
        // Completion of created order
        public ActionResult Complete(int id)
        {
            bool isValid = db.CustomerOrders.Any(
                o => o.Id == id &&
                     o.CustomerUserName == User.Identity.Name
                );

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;
using BicycleWorldShop.DAL;

namespace BicycleWorldShop.Controllers
{
    public class HomeController : Controller
    {
        private BicycleWorldShopContext db = new BicycleWorldShopContext();
        public ActionResult Index()
        {
            int elbikesid = 8;
            var electricBikesPromo = (from a in db.Products
                                      where a.Category_Id == elbikesid
                                      select a).Take(8);

            return View(electricBikesPromo);
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

        public ActionResult PrivacyInfo()
        {
            ViewBag.Message = "Your privacy page.";

            return View();
        }

        public ActionResult ServicePage()
        {
            ViewBag.Message = "Your service page.";

            return View();
        }
        public ActionResult Events()
        {
            ViewBag.Message = "Your events page.";

            return View();
        }

        public ActionResult Catalog()
        {
            ViewBag.Message = "Your catalog page.";

            return View();
        }



        [HttpGet]
        public ActionResult Emails()
        {
           

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Emails(ViewModels.EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("vlatko.kocoski@gmail.com"));  // replace with valid value 
                message.From = new MailAddress(model.Email);  // replace with valid value
                message.Subject = model.Subject.ToString();
                message.Body = string.Format(body, model.Name, model.Email, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new System.Net.NetworkCredential
                    {
                        UserName = "vkoctesting@gmail.com",  // replace with valid value
                        Password = "test12345@"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }







    }
}
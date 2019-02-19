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

    //HomePage controller
    public class HomeController : Controller
    { // Access to database
        private BicycleWorldShopContext db = new BicycleWorldShopContext();

        // HomePage action that finds first 8 electric bikes, and sends data to the appropriate view for the homepage
        public ActionResult Index()
        {




            int elbikesid = 8;
            var electricBikesPromo = (from a in db.Products
                                      where a.Category_Id == elbikesid
                                      select a).Take(8);

            return View(electricBikesPromo);
        }
        // Auxilary action methods
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        // Auxilary action methods
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // Auxilary action methods
        public ActionResult PrivacyInfo()
        {
            ViewBag.Message = "Your privacy page.";

            return View();
        }
        // Auxilary action methods
        public ActionResult ServicePage()
        {
            ViewBag.Message = "Your service page.";

            return View();
        }
        // Auxilary action methods
        public ActionResult Events()
        {
            ViewBag.Message = "Your events page.";

            return View();
        }
        // Auxilary action methods
        public ActionResult Catalog()
        {
            ViewBag.Message = "Your catalog page.";

            return View();
        }

        // HttpGet action to return email view

        [HttpGet]
        public ActionResult Emails()
        {
           

            return View();
        }
        // HttpPost action that enables user to send email to the BicycleWorldShop email (vkoctesting@gmail.com)
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
                    { //Website's official e-mail username
                        UserName = "vkoctesting@gmail.com",  // replace with valid value
                        Password = "test12345@"  // replace with valid value
                        //website's official e-mail password
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
        // Action that return  view for succesfully sent email
        public ActionResult Sent()
        {
            return View();
        }







    }
}
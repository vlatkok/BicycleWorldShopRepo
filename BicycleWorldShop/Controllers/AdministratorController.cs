using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BicycleWorldShop.Controllers
{// This controoller is used for redirection of the administrator's activities
    public class AdministratorController : Controller
    {
        // GET: Administrator
        [Authorize(Users="Administrator")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
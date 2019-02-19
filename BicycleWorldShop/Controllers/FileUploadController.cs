using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BicycleWorldShop.Controllers
{// This controller is used for administrator purposes ( manipulation with data)
    public class FileUploadController : Controller
    {
       
        // Action which reads all images from server location for further presentation
        // GET: FileUpload
        public ActionResult Index()
        {
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/Content/Images/Products"))
                 .Select(fn => "~/Content/Images/Products/" + Path.GetFileName(fn));



            return View();
        }
        // Post method for the upload of product images with appropriete parameters
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase[] files)
        {
            int permittedSizeInBytes = 5000000;//5mb

            string permittedType = "image/jpeg,image/jpg";

            int counter = 0;

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null  )
                    {if (file.ContentLength < permittedSizeInBytes)
                        {
                            if (permittedType.Split(",".ToCharArray()).Contains(file.ContentType))
                            {
                                var InputFileName = Path.GetFileName(file.FileName);
                                var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Images/Products/") + InputFileName);
                                //Save file to server folder  
                                file.SaveAs(ServerSavePath);
                                counter++;
                                //assigning file uploaded status to ViewBag for showing message to user.  
                            }
                        }
                    }

                }

                ViewBag.UploadStatus = counter.ToString() + " files uploaded successfully.";

            }
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/Content/Images/Products"))
               .Select(fn => "~/Content/Images/Products/" + Path.GetFileName(fn));
            return View();
        }
        // Action about deleting already saved product image to the server
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoto(string photoFileName)
        {
            //Session["DeleteSuccess"] = "No";
            var photoName = "";
            photoName = photoFileName;
            string fullPath = Request.MapPath("~/Content/Images/Products/"
            + photoName);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                //Session["DeleteSuccess"] = "Yes";
            }
            return RedirectToAction("Index");
        }


    }
}
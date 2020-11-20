using FileUpload.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ImageModel img)
        {
            string Filename = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
            string FileExt = Path.GetExtension(img.ImageFile.FileName);
            Filename = DateTime.Now.ToString("yyyyMMdd") + "-"+Filename.Trim()+ FileExt;
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            img.ImagePath = UploadPath + Filename;
            img.ImageFile.SaveAs(img.ImagePath);
            return Content("Upload Successful!");
        }
    }
}
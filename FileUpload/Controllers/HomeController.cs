using FileUpload.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas.Draw;

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
            return Content("Upload Successful! <a href="+"./Home/Index"+">Upload Another</a>");
        }

        public ActionResult MergeImages()
        {
            string imgFolder = @"C:\Users\Habiba\Desktop\APWDN\FileUpload\FileUpload\UploadedImages\";
            string pdfFolder = @"C:\Users\Habiba\Desktop\APWDN\FileUpload\FileUpload\Generated PDFs\demo.pdf";
            
            //Getting Images
            var imageFiles = Directory.GetFiles(imgFolder, "*.jpg");
            List<string> imageList = new List<string>();
            foreach(string image in imageFiles)
            {
                imageList.Add(image);
            }

            //Creating the pdf document
            PdfWriter writer = new PdfWriter(pdfFolder);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            //Header
            Paragraph header = new Paragraph("DEMO").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
            //New Line
            Paragraph newline = new Paragraph(new Text("\n"));
            //Adding Header & New Line
            document.Add(newline);
            document.Add(header);
            //Line Separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            //Adding the Images
            foreach(string i in imageList)
            {
                Image img = new Image(ImageDataFactory.Create(i)).SetTextAlignment(TextAlignment.CENTER);
                document.Add(img);
            }

            //Closing the document & returning content
            document.Close();
            return Content("PDF Generation Successful! <a href=" + "./Home/MergeImages" + ">Generate Another</a>");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileUpload.Models
{
    public class ImageModel
    {
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
}
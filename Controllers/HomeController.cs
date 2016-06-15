using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mosaicus.Controllers
{
    public class HomeController : Controller
    {

        public int PicSize = 5;

        // GET: Home
        public ViewResult Index()
        {
            return View();
        }

        // GET: Home
        [HttpPost]
        public string Processing()
        {
            string result = "Error!";

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("Data/UsersPhoto/"), fileName);
                    file.SaveAs(path);

                    result = Models.Mosaic.processing(Server.MapPath("Data"), fileName, PicSize);
                }
            }

            return result;
        }

        // GET: UpdateImages
        [HttpGet]
        public string UpdateImages()
        {
            string result;

            result = Models.PictureDB.getPrictures(Server.MapPath("Data"), PicSize);
            
            return result;
        }
 
    }
}
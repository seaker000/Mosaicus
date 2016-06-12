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
        public string Processing()
        {
            string result;
            result = Models.Mosaic.processing(Server.MapPath("Data"), "main5.jpg", PicSize);

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
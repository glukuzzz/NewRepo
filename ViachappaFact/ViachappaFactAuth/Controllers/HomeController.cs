using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViachappaFactAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var p = new Dictionary<string, long>();

            foreach (string f in Request.Files)
            {
                var file = Request.Files[f];
                p.Add(file.FileName, file.ContentLength);
                
                //byte[] fileData = null;
                //using (var binaryReader = new BinaryReader(file.InputStream))
                //{
                //    fileData = binaryReader.ReadBytes(file.ContentLength);
                //}
              
                
            }
            ViewBag.Test = p;
            return View();
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
    }
}
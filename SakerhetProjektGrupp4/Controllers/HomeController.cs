using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SakerhetProjektGrupp4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string anvNamn, string losord)
        {
            var a = anvNamn; 
            var b = losord;
            return View();
        }
        

    }
}
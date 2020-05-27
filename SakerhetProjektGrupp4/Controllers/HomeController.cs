using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakerhetProjektGrupp4.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SakerhetProjektGrupp4.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(string anvNamn, string losord)
        {
            PersonalModel PersInfo = new PersonalModel();

            PersInfo.AnvandarNamn = anvNamn;
            PersInfo.Losenord = losord;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54501/");
                var response = client.PostAsJsonAsync("Login", PersInfo).Result;


                if (response.IsSuccessStatusCode)
                {
                    var AnvSvar = response.Content.ReadAsStringAsync().Result;
                    // ResponseAnv = JsonConvert.DeserializeObject<List<PersonalModel>>(AnvSvar);  //THE FCKING SACRATE CODE. TOUCH, DIE.
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }

            return View();
        }

        //[HttpPost]
        //public ActionResult Index(string anvNamn, string losord)
        //{
        //    AnvandarModel test = new AnvandarModel();
        //    List<PersonalModel> ResponseAnv = new List<PersonalModel>();
                
        //    test.Email = anvNamn;
        //    test.Losenord = losord;

        //    using (var client = new HttpClient())
        //    {

        //        client.BaseAddress = new Uri("http://localhost:54501/");
        //        var response = client.PostAsJsonAsync("LoggaIn", test).Result;


        //        if (response.IsSuccessStatusCode)
        //        {
        //            var AnvSvar = response.Content.ReadAsStringAsync().Result;
        //            // ResponseAnv = JsonConvert.DeserializeObject<List<PersonalModel>>(AnvSvar);  //THE FCKING SACRATE CODE. TOUCH, DIE.
        //            Console.Write("Success");
        //        }
        //        else
        //            Console.Write("Error");
        //    }

        //    return View();
        //}
    }
}
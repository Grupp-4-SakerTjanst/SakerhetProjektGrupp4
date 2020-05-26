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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        //Metod för att hämta PersonalLista RAW-data
        
        [HttpPost]
        public async Task<ActionResult> Index(string Email, string Losenord)
        {
            string Baseurl = "http://localhost:54501/"; //Ganim
            List<AnvandarModel> AnvInfo = new List<AnvandarModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("SkapaAnvandare");
                if (Res.IsSuccessStatusCode)
                {
                    var AnvSvar = Res.Content.ReadAsStringAsync().Result;
                    AnvInfo = JsonConvert.DeserializeObject<List<AnvandarModel>>(AnvSvar);
                }
                
                
                return View(AnvInfo);
            }
        }
  

        

    }
}
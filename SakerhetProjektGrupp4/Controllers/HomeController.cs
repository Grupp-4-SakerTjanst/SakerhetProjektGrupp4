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
        [Route("/{Id}")]
        [HttpPost]
        public async Task<ActionResult> Index(string anvNamn,string Losenord)
        {
            string Baseurl = "http://localhost:56539/"; //Ganim
            List<AnvandarModel> LogInInfo = new List<AnvandarModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("Personal/1");
                if (Res.IsSuccessStatusCode)
                {
                    var LogInSvar = Res.Content.ReadAsStringAsync().Result;
                    LogInInfo = JsonConvert.DeserializeObject<List<AnvandarModel>>(LogInSvar);
                }
                
                
                return View(LogInInfo);
            }
        }
  

        

    }
}
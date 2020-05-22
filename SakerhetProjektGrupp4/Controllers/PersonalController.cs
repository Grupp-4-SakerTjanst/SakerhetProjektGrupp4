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
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Index()
        {
            return View();
        }

        [Route("Personals/{Id}")]
        [HttpPost]
        public async Task<ActionResult> Index(string anvNamn, string Losenord)
        {
            string Baseurl = "http://localhost:56539/"; //Ganim
            List<PersonalModel> AnvInfo = new List<PersonalModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("Personals/1");
                if (Res.IsSuccessStatusCode)
                {
                    var AnvSvar = Res.Content.ReadAsStringAsync().Result;
                    AnvInfo = JsonConvert.DeserializeObject<List<PersonalModel>>(AnvSvar);
                }


                return View(AnvInfo);
            }
        }
    }
}
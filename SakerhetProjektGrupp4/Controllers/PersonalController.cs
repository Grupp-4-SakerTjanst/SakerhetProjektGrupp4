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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;


namespace SakerhetProjektGrupp4.Controllers
{
    public class PersonalController : Controller
    {
        

        // GET: Personal
        public ActionResult Index()
        {
           
            return View();
        }


        [HttpPost]
        public ActionResult Index(string anvNamn, string losen)
        {
            PersonalModel test = new PersonalModel();
            List<PersonalModel> ResponseAnv = new List<PersonalModel>();

            test.AnvandarNamn = anvNamn;
            test.Losenord = losen;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54501/");
                var response = client.PostAsJsonAsync("Login",test).Result;
                
                
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

        public ActionResult SkapaPersonal()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SkapaPersonal(PersonalModel Personal)
        {
            PersonalModel tempPers = new PersonalModel();
            List<PersonalModel> ResponseAnv = new List<PersonalModel>();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54501/");
                var response = client.PostAsJsonAsync("SkapaPersonal", Personal).Result;

                if (response.IsSuccessStatusCode)
                {
                    var AnvSvar = response.Content.ReadAsStringAsync().Result;
                   
                    ResponseAnv = JsonConvert.DeserializeObject<List<PersonalModel>>(AnvSvar);  //THE FCKING SACRATE CODE. TOUCH, DIE.
                    Console.Write("Success");
                    
                }
                else
                    Console.Write("Error");
            }

            



            return View();
        }
    }
}
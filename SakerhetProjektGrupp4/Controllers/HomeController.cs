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
        
        [HttpPost]
        public ActionResult Index(string anvNamn, string losord)
        {
            

            PersonalModel PersMod = new PersonalModel { AnvandarNamn = anvNamn, Losenord = losord };
            if (anvNamn == null || losord == null || anvNamn == "" || losord == "")
            {
                ModelState.AddModelError("index", "Du måste fylla i både användarnamn och lösenord");
                return View();
            }
           
            
            bool validUser = false;

           
            validUser = AnvCheck(PersMod);

            if (validUser == true)
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(PersMod.AnvandarNamn, false);
                return RedirectToAction("Index", "Personal");
            }
            ModelState.AddModelError("", "Inloggningen ej godkänd");
            
            return RedirectToAction("Index");


        }

       
        private bool AnvCheck(PersonalModel Person)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://193.10.202.74/inlogg/personals");

                var response = client.PostAsJsonAsync("Login", Person).Result;
                if (response.IsSuccessStatusCode)
                {
                    string PersonRes = response.Content.ReadAsStringAsync().Result;
                    PersonalModel PersBehorig = JsonConvert.DeserializeObject<PersonalModel>(PersonRes);
                    if (PersBehorig != null)
                    {
                        if (PersBehorig.BehorighetsNiva == 3)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
        }

    }
   
}
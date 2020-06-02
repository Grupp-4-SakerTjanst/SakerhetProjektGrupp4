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
    [Authorize]
    public class PersonalController : Controller
    {
      

        private PersonalModel db = new PersonalModel();

        string Baseurl = "http://193.10.202.74/personal/personal"; //Detta visar första sidan för admin att utföra CRUD
        
        public async Task<ActionResult> Index()
        {

            List<PersonalModel> PersonalInfo = new List<PersonalModel>();
            try
            {

                using (var client = new HttpClient())
                {
               
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                   
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                   
                    HttpResponseMessage Res = await client.GetAsync("personal");
                    
                    if (Res.IsSuccessStatusCode)
                    {
              
                        var PersonalResponse = Res.Content.ReadAsStringAsync().Result;

                        PersonalInfo = JsonConvert.DeserializeObject<List<PersonalModel>>(PersonalResponse);

                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Något gick fel" + e.Message);
                throw;
            }
            finally
            {
                //close connection
            }
            //returning the employee list to view  
            return View(PersonalInfo);
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
                client.BaseAddress = new Uri("http://193.10.202.74/inlogg/personals");
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
            return Redirect("Index");
        }

        // GET: Student
        public ActionResult TaBortPersonal(int? id)
        {
            //IList<PersonalModel> ResponseAnv = null;
            PersonalModel ResponseAnv = new PersonalModel();
            //PersonalModel person = new PersonalModel();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://193.10.202.74/personal/");
                var response = client.GetAsync("personal/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var PersonalResponse = response.Content.ReadAsStringAsync().Result;
                    ResponseAnv = JsonConvert.DeserializeObject<PersonalModel>(PersonalResponse);
                }
            }

            return View(ResponseAnv);
        }

        [HttpPost]
        public ActionResult TaBortPersonal(PersonalModel DelPer) //KLAR
        {
            PersonalModel ResponseAnv = new PersonalModel();
            //PersonalModel person = new PersonalModel();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://193.10.202.74/personal/");
                var response = client.DeleteAsync("personal/" + DelPer.Id).Result;


                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }
            return RedirectToAction("Index", "Personal");

        }

        
        public ActionResult UppdateraPersonal(int id) // KLAR ÄNDRA INGENTING
        {
            PersonalModel person = new PersonalModel();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://193.10.202.74/personal/");
                var response = client.GetAsync("personal/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var UpdateResponse = response.Content.ReadAsStringAsync().Result;
                    person = JsonConvert.DeserializeObject<PersonalModel>(UpdateResponse);
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult UppdateraPersonal(PersonalModel updPer)
        {
            PersonalModel ResponseAnv = new PersonalModel();
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("http://193.10.202.74/personal/"); //
                var response = client.PutAsJsonAsync<PersonalModel>("personal/", updPer).Result; //PutAsync

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                    //Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }
            return View();

        }


        public async Task<ActionResult> Drift()
        {

            List<DriftModel> test = new List<DriftModel>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54501/");

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage Res = await client.GetAsync("DriftStatus");

                if (Res.IsSuccessStatusCode)
                {

                    var PersonalResponse = Res.Content.ReadAsStringAsync().Result;

                    test = JsonConvert.DeserializeObject<List<DriftModel>>(PersonalResponse);

                }
            }


            return View(test);
        }

    }

}

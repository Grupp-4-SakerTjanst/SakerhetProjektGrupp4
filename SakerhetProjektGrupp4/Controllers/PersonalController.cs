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

        // GET: Student
        public ActionResult TaBortPersonal(int? id)
        {
            IList<PersonalModel> ResponseAnv = null;
            PersonalModel person = new PersonalModel();
           


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://193.10.202.74/personal/personal");
                //HTTP GET
                var responseTask = client.GetAsync("personal");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PersonalModel>>();
                    readTask.Wait();

                    ResponseAnv = readTask.Result;
                }
            }

            return View(person);
        }
        [HttpPost]
        public ActionResult TaBortPersonal(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://193.10.202.74/personal/personal/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("personal/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

    }

}

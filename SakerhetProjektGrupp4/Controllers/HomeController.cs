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

        //Hosted web API REST Service base url  
        string Baseurl = "http://localhost:54501/"; //Minnas
        public async Task<ActionResult> RESTKoppling(string anvNamn, string losord)
        {

            List<AnvandarModel> AnvInfo = new List<AnvandarModel>();
            try
            {

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("Anvandares/1");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var AnvSvar = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        AnvInfo = JsonConvert.DeserializeObject<List<AnvandarModel>>(AnvSvar);

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
            return View(AnvInfo);
        }




    }
}
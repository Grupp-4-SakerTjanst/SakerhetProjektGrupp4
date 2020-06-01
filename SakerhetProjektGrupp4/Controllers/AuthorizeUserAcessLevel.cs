using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SakerhetProjektGrupp4.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakerhetProjektGrupp4.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    public class AuthorizeUserAcessLevel : AuthorizeAttribute
    {

   
        public int UserRole { get; set; }


        public PersonalModel HamtaBehorig(string AnvNamn, string Losenord)
        {

            PersonalModel ResponseAnv = new PersonalModel();

            ResponseAnv.AnvandarNamn = AnvNamn;
            ResponseAnv.Losenord = Losenord;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://193.10.202.74/inlogg/personals");
                var response = client.PostAsJsonAsync("Login", ResponseAnv).Result;
                if (response.IsSuccessStatusCode)
                {

                    var PersonalResponse = response.Content.ReadAsStringAsync().Result;
                    ResponseAnv.BehorighetsNiva = JsonConvert.DeserializeObject<PersonalModel>(PersonalResponse).BehorighetsNiva;

                }

                return (ResponseAnv);

            }
        }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            PersonalModel ResponseAnv = new PersonalModel();
            var AnvNamn = (httpContext.Request.RequestContext.RouteData.Values["anvNamn"] as string)
             ??
             (httpContext.Request["anvNamn"] as string);
            var Losenord = (httpContext.Request.RequestContext.RouteData.Values["losord"] as string)
             ??
             (httpContext.Request["losord"] as string);

             ResponseAnv = HamtaBehorig(AnvNamn, Losenord);
            string CurrentUser = HttpContext.Current.User.Identity.Name.ToString();

            if (UserRole <= ResponseAnv.BehorighetsNiva)
            {
                
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
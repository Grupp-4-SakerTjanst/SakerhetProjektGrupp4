using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakerhetProjektGrupp4.Models
{
    public class PersonalSaftey
    {
       
        public int Id { get; set; }

       
        public string AnvandarNamn { get; set; }

        
        public string Losenord { get; set; }

        public int BehorighetsNiva { get; set; }
    }
}
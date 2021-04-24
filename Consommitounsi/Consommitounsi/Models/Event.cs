using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class Event
    {
        public long id_e { get; set; }

        public string Name_e { get; set; }

        public string Location_e { get; set; }

        public DateTime Date_e { get; set; }

        public int Max_participants_e { get; set; }
        
        public Admin admin { get; set; }





    }
}
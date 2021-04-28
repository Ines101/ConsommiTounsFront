using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class Event
    {
        public long id_e { get; set; }

        public string name_e { get; set; }

        public string location_e { get; set; }
        public DateTime date_e { get; set; }
        public int max_participants_e { get; set; }
        public Admin admin { get; set; }
    }
}
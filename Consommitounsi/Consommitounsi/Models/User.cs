using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class User
    {
        public long id_u { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public long phonenumber { get; set; }

    }
}
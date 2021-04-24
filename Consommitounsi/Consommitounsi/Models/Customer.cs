using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class Customer : User
    {
        

        public string DeliveryAdress { get; set; }
        public long Contact { get; set; }

    }
}
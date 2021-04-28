 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class Shipment
    {
        public long id_shipment { get; set; }

        public string status { get; set; }

        public float fees { get; set; }
        public DateTime date { get; set; }

        public DeliveryMan deliveryMan  { get; set; }
       
	}
}
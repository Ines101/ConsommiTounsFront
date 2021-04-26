using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class Reclamation
    {
        private long reclamationId { get; set; }
        private string subject { get; set; }
        private string description { get; set; }
        private DateTime created { get; set; }
        private Decision decision { get; set; }
        private User user { get; set; }

    }
}
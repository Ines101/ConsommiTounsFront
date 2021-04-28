using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class Reclamation
    {
        public long reclamtionId { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public String decision { get; set; }
        public User user { get; set; }

       public Reclamation()
        {
        }

        public Reclamation(long reclamtionId, string subject, string description,string decision, DateTime created, User user)
        {
            this.reclamtionId = reclamtionId;
            this.subject = subject;
            this.description = description;
            this.created = created;
            this.decision = decision;
            this.user = user;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
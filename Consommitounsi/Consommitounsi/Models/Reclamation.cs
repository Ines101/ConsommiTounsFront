using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class Reclamation
    {
        public long reclamationId { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public String decision { get; set; }
        public User user { get; set; }

        public Reclamation()
        {
        }

        public Reclamation(long reclamationId, string subject, string description,string decision, DateTime created, User user)
        {
            this.reclamationId = reclamationId;
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
            int hashCode = -150832125;
            hashCode = hashCode * -1521134295 + reclamationId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(subject);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            hashCode = hashCode * -1521134295 + created.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(decision);
            hashCode = hashCode * -1521134295 + EqualityComparer<User>.Default.GetHashCode(user);
            return hashCode;
        }
    }
}
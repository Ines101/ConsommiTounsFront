using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class User
    {
        public long userId { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public string lastname { get; set; }



        public string email { get; set; }

        public string number { get; set; }

        public DateTime created { get; set; }
        public Boolean enabled { get; set; }
        public Boolean mfa { get; set; }
        public string secret { get; set; }
        public Role role { get; set; }
        public string adress { get; set; }
        public int hours { set; get; }

        public User()
        {

        }
        public User(long userId, string username, string password, string name, string lastname, string email, string number, DateTime created, bool enabled, bool mfa, string secret, Role role, string adress, int hours)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.number = number;
            this.created = created;
            this.enabled = enabled;
            this.mfa = mfa;
            this.secret = secret;
            this.role = role;
            this.adress = adress;
            this.hours = hours;
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
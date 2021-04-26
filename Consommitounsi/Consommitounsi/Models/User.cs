using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consommitounsi.Models
{
    public class User
    {
        private long userId { get; set; }

        private string username { get; set; }

        private string password { get; set; }

        private string name { get; set; }

        private string lastname { get; set; }



        private string email { get; set; }

        private string number { get; set; }

        private DateTime created { get; set; }
        private Boolean enabled { get; set; }
        private Boolean mfa { get; set; }
        private string secret { get; set; }
        private Role role { get; set; }
        private string adress { get; set; }
        private int hours { set; get; }



    }
}
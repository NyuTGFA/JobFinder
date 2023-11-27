using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class User
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string introduction { get; set; }
     
        public string password { get; set; }
    }
}

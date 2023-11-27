using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class UserVm
    {
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string introduction { get; set; }
        
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public int? resetCode { get; set; } = null;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class Job
    {
        public string? _id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string companyName { get; set; }
        public DateTime createdAt { get; set; }
        public string contractType { get; set; }
        public string schedule { get; set; }
        public string location { get; set; }
        public string salary { get; set; }
    }
}

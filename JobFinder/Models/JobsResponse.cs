using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class JobsResponse
    {
        public Job job { get; set; }
        public bool alreadyApplied { get; set; }
    }
}

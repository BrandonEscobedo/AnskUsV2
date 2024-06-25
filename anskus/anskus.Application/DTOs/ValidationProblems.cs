using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DTOs
{
    public class ValidationProblems
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public Dictionary<string, string[]> errors { get; set; }
    }
}

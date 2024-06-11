using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Models.Authentication
{
    public class RefreshToken
    {
        public int? id { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
    }
}

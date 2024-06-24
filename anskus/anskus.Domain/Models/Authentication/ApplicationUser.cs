using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Models.Authentication
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        [InverseProperty("IdUsuarioNavigation")]

        public virtual CuestionarioActivo? CuestionarioActivo { get; set; }
    }
}

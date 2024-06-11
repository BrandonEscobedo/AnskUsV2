using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Models.Authentication
{
    public record GeneralResponse(bool Flag = false, string Message = null!);

}

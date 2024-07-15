using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Account
{
    public interface IAuthService
    {
        Task<string> GetAccessTokenAsync();
    }
}

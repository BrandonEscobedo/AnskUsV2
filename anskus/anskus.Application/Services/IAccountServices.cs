using anskus.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Services
{
    public interface IAccountServices
    {
        Task<GeneralResponse> CreateAccountAsync(CreateAccountDTO model);
        Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);
        Task<LoginResponse> LoginAccountAsync(LoginDTO model);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anskus.Application.Extensions;
namespace anskus.Application.Account
{
    public class AuthService: IAuthService
    {
        private readonly LocalStorageServices _localStorageService;

        public AuthService(LocalStorageServices localStorageService)
        {
            _localStorageService = localStorageService;
        }
      public  async Task<string> GetAccessTokenAsync()
        {

            var token = await _localStorageService.GetModelFromToken();
            return token?.Token ?? string.Empty;
        }
    }
}

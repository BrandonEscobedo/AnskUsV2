using anskus.Application.Extensions;
using anskus.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Services
{
    public class AccountServices(HttpClientServices httpClientServices) : IAccountServices
    {
        public async Task<GeneralResponse> CreateAccountAsync(CreateAccountDTO model)
        {
            try
            {
                var publicClient = httpClientServices.GetPublicClient();
                var response = await publicClient.PostAsJsonAsync(Constant.RegisterRoute, model);
                string error = CheckResponseStatus(response);
                if (!string.IsNullOrEmpty(error))
                    return new GeneralResponse(Flag: false, error);
                var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
                return result!;
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }

        public async Task<LoginResponse> LoginAccountAsync(LoginDTO model)
        {
            try
            {
                var publicClient = httpClientServices.GetPublicClient();
                var response = await publicClient.PostAsJsonAsync(Constant.LoginRoute, model);
                string error = CheckResponseStatus(response);
                if (!string.IsNullOrEmpty(error))
                    return new LoginResponse(Flag: false, error);
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result!;
            }
            catch (Exception ex)
            {
                return new LoginResponse(false, ex.Message);
            }
        }
        private static string CheckResponseStatus(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return $"ha ocurrido un error de tipo :{response.ReasonPhrase}";
            }
            else
            {
                return null!;
            }
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model)
        {
            try
            {
                var publicClient = httpClientServices.GetPublicClient();
                var response = await publicClient.PostAsJsonAsync(Constant.RefreshTokenRoute, model);
                string error = CheckResponseStatus(response);
                if (!string.IsNullOrEmpty(error))
                    return new LoginResponse(Flag: false, error);
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result!;
            }
            catch (Exception ex)
            {

                return new LoginResponse(false, ex.Message);
            }

        }
    }

}

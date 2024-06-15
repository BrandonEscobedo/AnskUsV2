using anskus.Application.DTOs.Request.Account;
using anskus.Application.Extensions;
using anskus.Application.Services;
using anskus.Domain.Models.Authentication;
using Microsoft.AspNetCore.Components;
using NetcodeHub.Packages.Extensions.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace anskus.Application.Extensions
{
    public class CustomHttpHandler(LocalStorageServices localStorageServices,
       NavigationManager navigationManager, IAccountServices accountServices) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            try
            {
                bool loginUrl = requestMessage.RequestUri!.AbsoluteUri.Contains(Constant.LoginRoute);
                bool RegisterUrl = requestMessage.RequestUri!.AbsoluteUri.Contains(Constant.RegisterRoute);
                bool RefreshToken = requestMessage.RequestUri!.AbsoluteUri.Contains(Constant.RefreshTokenRoute);
                if (loginUrl || RegisterUrl || RefreshToken)
                    return await base.SendAsync(requestMessage, cancellationToken);
                var result = await base.SendAsync(requestMessage, cancellationToken);
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var token = await localStorageServices.GetModelFromToken();
                    if (token != null) return result;
                    var newJwToken = await GetRefreshToken(token.Refresh!);
                    if (string.IsNullOrEmpty(newJwToken)) return result;
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(Constant.HttpClientHadersSchame, newJwToken);
                    return await base.SendAsync(requestMessage, cancellationToken);

                }
                return result;
            }
            catch { return null!; }

        }
        public async Task<string> GetRefreshToken(string refreshToken)
        {
            try
            {
                var response = await accountServices.RefreshTokenAsync(new RefreshTokenDTO() { Token = refreshToken });
                if (response == null || response.token == null)
                {
                    await ClearBrowserStorage();
                    NavigateToLogin();
                    return null!;
                }
                await localStorageServices.RemoveTokenFromBrowserLocalStorage();
                await localStorageServices.SetBrowserLocalStorage(new LocalStorageDTO() { Refresh = response!.RefreshToken, Token = response.token });
                return response.token;

            }
            catch { return null!; }
        }
        private void NavigateToLogin() => navigationManager.NavigateTo(navigationManager.BaseUri, true, true);
        private async Task ClearBrowserStorage() => await localStorageServices.RemoveTokenFromBrowserLocalStorage();
    }
}

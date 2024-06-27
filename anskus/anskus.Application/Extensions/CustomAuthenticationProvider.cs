using anskus.Application.DTOs.Request.Account;
using anskus.Application.DTOs.Response.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
namespace anskus.Application.Extensions
{
    public class CustomAuthenticationProvider(LocalStorageServices localStorageServices) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal claims = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenModel = await localStorageServices.GetModelFromToken();
            if (string.IsNullOrEmpty(tokenModel.Token))
                return await Task.FromResult(new AuthenticationState(claims));
            var handler= new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenModel.Token);
            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                await UpdateAthenticationState(new LocalStorageDTO());
                return new AuthenticationState(claims);
            }
            var getUserClaims = DecryptToken(tokenModel.Token!);
            if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(claims));
            var claimsPrincipal = SetClaims(getUserClaims);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));

        }
        public async Task UpdateAthenticationState(LocalStorageDTO localStorageDTO)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (localStorageDTO.Token != null | localStorageDTO.Refresh != null)
            {
                await localStorageServices.SetBrowserLocalStorage(localStorageDTO);
                var getUserClaims = DecryptToken(localStorageDTO.Token!);
                claimsPrincipal = SetClaims(getUserClaims);
            }
            else
            {
                await localStorageServices.RemoveTokenFromBrowserLocalStorage();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        public static ClaimsPrincipal SetClaims(UserClaimsDTO userClaims)
        {
            if (userClaims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Email, userClaims.Email),
                new(ClaimTypes.Name, userClaims.Email)
            ], Constant.AuthenticationType));
        }
        private static UserClaimsDTO DecryptToken(string jwtoken)
        {
            try
            {
                if (string.IsNullOrEmpty(jwtoken))
                {
                    return new UserClaimsDTO();
                }
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtoken);
                var name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;
                var email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
                return new UserClaimsDTO(email);

            }
            catch (Exception ex) { return null!; }
        }
    }
}

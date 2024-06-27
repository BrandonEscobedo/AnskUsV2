using anskus.Application.DTOs;
using anskus.Application.DTOs.Request.Account;
using anskus.Domain.Account;
using anskus.Domain.Models.Authentication;
using anskus.Infrestructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Repositorys
{
    public class AccountRepository(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        SignInManager<ApplicationUser> signInManager, AnskusDbContext context) : IAccountRepository
    {

        private async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
         return   await userManager.FindByEmailAsync(email);
        }

        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private string CheckResponse(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return string.Join(Environment.NewLine, errors);
            }
            return null!;
        }
        private async Task<string> GenerateToken(ApplicationUser user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]!));
                var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var userClaims = new[]
                {
                    new Claim(ClaimTypes.Name,user.Email!),
                    new Claim(ClaimTypes.Email,user.Email!),
                    new Claim(ClaimTypes.NameIdentifier,user.Email),

                };
                var expiry = DateTime.UtcNow.AddDays(2);
                var token = new JwtSecurityToken
                    (
                    issuer: configuration["JwtIssuer"],
                    audience: configuration["JwtAudience"],
                    claims: userClaims,
                    expires: expiry,
                    signingCredentials: creds
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch { return null!; }
        }
        public async Task<bool> isEmailUniqueAsync(string email)
        {
          return !await context.Users.AnyAsync(x=>x.Email == email);
        }
        public async Task<GeneralResponse> CreateAccountAsync(CreateAccountDTO model)
        {
            try
            {
                var user = new ApplicationUser()
                {
                  
                    Email = model.Email,
                    UserName = model.Email,
                    PasswordHash = model.Password
                };

                var result = await userManager.CreateAsync(user, model.Password);
                string error = CheckResponse(result);
                if (!string.IsNullOrEmpty(error))
                {
                    return new GeneralResponse(false, error);
                }
                return new GeneralResponse(true, "Se ha creado  la cuenta");
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
                var user = await FindUserByEmailAsync(model.Email!);
                if (user is null)
                    return new LoginResponse(false, "Este usuario no existe");
                SignInResult result;
                try
                {
                    result = await signInManager.CheckPasswordSignInAsync(user, model.Password!, false);

                }
                catch
                {
                    return new LoginResponse(false, "Credenciales Invalidas");

                }
                if (!result.Succeeded)
                {
                    return new LoginResponse(false, "Credenciales Invalidas");
                }
                string jwtoken = await GenerateToken(user);
                string refreshtoken = GenerateRefreshToken();
                if (string.IsNullOrEmpty(jwtoken) || string.IsNullOrEmpty(refreshtoken))
                {
                    return new LoginResponse(false, "ocurrio un error al iniciar sesion");
                }
                else
                {
                    var saveResult = await SaveRefreshToken(user.Id, refreshtoken);
                    if (saveResult.Flag)
                        return new LoginResponse(true, $"{user.Email} Se ha logeado correctamente", jwtoken, refreshtoken);

                    else
                        return new LoginResponse();
                }



            }
            catch (Exception ex)
            {
                return new LoginResponse(false, ex.Message);

            }
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model)
        {
            var token = await context.RefreshToken.FirstOrDefaultAsync(x => x.Token == model.Token);
            if (token == null)
                return new LoginResponse();
            var user = await userManager.FindByEmailAsync(token.UserId.ToString()!);
            string newtoken = await GenerateToken(user);
            string newRefreshToken = GenerateRefreshToken();
            var saveResult = await SaveRefreshToken(user.Id, newRefreshToken);
            if (saveResult.Flag)
            {
                return new LoginResponse(true, $"{user.Email} se ha logeado correctamente", newtoken, newRefreshToken);

            }
            else
                return new LoginResponse();
        }
        public async Task<GeneralResponse> SaveRefreshToken(Guid userId, string token)
        {
            try
            {
                var user = await context.RefreshToken.FirstOrDefaultAsync(t => t.UserId == userId);
                if (user == null)
                {
                    context.RefreshToken.Add(new RefreshToken() { UserId = userId, Token = token });

                }
                else
                    user.Token = token;
                await context.SaveChangesAsync();
                return new GeneralResponse(true, null!);
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }
    }
}

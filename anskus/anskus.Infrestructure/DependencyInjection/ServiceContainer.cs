using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using anskus.Infrestructure.Persistence;
using Microsoft.Extensions.Options;
using anskus.Domain.Cuestionarios;
using anskus.Infrestructure.Repositorys;
using anskus.Infrestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using anskus.Domain.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using anskus.Domain.Account;
using anskus.Infrestructure.Factory;
using anskus.Application.Services;
using anskus.Infrestructure.Services;
using Azure.Storage.Blobs;

namespace anskus.Infrestructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrestructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDb"));
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return new MongoClient(settings.ConnectionString);

            });
            services.AddScoped(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                var clients = sp.GetRequiredService<IMongoClient>();
                return clients.GetDatabase(settings.DatabaseName);
            });
            services.AddDbContext<AnskusDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<AnskusDbContext>()
                .AddSignInManager()
                .Services.AddAuthentication(sp =>
                {
                    sp.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    sp.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = configuration["JwtIssuer"],
                                    ValidAudience = configuration["JwtAudience"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]!))
                                };
                            });
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));
            services.AddScoped<ICuestionarioRepository, CuestionarioRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICuestionarioActivoRepository, CuestionarioActivoRepository>();
            services.AddScoped<IRandomCodeFactory, RandomCodeFactory>();
            services.AddScoped<ICodeValidator, RandomCodeFactory>();
            services.AddScoped<ICuestionarioActivoService, CuestionarioActivoService>();
            return services;

        }


    }
}

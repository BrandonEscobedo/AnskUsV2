﻿using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NetcodeHub.Packages.Extensions.LocalStorage;
using anskus.Application.Extensions;
using anskus.Application.Services;
namespace anskus.Application.DependencyInjection
{
    public static class ServiceContainer
    {
      public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ServiceContainer).Assembly);
            });
            services.AddValidatorsFromAssembly(AppicationAssemblyReference.assembly);
            return services;
        }
        public static IServiceCollection ApplicationClientService(this IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddNetcodeHubLocalStorageService();
            services.AddAuthorizationCore();
            services.AddScoped<ICuestionarioService, CuestionarioService>();
            services.AddScoped<HttpClientServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
            services.AddScoped<LocalStorageServices>();
            services.AddTransient<CustomHttpHandler>();
            services.AddCascadingAuthenticationState();
            services.AddHttpClient("TestAnskusClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7229/");
            }).AddHttpMessageHandler<CustomHttpHandler>();
            return services;
        }

    }
}
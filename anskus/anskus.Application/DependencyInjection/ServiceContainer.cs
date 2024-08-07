﻿using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NetcodeHub.Packages.Extensions.LocalStorage;
using anskus.Application.Extensions;
using anskus.Application.Services;
using anskus.Application.Utility;
using anskus.Application.Codigo;
using anskus.Domain.Cuestionarios;
using anskus.Application.HubServices.StateContainers;
using anskus.Application.HubServices;
using anskus.Application.HubServices.StateContainers.Creador;
using anskus.Application.HubServices.StateContainers.Jugador;
using anskus.Application.Account;
namespace anskus.Application.DependencyInjection
{
    public static class ServiceContainer
    {
      public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ServiceContainer).Assembly);
            });
            services.AddValidatorsFromAssembly(AppicationAssemblyReference.assembly);
            services.AddScoped<IVerificarCodigo, VerificarCodigo>();
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
            services.AddScoped<IStateParticipantes, StateParticipantes>();
            services.AddScoped<IHubStateCreador, HubStateCreador>();
            services.AddScoped<IHubJugadorServices, HubJugadorServices>();
            services.AddScoped<IStateJugador, StateJugador>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStateContainerOnPreg, StateContainerOnPreg>();
            services.AddScoped<IHubCreadorServices, HubCreadorServices>();
            services.AddScoped<IStateCreador, StateCreador>();
            services.AddScoped<IContentMediaServices, ContentMediaServices>();
            services.AddScoped<IHubconnectionService, HubconnectionService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
            services.AddScoped<LocalStorageServices>();
            services.AddTransient<CustomHttpHandler>();
            services.AddScoped<IHubConnectionManager, HubConnectionManager>();  
            services.AddScoped<ICuestionarioActivoServices, CuestionarioActivoServices>();
            services.AddCascadingAuthenticationState();
            services.AddHttpClient("TestAnskusClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7229/");
            }).AddHttpMessageHandler<CustomHttpHandler>();

            return services;
        }

    }
}

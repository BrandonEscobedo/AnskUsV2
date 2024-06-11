using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

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

    }
}

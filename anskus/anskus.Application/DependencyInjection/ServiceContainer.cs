using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return services;
        }

    }
}

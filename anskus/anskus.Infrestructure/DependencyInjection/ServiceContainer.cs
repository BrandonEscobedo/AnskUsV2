using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using anskus.Infrestructure.Persistence;
using Microsoft.Extensions.Options;
using anskus.Domain.Cuestionarios;
using anskus.Infrestructure.Repositorys;

namespace anskus.Infrestructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrestructureService(this IServiceCollection services,IConfiguration configuration) 
        {
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDb"));
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings=sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return new MongoClient(settings.ConnectionString);

            });
            services.AddScoped(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                var clients = sp.GetRequiredService<IMongoClient>();
                return clients.GetDatabase(settings.DatabaseName);
            });

            services.AddScoped<ICuestionarioRepository, CuestionarioRepository>();
            return services;

        }

    }
}

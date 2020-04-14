using Microsoft.Extensions.DependencyInjection;
using Syngenta.Sintegra.Domain;
using Syngenta.Sintegra.Repository.Repository;

namespace Syngenta.Sintegra.Bootstrapper
{
    public static class RepositoryInitializer
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {

            //services.AddScoped<IRepositorioPG, Repositorio.Base.RepositorioPG>();
            //services.AddScoped<IConexaoPG, PostgreSQLConexao>();

            services.AddTransient<IRequestRepository, RequestRepository>();

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Syngenta.RPA.Sintegra.Bootstrapper
{
    public static class RepositoryInitializer
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {

            //services.AddScoped<IRepositorioPG, Repositorio.Base.RepositorioPG>();
            //services.AddScoped<IConexaoPG, PostgreSQLConexao>();

            //services.AddTransient<IRepositorioDeAbcfarma, RepositorioDeAbcfarma>();

            return services;
        }
    }
}

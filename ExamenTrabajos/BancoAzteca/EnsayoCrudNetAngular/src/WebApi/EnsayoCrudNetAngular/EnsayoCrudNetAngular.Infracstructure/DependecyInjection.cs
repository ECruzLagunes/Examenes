using EnsayoCrudNetAngular.Infrastructure.DataAccess;
using EnsayoCrudNetAngular.Infrastructure.Interface;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnsayoCrudNetAngular.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Registra el repositorio con la cadena de conexión
            services.AddScoped<IEmpleadoRepository>(provider => new EmpleadoRepository(connectionString));

            return services;
        }
    }
}
using EscuelaMusica.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using EscuelaMusica.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace EscuelaMusica.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection svcs, IConfiguration cfg)
    {
        svcs.AddScoped<IEscuelaRepository, EscuelaRepository>();
        svcs.AddScoped<IProfesorRepository, ProfesorRepository>();
        svcs.AddScoped<IAlumnoRepository, AlumnoRepository>();
        svcs.AddScoped<IAsignacionRepository, AsignacionRepository>();

        return svcs;
    }
}
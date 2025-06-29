using EscuelaMusica.Application.Interface;
using EscuelaMusica.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EscuelaMusica.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection svcs)
        {
            svcs.AddScoped<IEscuelaService, EscuelaService>();
            svcs.AddScoped<IProfesorService, ProfesorService>();
            svcs.AddScoped<IAlumnoService, AlumnoService>();
            svcs.AddScoped<IAsignacionService, AsignacionService>();

            return svcs;
        }
    }
}

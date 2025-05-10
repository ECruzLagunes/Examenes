using EnsayoCrudNetAngular.Domain.Entities;

namespace EnsayoCrudNetAngular.Application.Interface
{
    public interface IEmpleadoService
    {
        public Task<(Empleado? empleado, int codigo, string mensaje)> ObtenerEmpleadoPorIdAsync(int idEmpleado);
        public Task<(List<Empleado> lista, int resultado, string mensaje)> ObtenerListaEmpleadosAsync();
    }
}

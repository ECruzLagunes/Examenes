using EnsayoCrudNetAngular.Domain.Entities;

namespace EnsayoCrudNetAngular.Infrastructure.Interface
{
    public interface IEmpleadoRepository
    {
        Task<List<Empleado>> ObtenerListaEmpleadosAsync();

        Task<Empleado?> ObtenerEmpleadoPorIdAsync(int idEmpleado);
    }
}

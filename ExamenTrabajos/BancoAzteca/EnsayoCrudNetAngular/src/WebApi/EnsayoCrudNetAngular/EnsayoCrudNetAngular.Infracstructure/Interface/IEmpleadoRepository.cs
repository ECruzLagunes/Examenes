using EnsayoCrudNetAngular.Domain.Entities;

namespace EnsayoCrudNetAngular.Infrastructure.Interface
{
    public interface IEmpleadoRepository
    {
        Task<List<Empleado>> ObtenerListaEmpleadosAsync();

        Task<Empleado?> ObtenerEmpleadoPorIdAsync(int idEmpleado);

        Task<(int idGenerado, string mensaje)> InsertarEmpleadoAsync(Empleado empleado);
        
        Task<(int codigo, string mensaje)> EditarEmpleadoAsync(Empleado empleado);

        Task<(int idEliminado, int resultado, string mensaje)> EliminarEmpleadoAsync(int idEmpleado);
    }
}

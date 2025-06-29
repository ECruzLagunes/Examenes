using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Interface
{
    public interface IAlumnoService
    {
        Task<OperationResult> CrearAsync(Alumno a, CancellationToken ct);
        Task<OperationResult> EditarAsync(Alumno a, CancellationToken ct);
        Task<OperationResult> EliminarAsync(int id, CancellationToken ct);
        Task<IReadOnlyList<Alumno>> ListarAsync(CancellationToken ct);
        Task<Alumno?> ObtenerAsync(int id, CancellationToken ct);
    }
}

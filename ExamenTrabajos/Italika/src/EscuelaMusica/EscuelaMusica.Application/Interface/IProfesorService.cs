using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Interface
{
    public interface IProfesorService
    {
        Task<OperationResult> CrearAsync(Profesor p, CancellationToken ct);
        Task<OperationResult> EditarAsync(Profesor p, CancellationToken ct);
        Task<OperationResult> EliminarAsync(int id, CancellationToken ct);
        Task<IReadOnlyList<Profesor>> ListarAsync(CancellationToken ct);
        Task<Profesor?> ObtenerAsync(int id, CancellationToken ct);
    }
}

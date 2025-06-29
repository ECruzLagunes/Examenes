using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Domain.Contracts;
public interface IProfesorRepository
{
    Task<OperationResult> InsertarAsync(Profesor p, CancellationToken ct);
    Task<OperationResult> ActualizarAsync(Profesor p, CancellationToken ct);
    Task<OperationResult> EliminarAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Profesor>> ObtenerTodasAsync(CancellationToken ct);
    Task<Profesor?> ObtenerPorIdAsync(int id, CancellationToken ct);
}

using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Domain.Contracts;

public interface IEscuelaRepository
{
    Task<OperationResult> InsertarAsync(Escuela e, CancellationToken ct);
    Task<OperationResult> ActualizarAsync(Escuela e, CancellationToken ct);
    Task<OperationResult> EliminarAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Escuela>> ObtenerTodasAsync(CancellationToken ct);
    Task<Escuela?> ObtenerPorIdAsync(int id, CancellationToken ct);
}
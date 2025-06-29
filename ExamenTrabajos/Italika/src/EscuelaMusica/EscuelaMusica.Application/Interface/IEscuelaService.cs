using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Interface
{
    public interface IEscuelaService
    {
        Task<OperationResult> CrearAsync(Escuela e, CancellationToken ct);
        Task<OperationResult> EditarAsync(Escuela e, CancellationToken ct);
        Task<OperationResult> EliminarAsync(int id, CancellationToken ct);
        Task<IReadOnlyList<Escuela>> ListarAsync(CancellationToken ct);
        Task<Escuela?> ObtenerAsync(int id, CancellationToken ct);
    }
}

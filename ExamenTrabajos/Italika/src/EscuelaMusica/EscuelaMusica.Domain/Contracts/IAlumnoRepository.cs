using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Domain.Contracts;
public interface IAlumnoRepository
{
    Task<OperationResult> InsertarAsync(Alumno a, CancellationToken ct);
    Task<OperationResult> ActualizarAsync(Alumno a, CancellationToken ct);
    Task<OperationResult> EliminarAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Alumno>> ObtenerTodasAsync(CancellationToken ct);
    Task<Alumno?> ObtenerPorIdAsync(int id, CancellationToken ct);
}
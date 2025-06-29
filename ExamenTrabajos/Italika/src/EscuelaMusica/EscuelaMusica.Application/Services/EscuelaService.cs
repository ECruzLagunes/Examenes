using EscuelaMusica.Application.Interface;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Services;

public sealed class EscuelaService : IEscuelaService
{
    private readonly IEscuelaRepository _r;
    public EscuelaService(IEscuelaRepository r) => _r = r;
    public Task<OperationResult> CrearAsync(Escuela e, CancellationToken ct) => _r.InsertarAsync(e, ct);
    public Task<OperationResult> EditarAsync(Escuela e, CancellationToken ct) => _r.ActualizarAsync(e, ct);
    public Task<OperationResult> EliminarAsync(int id, CancellationToken ct) => _r.EliminarAsync(id, ct);
    public Task<IReadOnlyList<Escuela>> ListarAsync(CancellationToken ct) => _r.ObtenerTodasAsync(ct);
    public Task<Escuela?> ObtenerAsync(int id, CancellationToken ct) => _r.ObtenerPorIdAsync(id, ct);
}
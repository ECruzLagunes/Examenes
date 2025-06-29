using EscuelaMusica.Application.Interface;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Services
{
    public sealed class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _r;
        public ProfesorService(IProfesorRepository r) => _r = r;
        public Task<OperationResult> CrearAsync(Profesor p, CancellationToken ct) => _r.InsertarAsync(p, ct);
        public Task<OperationResult> EditarAsync(Profesor p, CancellationToken ct) => _r.ActualizarAsync(p, ct);
        public Task<OperationResult> EliminarAsync(int id, CancellationToken ct) => _r.EliminarAsync(id, ct);
        public Task<IReadOnlyList<Profesor>> ListarAsync(CancellationToken ct) => _r.ObtenerTodasAsync(ct);
        public Task<Profesor?> ObtenerAsync(int id, CancellationToken ct) => _r.ObtenerPorIdAsync(id, ct);
    }
}

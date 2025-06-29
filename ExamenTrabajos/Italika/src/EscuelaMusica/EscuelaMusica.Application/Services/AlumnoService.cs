using EscuelaMusica.Application.Interface;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Services
{
    public sealed class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _r;
        public AlumnoService(IAlumnoRepository r) => _r = r;
        public Task<OperationResult> CrearAsync(Alumno a, CancellationToken ct) => _r.InsertarAsync(a, ct);
        public Task<OperationResult> EditarAsync(Alumno a, CancellationToken ct) => _r.ActualizarAsync(a, ct);
        public Task<OperationResult> EliminarAsync(int id, CancellationToken ct) => _r.EliminarAsync(id, ct);
        public Task<IReadOnlyList<Alumno>> ListarAsync(CancellationToken ct) => _r.ObtenerTodasAsync(ct);
        public Task<Alumno?> ObtenerAsync(int id, CancellationToken ct) => _r.ObtenerPorIdAsync(id, ct);
    }
}

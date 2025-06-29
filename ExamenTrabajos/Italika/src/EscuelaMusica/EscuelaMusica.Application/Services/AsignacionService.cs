using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;

public sealed class AsignacionService : IAsignacionService
{
    private readonly IAsignacionRepository _repo;
    public AsignacionService(IAsignacionRepository repo) => _repo = repo;

    public Task<OperationResult> AsignarAsync(int a, int p, CancellationToken ct) => _repo.AsignarAsync(a, p, ct);
    public Task<OperationResult> DesasignarAsync(int a, int p, CancellationToken ct) => _repo.DesasignarAsync(a, p, ct);
    public Task<IReadOnlyList<Alumno>> AlumnosPorProfesorAsync(int p, CancellationToken ct) => _repo.AlumnosPorProfesorAsync(p, ct);
    public Task<IReadOnlyList<Profesor>> ProfesoresPorAlumnoAsync(int a, CancellationToken ct) => _repo.ProfesoresPorAlumnoAsync(a, ct);
}
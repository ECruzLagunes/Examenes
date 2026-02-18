using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;
using Microsoft.Extensions.Logging;

public sealed class AsignacionService : IAsignacionService
{
    private readonly IAsignacionRepository _repo;
    private readonly Microsoft.Extensions.Logging.ILogger<AsignacionService> _logger;

    public AsignacionService(IAsignacionRepository repo, Microsoft.Extensions.Logging.ILogger<AsignacionService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<OperationResult> AsignarAsync(int a, int p, CancellationToken ct)
    {
        _logger.LogInformation("Asignar alumno {AlumnoId} to profesor {ProfesorId}", a, p);
        try
        {
            var res = await _repo.AsignarAsync(a, p, ct);
            _logger.LogInformation("Asignar result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning alumno {AlumnoId} to profesor {ProfesorId}", a, p);
            throw;
        }
    }

    public async Task<OperationResult> DesasignarAsync(int a, int p, CancellationToken ct)
    {
        _logger.LogInformation("Desasignar alumno {AlumnoId} from profesor {ProfesorId}", a, p);
        try
        {
            var res = await _repo.DesasignarAsync(a, p, ct);
            _logger.LogInformation("Desasignar result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unassigning alumno {AlumnoId} from profesor {ProfesorId}", a, p);
            throw;
        }
    }

    public async Task<IReadOnlyList<Alumno>> AlumnosPorProfesorAsync(int p, CancellationToken ct)
    {
        _logger.LogInformation("Getting alumnos for profesor {ProfesorId}", p);
        try
        {
            var list = await _repo.AlumnosPorProfesorAsync(p, ct);
            _logger.LogInformation("Found {Count} alumnos for profesor {ProfesorId}", list?.Count ?? 0, p);
            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting alumnos for profesor {ProfesorId}", p);
            throw;
        }
    }

    public async Task<IReadOnlyList<Profesor>> ProfesoresPorAlumnoAsync(int a, CancellationToken ct)
    {
        _logger.LogInformation("Getting profesores for alumno {AlumnoId}", a);
        try
        {
            var list = await _repo.ProfesoresPorAlumnoAsync(a, ct);
            _logger.LogInformation("Found {Count} profesores for alumno {AlumnoId}", list?.Count ?? 0, a);
            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting profesores for alumno {AlumnoId}", a);
            throw;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Api.Models;
using ApiResponse = EscuelaMusica.Api.Utils.ApiResponse;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public sealed class AsignacionesController : ControllerBase
{
    private readonly IAsignacionService _svc;
    private readonly ILogger<AsignacionesController> _logger;
    public AsignacionesController(IAsignacionService svc, ILogger<AsignacionesController> logger)
    {
        _svc = svc;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Asignar([FromBody] AsignDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Asignar called: Alumno {AlumnoId} -> Profesor {ProfesorId}", dto.IdAlumno, dto.IdProfesor);
        try
        {
            var r = await _svc.AsignarAsync(dto.IdAlumno, dto.IdProfesor, ct);
            _logger.LogInformation("Asignar result Codigo={Codigo} Id={Id}", r.Codigo, r.Id);
            return ApiResponse.FromResult(r);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Asignar Alumno {AlumnoId} Profesor {ProfesorId}", dto.IdAlumno, dto.IdProfesor);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Desasignar([FromBody] AsignDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Desasignar called: Alumno {AlumnoId} -> Profesor {ProfesorId}", dto.IdAlumno, dto.IdProfesor);
        try
        {
            var r = await _svc.DesasignarAsync(dto.IdAlumno, dto.IdProfesor, ct);
            _logger.LogInformation("Desasignar result Codigo={Codigo} Id={Id}", r.Codigo, r.Id);
            return ApiResponse.FromResult(r);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Desasignar Alumno {AlumnoId} Profesor {ProfesorId}", dto.IdAlumno, dto.IdProfesor);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpGet("profesor/{id:int}")]
    public Task<IReadOnlyList<Alumno>> AlumnosDeProfesor(int id, CancellationToken ct)
    {
        _logger.LogInformation("AlumnosDeProfesor called for Profesor {Id}", id);
        return _svc.AlumnosPorProfesorAsync(id, ct);
    }

    [HttpGet("alumno/{id:int}")]
    public Task<IReadOnlyList<Profesor>> ProfesoresDeAlumno(int id, CancellationToken ct)
    {
        _logger.LogInformation("ProfesoresDeAlumno called for Alumno {Id}", id);
        return _svc.ProfesoresPorAlumnoAsync(id, ct);
    }
}

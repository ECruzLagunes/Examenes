using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EscuelaMusica.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class AsignacionesController : ControllerBase
{
    private readonly IAsignacionService _svc;
    public AsignacionesController(IAsignacionService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> Asignar([FromBody] AsignDto dto, CancellationToken ct)
    {
        var r = await _svc.AsignarAsync(dto.IdAlumno, dto.IdProfesor, ct);
        return r.Codigo == 0 ? Ok(r) : BadRequest(r);
    }

    [HttpDelete]
    public async Task<IActionResult> Desasignar([FromBody] AsignDto dto, CancellationToken ct)
    {
        var r = await _svc.DesasignarAsync(dto.IdAlumno, dto.IdProfesor, ct);
        return r.Codigo == 0 ? NoContent() : NotFound(r);
    }

    [HttpGet("profesor/{id:int}")]
    public Task<IReadOnlyList<Alumno>> AlumnosDeProfesor(int id, CancellationToken ct)
        => _svc.AlumnosPorProfesorAsync(id, ct);

    [HttpGet("alumno/{id:int}")]
    public Task<IReadOnlyList<Profesor>> ProfesoresDeAlumno(int id, CancellationToken ct)
        => _svc.ProfesoresPorAlumnoAsync(id, ct);
}

public record AsignDto(int IdAlumno, int IdProfesor);
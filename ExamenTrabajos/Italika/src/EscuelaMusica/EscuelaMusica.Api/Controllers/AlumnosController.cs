using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Application.Interface;

[ApiController]
[Route("api/[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly IAlumnoService _svc;
    public AlumnosController(IAlumnoService svc) => _svc = svc;

    [HttpGet] public Task<IReadOnlyList<Alumno>> Get(CancellationToken ct) => _svc.ListarAsync(ct);
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Alumno>> Get(int id, CancellationToken ct)
    { 
        var a = await _svc.ObtenerAsync(id, ct); 
        return a is null ? NotFound() : Ok(a); 
    }

    [HttpPost]
    public async Task<ActionResult> Post(Alumno dto, CancellationToken ct)
    { 
        var r = await _svc.CrearAsync(dto, ct); 
        return r.Codigo == 0 ? CreatedAtAction(nameof(Get), new { id = r.Id }, dto) : BadRequest(r); 
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Alumno dto, CancellationToken ct)
    { 
        if (id != dto.IdAlumno) 
            return BadRequest(); 
        
        var r = await _svc.EditarAsync(dto, ct); 
        return r.Codigo == 0 ? NoContent() : NotFound(r); 
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    { 
        var r = await _svc.EliminarAsync(id, ct); 
        return r.Codigo == 0 ? NoContent() : NotFound(r); 
    }
}
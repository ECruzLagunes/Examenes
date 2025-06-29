using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Application.Interface;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _svc;
    public ProfesoresController(IProfesorService svc) => _svc = svc;

    [HttpGet] public Task<IReadOnlyList<Profesor>> Get(CancellationToken ct) => _svc.ListarAsync(ct);
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Profesor>> Get(int id, CancellationToken ct)
    { 
        var p = await _svc.ObtenerAsync(id, ct); 
        return p is null ? NotFound() : Ok(p); 
    }

    [HttpPost]
    public async Task<ActionResult> Post(Profesor dto, CancellationToken ct)
    { 
        var r = await _svc.CrearAsync(dto, ct); 
        return r.Codigo == 0 ? CreatedAtAction(nameof(Get), new { id = r.Id }, dto) : BadRequest(r); 
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Profesor dto, CancellationToken ct)
    { 
        if (id != dto.IdProfesor) 
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
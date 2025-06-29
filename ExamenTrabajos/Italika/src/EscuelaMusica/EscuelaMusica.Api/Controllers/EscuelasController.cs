using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Application.Interface;

[ApiController]
[Route("api/[controller]")]
public class EscuelasController : ControllerBase
{
    private readonly IEscuelaService _svc;
    public EscuelasController(IEscuelaService svc) => _svc = svc;

    [HttpGet] public Task<IReadOnlyList<Escuela>> Get(CancellationToken ct) => _svc.ListarAsync(ct);
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Escuela>> Get(int id, CancellationToken ct)
    { 
        var e = await _svc.ObtenerAsync(id, ct); 
        return e is null ? NotFound() : Ok(e); 
    }

    [HttpPost]
    public async Task<ActionResult> Post(Escuela dto, CancellationToken ct)
    { 
        var r = await _svc.CrearAsync(dto, ct); return r.Codigo == 0 ? CreatedAtAction(nameof(Get), new { id = r.Id }, dto) : BadRequest(r); 
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Escuela dto, CancellationToken ct)
    { 
        if (id != dto.IdEscuela) 
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
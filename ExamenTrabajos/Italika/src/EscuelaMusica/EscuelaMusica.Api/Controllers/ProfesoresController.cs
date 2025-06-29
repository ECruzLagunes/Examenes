using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Domain.Entities;
using ApiResponse = EscuelaMusica.Api.Utils.ApiResponse;
using EscuelaMusica.Application.Interface;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _svc;
    public ProfesoresController(IProfesorService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        try { return ApiResponse.FromData(await _svc.ListarAsync(ct)); }
        catch (Exception ex) { return ApiResponse.FromException(ex); }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken ct)
    {
        try
        {
            var p = await _svc.ObtenerAsync(id, ct);
            return p is null ? ApiResponse.FromResult(new(1, "No encontrado", null))
                             : ApiResponse.FromData(p);
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Profesor dto, CancellationToken ct)
    {
        try 
        { 
            return ApiResponse.FromResult(await _svc.CrearAsync(dto, ct)); 
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Profesor dto, CancellationToken ct)
    {
        try
        {
            if (id != dto.IdProfesor) 
                return ApiResponse.FromResult(new(1, "Id distinto", null));

            return ApiResponse.FromResult(await _svc.EditarAsync(dto, ct));
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        try 
        { 
            return ApiResponse.FromResult(await _svc.EliminarAsync(id, ct)); 
        }
        catch (Exception ex)
        { 
            return ApiResponse.FromException(ex);
        }
    }
}

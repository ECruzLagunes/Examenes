using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Entities;
using ApiResponse = EscuelaMusica.Api.Utils.ApiResponse;
using EscuelaMusica.Application.Interface;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class AlumnosController : ControllerBase
{
    private readonly IAlumnoService _svc;
    public AlumnosController(IAlumnoService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        try 
        { 
            return ApiResponse.FromData(await _svc.ListarAsync(ct)); 
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken ct)
    {
        try
        {
            var a = await _svc.ObtenerAsync(id, ct);
            
            return a is null ? ApiResponse.FromResult(new(1, "No encontrado", null))
                             : ApiResponse.FromData(a);
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Alumno dto, CancellationToken ct)
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
    public async Task<IActionResult> Update(int id, Alumno dto, CancellationToken ct)
    {
        try
        {
            if (id != dto.IdAlumno) return ApiResponse.FromResult(new(1, "Id distinto", null));
            return ApiResponse.FromResult(await _svc.EditarAsync(dto, ct));
        }
        catch (Exception ex) { return ApiResponse.FromException(ex); }
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

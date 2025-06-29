using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Domain.Entities;
using ApiResponse = EscuelaMusica.Api.Utils.ApiResponse;
using EscuelaMusica.Application.Interface;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class EscuelasController : ControllerBase
{
    private readonly IEscuelaService _svc;
    public EscuelasController(IEscuelaService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        try
        {
            var list = await _svc.ListarAsync(ct);
            return ApiResponse.FromData(list);
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        try
        {
            var e = await _svc.ObtenerAsync(id, ct);
            if (e is null) return ApiResponse.FromResult(new(-1, "No encontrado", null));
            return ApiResponse.FromData(e);
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Escuela dto, CancellationToken ct)
    {
        try
        {
            var r = await _svc.CrearAsync(dto, ct);
            return ApiResponse.FromResult(r);
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Escuela dto, CancellationToken ct)
    {
        try
        {
            if (id != dto.IdEscuela) return ApiResponse.FromResult(new(1, "Id distinto", null));
            var r = await _svc.EditarAsync(dto, ct);
            return ApiResponse.FromResult(r);
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
            var r = await _svc.EliminarAsync(id, ct);
            return ApiResponse.FromResult(r);
        }
        catch (Exception ex) 
        { 
            return ApiResponse.FromException(ex); 
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Api.Models;
using ApiResponse = EscuelaMusica.Api.Utils.ApiResponse;
using EscuelaMusica.Application.Interface;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class EscuelasController : ControllerBase
{
    private readonly IEscuelaService _svc;
    private readonly ILogger<EscuelasController> _logger;
    public EscuelasController(IEscuelaService svc, ILogger<EscuelasController> logger)
    {
        _svc = svc;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("GetAll Escuelas called");
        try
        {
            var list = await _svc.ListarAsync(ct);
            _logger.LogInformation("GetAll Escuelas returned {Count} items", list?.Count ?? 0);
            return ApiResponse.FromData(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAll Escuelas");
            return ApiResponse.FromException(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        _logger.LogInformation("Get Escuela by Id {Id} called", id);
        try
        {
            var e = await _svc.ObtenerAsync(id, ct);
            if (e is null)
            {
                _logger.LogWarning("Escuela Id {Id} not found", id);
                return ApiResponse.FromResult(new(-1, "No encontrado", null));
            }

            _logger.LogInformation("Escuela Id {Id} retrieved", id);
            return ApiResponse.FromData(e);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Get Escuela by Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(EscuelaCreateDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Create Escuela called with Codigo {Codigo}", dto.CodigoEscuela);
        try
        {
            var entity = new Escuela
            {
                CodigoEscuela = dto.CodigoEscuela,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var r = await _svc.CrearAsync(entity, ct);
            _logger.LogInformation("Create Escuela result Codigo={Codigo} Id={Id}", r.Codigo, r.Id);
            return ApiResponse.FromResult(r);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Create Escuela with Codigo {Codigo}", dto.CodigoEscuela);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EscuelaUpdateDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Update Escuela called for Id {Id}", id);
        try
        {
            if (id != dto.IdEscuela)
            {
                _logger.LogWarning("Update Escuela Id mismatch: route {RouteId} != body {BodyId}", id, dto.IdEscuela);
                return ApiResponse.FromResult(new(1, "Id distinto", null));
            }

            var entity = new Escuela
            {
                IdEscuela = dto.IdEscuela,
                CodigoEscuela = dto.CodigoEscuela,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var r = await _svc.EditarAsync(entity, ct);
            _logger.LogInformation("Update Escuela result Codigo={Codigo} Id={Id}", r.Codigo, r.Id);
            return ApiResponse.FromResult(r);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Update Escuela Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        _logger.LogInformation("Delete Escuela called for Id {Id}", id);
        try
        {
            var r = await _svc.EliminarAsync(id, ct);
            _logger.LogInformation("Delete Escuela result Codigo={Codigo} Id={Id}", r.Codigo, r.Id);
            return ApiResponse.FromResult(r);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Delete Escuela Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }
}

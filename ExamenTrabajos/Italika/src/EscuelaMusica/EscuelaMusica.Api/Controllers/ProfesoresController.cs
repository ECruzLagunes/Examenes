using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Api.Models;
using ApiResponse = EscuelaMusica.Api.Utils.ApiResponse;
using EscuelaMusica.Application.Interface;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _svc;
    private readonly ILogger<ProfesoresController> _logger;
    public ProfesoresController(IProfesorService svc, ILogger<ProfesoresController> logger)
    {
        _svc = svc;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("GetAll Profesores called");
        try
        {
            var list = await _svc.ListarAsync(ct);
            _logger.LogInformation("GetAll Profesores returned {Count} items", list?.Count ?? 0);
            return ApiResponse.FromData(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAll Profesores");
            return ApiResponse.FromException(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken ct)
    {
        _logger.LogInformation("Get Profesor by Id {Id} called", id);
        try
        {
            var p = await _svc.ObtenerAsync(id, ct);
            if (p is null)
            {
                _logger.LogWarning("Profesor Id {Id} not found", id);
                return ApiResponse.FromResult(new(1, "No encontrado", null));
            }

            _logger.LogInformation("Profesor Id {Id} retrieved", id);
            return ApiResponse.FromData(p);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Get Profesor by Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProfesorCreateDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Create Profesor called with Codigo {Codigo}", dto.CodigoProfesor);
        try
        {
            var entity = new Profesor
            {
                CodigoProfesor = dto.CodigoProfesor,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                IdEscuela = dto.IdEscuela
            };

            var res = await _svc.CrearAsync(entity, ct);
            _logger.LogInformation("Create Profesor result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return ApiResponse.FromResult(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Create Profesor with Codigo {Codigo}", dto.CodigoProfesor);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProfesorUpdateDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Update Profesor called for Id {Id}", id);
        try
        {
            if (id != dto.IdProfesor)
            {
                _logger.LogWarning("Update Profesor Id mismatch: route {RouteId} != body {BodyId}", id, dto.IdProfesor);
                return ApiResponse.FromResult(new(1, "Id distinto", null));
            }

            var entity = new Profesor
            {
                IdProfesor = dto.IdProfesor,
                CodigoProfesor = dto.CodigoProfesor,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                IdEscuela = dto.IdEscuela
            };

            var res = await _svc.EditarAsync(entity, ct);
            _logger.LogInformation("Update Profesor result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return ApiResponse.FromResult(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Update Profesor Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        _logger.LogInformation("Delete Profesor called for Id {Id}", id);
        try
        {
            var res = await _svc.EliminarAsync(id, ct);
            _logger.LogInformation("Delete Profesor result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return ApiResponse.FromResult(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Delete Profesor Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }
}

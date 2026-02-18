using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Api.Models;
using ApiResponse = EscuelaMusica.Api.Utils.ApiResponse;
using EscuelaMusica.Application.Interface;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class AlumnosController : ControllerBase
{
    private readonly IAlumnoService _svc;
    private readonly ILogger<AlumnosController> _logger;
    public AlumnosController(IAlumnoService svc, ILogger<AlumnosController> logger)
    {
        _svc = svc;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("GetAll Alumnos called");
        try
        {
            var list = await _svc.ListarAsync(ct);
            _logger.LogInformation("GetAll returned {Count} items", list?.Count ?? 0);
            return ApiResponse.FromData(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAll Alumnos");
            return ApiResponse.FromException(ex);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken ct)
    {
        _logger.LogInformation("Get Alumno by Id {Id} called", id);
        try
        {
            var a = await _svc.ObtenerAsync(id, ct);

            if (a is null)
            {
                _logger.LogWarning("Alumno Id {Id} not found", id);
                return ApiResponse.FromResult(new(1, "No encontrado", null));
            }

            _logger.LogInformation("Alumno Id {Id} retrieved", id);
            return ApiResponse.FromData(a);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Get Alumno by Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(AlumnoCreateDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Create Alumno called with Codigo {Codigo}", dto.CodigoAlumno);
        try
        {
            var entity = new Alumno
            {
                CodigoAlumno = dto.CodigoAlumno,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                IdEscuela = dto.IdEscuela
            };

            var res = await _svc.CrearAsync(entity, ct);
            _logger.LogInformation("Create Alumno result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return ApiResponse.FromResult(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Create Alumno with Codigo {Codigo}", dto.CodigoAlumno);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AlumnoUpdateDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Update Alumno called for Id {Id}", id);
        try
        {
            if (id != dto.IdAlumno)
            {
                _logger.LogWarning("Update Alumno Id mismatch: route {RouteId} != body {BodyId}", id, dto.IdAlumno);
                return ApiResponse.FromResult(new(1, "Id distinto", null));
            }

            var entity = new Alumno
            {
                IdAlumno = dto.IdAlumno,
                CodigoAlumno = dto.CodigoAlumno,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                IdEscuela = dto.IdEscuela
            };

            var res = await _svc.EditarAsync(entity, ct);
            _logger.LogInformation("Update Alumno result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return ApiResponse.FromResult(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Update Alumno Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        _logger.LogInformation("Delete Alumno called for Id {Id}", id);
        try
        {
            var res = await _svc.EliminarAsync(id, ct);
            _logger.LogInformation("Delete Alumno result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return ApiResponse.FromResult(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Delete Alumno Id {Id}", id);
            return ApiResponse.FromException(ex);
        }
    }
}

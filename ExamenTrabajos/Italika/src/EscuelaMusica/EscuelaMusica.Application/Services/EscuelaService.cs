using EscuelaMusica.Application.Interface;
using Microsoft.Extensions.Logging;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Services;

public sealed class EscuelaService : IEscuelaService
{
    private readonly IEscuelaRepository _r;
    private readonly Microsoft.Extensions.Logging.ILogger<EscuelaService> _logger;
    public EscuelaService(IEscuelaRepository r, Microsoft.Extensions.Logging.ILogger<EscuelaService> logger)
    {
        _r = r;
        _logger = logger;
    }

    public async Task<OperationResult> CrearAsync(Escuela e, CancellationToken ct)
    {
        _logger.LogInformation("Creating escuela with Codigo {Codigo}", e.CodigoEscuela);
        try
        {
            var res = await _r.InsertarAsync(e, ct);
            _logger.LogInformation("Created escuela result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating escuela {Codigo}", e.CodigoEscuela);
            throw;
        }
    }

    public async Task<OperationResult> EditarAsync(Escuela e, CancellationToken ct)
    {
        _logger.LogInformation("Updating escuela Id {Id}", e.IdEscuela);
        try
        {
            var res = await _r.ActualizarAsync(e, ct);
            _logger.LogInformation("Updated escuela result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating escuela Id {Id}", e.IdEscuela);
            throw;
        }
    }

    public async Task<OperationResult> EliminarAsync(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting escuela Id {Id}", id);
        try
        {
            var res = await _r.EliminarAsync(id, ct);
            _logger.LogInformation("Deleted escuela result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting escuela Id {Id}", id);
            throw;
        }
    }

    public async Task<IReadOnlyList<Escuela>> ListarAsync(CancellationToken ct)
    {
        _logger.LogInformation("Listing escuelas");
        try
        {
            var list = await _r.ObtenerTodasAsync(ct);
            _logger.LogInformation("Listed {Count} escuelas", list?.Count ?? 0);
            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing escuelas");
            throw;
        }
    }

    public async Task<Escuela?> ObtenerAsync(int id, CancellationToken ct)
    {
        _logger.LogInformation("Getting escuela by Id {Id}", id);
        try
        {
            var e = await _r.ObtenerPorIdAsync(id, ct);
            if (e is null) _logger.LogInformation("Escuela Id {Id} not found", id);
            else _logger.LogInformation("Escuela Id {Id} retrieved", id);
            return e;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting escuela Id {Id}", id);
            throw;
        }
    }
}

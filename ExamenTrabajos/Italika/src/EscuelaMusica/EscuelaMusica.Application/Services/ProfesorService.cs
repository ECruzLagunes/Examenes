using EscuelaMusica.Application.Interface;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Services
{
    using Microsoft.Extensions.Logging;

    public sealed class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _r;
        private readonly ILogger<ProfesorService> _logger;

        public ProfesorService(IProfesorRepository r, ILogger<ProfesorService> logger)
        {
            _r = r;
            _logger = logger;
        }

        public async Task<OperationResult> CrearAsync(Profesor p, CancellationToken ct)
        {
            _logger.LogInformation("Creating profesor with CodigoProfesor {Codigo}", p.CodigoProfesor);
            try
            {
                var res = await _r.InsertarAsync(p, ct);
                _logger.LogInformation("Created profesor result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating profesor {Codigo}", p.CodigoProfesor);
                throw;
            }
        }

        public async Task<OperationResult> EditarAsync(Profesor p, CancellationToken ct)
        {
            _logger.LogInformation("Updating profesor Id {Id}", p.IdProfesor);
            try
            {
                var res = await _r.ActualizarAsync(p, ct);
                _logger.LogInformation("Updated profesor result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profesor Id {Id}", p.IdProfesor);
                throw;
            }
        }

        public async Task<OperationResult> EliminarAsync(int id, CancellationToken ct)
        {
            _logger.LogInformation("Deleting profesor Id {Id}", id);
            try
            {
                var res = await _r.EliminarAsync(id, ct);
                _logger.LogInformation("Deleted profesor result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting profesor Id {Id}", id);
                throw;
            }
        }

        public async Task<IReadOnlyList<Profesor>> ListarAsync(CancellationToken ct)
        {
            _logger.LogInformation("Listing profesores");
            try
            {
                var list = await _r.ObtenerTodasAsync(ct);
                _logger.LogInformation("Listed {Count} profesores", list?.Count ?? 0);
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing profesores");
                throw;
            }
        }

        public async Task<Profesor?> ObtenerAsync(int id, CancellationToken ct)
        {
            _logger.LogInformation("Getting profesor by Id {Id}", id);
            try
            {
                var p = await _r.ObtenerPorIdAsync(id, ct);
                if (p is null) _logger.LogInformation("Profesor Id {Id} not found", id);
                else _logger.LogInformation("Profesor Id {Id} retrieved", id);
                return p;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting profesor Id {Id}", id);
                throw;
            }
        }
    }
}

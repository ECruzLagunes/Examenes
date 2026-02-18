using EscuelaMusica.Application.Interface;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Application.Services
{
    using Microsoft.Extensions.Logging;

    public sealed class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _r;
        private readonly ILogger<AlumnoService> _logger;

        public AlumnoService(IAlumnoRepository r, ILogger<AlumnoService> logger)
        {
            _r = r;
            _logger = logger;
        }

        public async Task<OperationResult> CrearAsync(Alumno a, CancellationToken ct)
        {
            _logger.LogInformation("Creating alumno with CodigoAlumno {Codigo}", a.CodigoAlumno);
            try
            {
                var res = await _r.InsertarAsync(a, ct);
                _logger.LogInformation("Created alumno result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating alumno {Codigo}", a.CodigoAlumno);
                throw;
            }
        }

        public async Task<OperationResult> EditarAsync(Alumno a, CancellationToken ct)
        {
            _logger.LogInformation("Updating alumno Id {Id}", a.IdAlumno);
            try
            {
                var res = await _r.ActualizarAsync(a, ct);
                _logger.LogInformation("Updated alumno result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating alumno Id {Id}", a.IdAlumno);
                throw;
            }
        }

        public async Task<OperationResult> EliminarAsync(int id, CancellationToken ct)
        {
            _logger.LogInformation("Deleting alumno Id {Id}", id);
            try
            {
                var res = await _r.EliminarAsync(id, ct);
                _logger.LogInformation("Deleted alumno result Codigo={Codigo} Id={Id}", res.Codigo, res.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting alumno Id {Id}", id);
                throw;
            }
        }

        public async Task<IReadOnlyList<Alumno>> ListarAsync(CancellationToken ct)
        {
            _logger.LogInformation("Listing alumnos");
            try
            {
                var list = await _r.ObtenerTodasAsync(ct);
                _logger.LogInformation("Listed {Count} alumnos", list?.Count ?? 0);
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing alumnos");
                throw;
            }
        }

        public async Task<Alumno?> ObtenerAsync(int id, CancellationToken ct)
        {
            _logger.LogInformation("Getting alumno by Id {Id}", id);
            try
            {
                var a = await _r.ObtenerPorIdAsync(id, ct);
                if (a is null) _logger.LogInformation("Alumno Id {Id} not found", id);
                else _logger.LogInformation("Alumno Id {Id} retrieved", id);
                return a;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting alumno Id {Id}", id);
                throw;
            }
        }
    }
}

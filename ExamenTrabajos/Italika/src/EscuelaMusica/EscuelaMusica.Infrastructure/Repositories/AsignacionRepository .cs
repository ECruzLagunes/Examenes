using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace EscuelaMusica.Infrastructure.Repositories;

internal sealed class AsignacionRepository : BaseRepository, IAsignacionRepository
{
    public AsignacionRepository(IConfiguration cfg) : base(cfg) { }

    public Task<OperationResult> AsignarAsync(int aId, int pId, CancellationToken ct)
        => ExecCrudAsync("dbo.SpAsignaciones", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "A");
            cmd.Parameters.AddWithValue("@IdAlumno", aId);
            cmd.Parameters.AddWithValue("@IdProfesor", pId);
        }, ct);

    public Task<OperationResult> DesasignarAsync(int aId, int pId, CancellationToken ct)
        => ExecCrudAsync("dbo.SpAsignaciones", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "D");
            cmd.Parameters.AddWithValue("@IdAlumno", aId);
            cmd.Parameters.AddWithValue("@IdProfesor", pId);
        }, ct);

    public async Task<IReadOnlyList<Alumno>> AlumnosPorProfesorAsync(int pId, CancellationToken ct)
    {
        var list = new List<Alumno>();
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);

        await using var cmd = new SqlCommand("dbo.SpAsignaciones", cn)
        { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@Accion", "P");
        cmd.Parameters.AddWithValue("@IdProfesor", pId);

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        while (await dr.ReadAsync(ct))
            list.Add(new Alumno
            {
                IdAlumno = dr.GetInt32("IdAlumno"),
                Nombre = dr.GetString("Nombre"),
                Apellido = dr.GetString("Apellido"),
                IdEscuela = dr.GetInt32("IdEscuela")
            });
        return list;
    }

    public async Task<IReadOnlyList<Profesor>> ProfesoresPorAlumnoAsync(int aId, CancellationToken ct)
    {
        var list = new List<Profesor>();
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);

        await using var cmd = new SqlCommand(
            """
            SELECT p.* FROM dbo.AlumnosProfesores ap
            JOIN dbo.Profesores p ON ap.IdProfesor = p.IdProfesor
            WHERE ap.IdAlumno = @al
            """, cn);
        cmd.Parameters.AddWithValue("@al", aId);

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        while (await dr.ReadAsync(ct))
            list.Add(new Profesor
            {
                IdProfesor = dr.GetInt32("IdProfesor"),
                CodigoProfesor = dr.GetString("CodigoProfesor"),
                Nombre = dr.GetString("Nombre"),
                Apellido = dr.GetString("Apellido"),
                IdEscuela = dr.IsDBNull("IdEscuela") ? null : dr.GetInt32("IdEscuela")
            });
        return list;
    }
}
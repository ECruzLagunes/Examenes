using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Domain.Common;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EscuelaMusica.Infrastructure.Repositories;
internal sealed class AlumnoRepository : BaseRepository, IAlumnoRepository
{
    public AlumnoRepository(IConfiguration cfg) : base(cfg) { }

    public Task<OperationResult> InsertarAsync(Alumno a, CancellationToken ct)
        => ExecCrudAsync("dbo.SpAlumnos", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "I");
            cmd.Parameters.AddWithValue("@CodigoAlumno", a.CodigoAlumno);
            cmd.Parameters.AddWithValue("@Nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", a.Apellido);
            cmd.Parameters.AddWithValue("@FechaNacimiento", a.FechaNacimiento.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@IdEscuela", (object?)a.IdEscuela ?? DBNull.Value);
        }, ct);

    public Task<OperationResult> ActualizarAsync(Alumno a, CancellationToken ct)
        => ExecCrudAsync("dbo.SpAlumnos", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "U");
            cmd.Parameters.AddWithValue("@IdAlumno", a.IdAlumno);
            cmd.Parameters.AddWithValue("@Nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", a.Apellido);
            cmd.Parameters.AddWithValue("@FechaNacimiento", a.FechaNacimiento.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@IdEscuela", (object?)a.IdEscuela ?? DBNull.Value);
        }, ct);

    public Task<OperationResult> EliminarAsync(int id, CancellationToken ct)
        => ExecCrudAsync("dbo.SpAlumnos", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "D");
            cmd.Parameters.AddWithValue("@IdAlumno", id);
        }, ct);

    public async Task<IReadOnlyList<Alumno>> ObtenerTodasAsync(CancellationToken ct)
    {
        var list = new List<Alumno>();
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);

        await using var cmd = new SqlCommand("dbo.SpAlumnos", cn)
        { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@Accion", "R");

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        while (await dr.ReadAsync(ct)) list.Add(Map(dr));
        return list;
    }

    public async Task<Alumno?> ObtenerPorIdAsync(int id, CancellationToken ct)
    {
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);
        await using var cmd = new SqlCommand("dbo.SpAlumnos", cn)
        { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@Accion", "B");
        cmd.Parameters.AddWithValue("@IdAlumno", id);

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        return await dr.ReadAsync(ct) ? Map(dr) : null;
    }

    private static Alumno Map(SqlDataReader dr) => new()
    {
        IdAlumno = dr.GetInt32("IdAlumno"),
        CodigoAlumno = dr.GetString("CodigoAlumno"),
        Nombre = dr.GetString("Nombre"),
        Apellido = dr.GetString("Apellido"),
        FechaNacimiento = DateOnly.FromDateTime(dr.GetDateTime("FechaNacimiento")),
        IdEscuela = dr.IsDBNull("IdEscuela") ? null : dr.GetInt32("IdEscuela")
    };
}
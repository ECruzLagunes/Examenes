using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Domain.Common;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EscuelaMusica.Infrastructure.Repositories;
internal sealed class ProfesorRepository : BaseRepository, IProfesorRepository
{
    public ProfesorRepository(IConfiguration cfg) : base(cfg) { }

    public Task<OperationResult> InsertarAsync(Profesor p, CancellationToken ct)
        => ExecCrudAsync("dbo.SP_Profesores_CRUD", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "I");
            cmd.Parameters.AddWithValue("@CodigoProfesor", p.CodigoProfesor);
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", p.Apellido);
            cmd.Parameters.AddWithValue("@IdEscuela",
                (object?)p.IdEscuela ?? DBNull.Value);
        }, ct);

    public Task<OperationResult> ActualizarAsync(Profesor p, CancellationToken ct)
        => ExecCrudAsync("dbo.SP_Profesores_CRUD", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "U");
            cmd.Parameters.AddWithValue("@IdProfesor", p.IdProfesor);
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", p.Apellido);
            cmd.Parameters.AddWithValue("@IdEscuela",
                (object?)p.IdEscuela ?? DBNull.Value);
        }, ct);

    public Task<OperationResult> EliminarAsync(int id, CancellationToken ct)
        => ExecCrudAsync("dbo.SP_Profesores_CRUD", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "D");
            cmd.Parameters.AddWithValue("@IdProfesor", id);
        }, ct);

    public async Task<IReadOnlyList<Profesor>> ObtenerTodasAsync(CancellationToken ct)
    {
        var list = new List<Profesor>();
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);

        await using var cmd = new SqlCommand("dbo.SP_Profesores_CRUD", cn)
        { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@Accion", "R");

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        while (await dr.ReadAsync(ct)) list.Add(Map(dr));
        return list;
    }

    public async Task<Profesor?> ObtenerPorIdAsync(int id, CancellationToken ct)
    {
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);
        await using var cmd = new SqlCommand("dbo.SP_Profesores_CRUD", cn)
        { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@Accion", "B");
        cmd.Parameters.AddWithValue("@IdProfesor", id);

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        return await dr.ReadAsync(ct) ? Map(dr) : null;
    }

    private static Profesor Map(SqlDataReader dr) => new()
    {
        IdProfesor = dr.GetInt32("IdProfesor"),
        CodigoProfesor = dr.GetString("CodigoProfesor"),
        Nombre = dr.GetString("Nombre"),
        Apellido = dr.GetString("Apellido"),
        IdEscuela = dr.IsDBNull("IdEscuela") ? null : dr.GetInt32("IdEscuela")
    };
}
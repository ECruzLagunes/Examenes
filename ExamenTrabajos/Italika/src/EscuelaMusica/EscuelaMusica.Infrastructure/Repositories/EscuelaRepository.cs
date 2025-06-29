using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Domain.Entities;
using EscuelaMusica.Domain.Common;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EscuelaMusica.Infrastructure.Repositories;

internal sealed class EscuelaRepository : BaseRepository, IEscuelaRepository
{
    public EscuelaRepository(IConfiguration cfg) : base(cfg) { }

    public Task<OperationResult> InsertarAsync(Escuela e, CancellationToken ct)
        => ExecCrudAsync("dbo.SP_Escuelas_CRUD", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "I");
            cmd.Parameters.AddWithValue("@CodigoEscuela", e.CodigoEscuela);
            cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion",
                (object?)e.Descripcion ?? DBNull.Value);
        }, ct);

    public Task<OperationResult> ActualizarAsync(Escuela e, CancellationToken ct)
        => ExecCrudAsync("dbo.SP_Escuelas_CRUD", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "U");
            cmd.Parameters.AddWithValue("@IdEscuela", e.IdEscuela);
            cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion",
                (object?)e.Descripcion ?? DBNull.Value);
        }, ct);

    public Task<OperationResult> EliminarAsync(int id, CancellationToken ct)
        => ExecCrudAsync("dbo.SP_Escuelas_CRUD", cmd =>
        {
            cmd.Parameters.AddWithValue("@Accion", "D");
            cmd.Parameters.AddWithValue("@IdEscuela", id);
        }, ct);

    /*---- lecturas ----*/
    public async Task<IReadOnlyList<Escuela>> ObtenerTodasAsync(CancellationToken ct)
    {
        var list = new List<Escuela>();
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);

        await using var cmd = new SqlCommand("dbo.SP_Escuelas_CRUD", cn)
        { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@Accion", "R");

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        while (await dr.ReadAsync(ct)) list.Add(Map(dr));
        return list;
    }

    public async Task<Escuela?> ObtenerPorIdAsync(int id, CancellationToken ct)
    {
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);
        await using var cmd = new SqlCommand("dbo.SP_Escuelas_CRUD", cn)
        { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("@Accion", "B");
        cmd.Parameters.AddWithValue("@IdEscuela", id);

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        return await dr.ReadAsync(ct) ? Map(dr) : null;
    }

    private static Escuela Map(SqlDataReader dr) => new()
    {
        IdEscuela = dr.GetInt32("IdEscuela"),
        CodigoEscuela = dr.GetString("CodigoEscuela"),
        Nombre = dr.GetString("Nombre"),
        Descripcion = dr.IsDBNull("Descripcion") ? null : dr.GetString("Descripcion")
    };
}
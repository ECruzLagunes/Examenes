using EscuelaMusica.Domain.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EscuelaMusica.Infrastructure.Repositories;

internal abstract class BaseRepository
{
    protected readonly string _cs;
    protected BaseRepository(IConfiguration cfg) =>
        _cs = cfg.GetConnectionString("EscuelasDB")!;

    protected async Task<OperationResult> ExecCrudAsync(
        string sp,
        Action<SqlCommand> buildParams,
        CancellationToken ct)
    {
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync(ct);
        await using var cmd = new SqlCommand(sp, cn) { CommandType = CommandType.StoredProcedure };
        buildParams(cmd);

        await using var dr = await cmd.ExecuteReaderAsync(ct);
        await dr.ReadAsync(ct);
        return new(
            dr.GetInt32(0),
            dr.GetString(1),
            dr.IsDBNull(2) ? null : dr.GetInt32(2));
    }
}
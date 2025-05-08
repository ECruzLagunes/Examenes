namespace EnsayoCrudNetAngular.Infrastructure.DataAccess
{
    using EnsayoCrudNetAngular.Application.Dto;
    using EnsayoCrudNetAngular.Domain.Entities;
    using EnsayoCrudNetAngular.Infrastructure.Helpers;

    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System.Data;

    public class EmpleadoDataAccess
    {
        private readonly string _connectionString;

        public EmpleadoDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<List<Empleado>> ObtenerListaEmpleadosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("Usp_ListaEmpleados", connection)
            {
                CommandType = CommandType.StoredProcedure
            };


            await connection.OpenAsync();

            var empleados = new List<Empleado>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var empleado = reader.MapToEntity<Empleado>();
                    empleados.Add(empleado);
                }
            }

            return empleados;
        }

        public async Task<Empleado?> ObtenerEmpleadoPorIdAsync(int idEmpleado)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("Usp_ObtenerEmpleadoPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@IdEmpleado", SqlDbType.Int) { Value = idEmpleado });

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows && await reader.ReadAsync())
            {
                return reader.MapToEntity<Empleado>();
            }

            return null;
        }

        public async Task<int> InsertarAsync(EmpleadoDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("Usp_InsertaEmpleado", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@NombreCompleto", dto.NombreCompleto);
            cmd.Parameters.AddWithValue("@Correo", dto.Correo);
            cmd.Parameters.AddWithValue("@Sueldo", dto.Sueldo);
            cmd.Parameters.AddWithValue("@FechaContrato", dto.FechaContrato);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<bool> EditarAsync(EmpleadoDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("Usp_EditarEmpleado", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@IdEmpleado", dto.IdEmpleado);
            cmd.Parameters.AddWithValue("@NombreCompleto", dto.NombreCompleto);
            cmd.Parameters.AddWithValue("@Correo", dto.Correo);
            cmd.Parameters.AddWithValue("@Sueldo", dto.Sueldo);
            cmd.Parameters.AddWithValue("@FechaContrato", dto.FechaContrato);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("Usp_EliminarEmpleado", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@IdEmpleado", id);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }


}

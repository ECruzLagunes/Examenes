namespace EnsayoCrudNetAngular.Infrastructure.DataAccess
{
    using EnsayoCrudNetAngular.Domain.Entities;
    using EnsayoCrudNetAngular.Infrastructure.Helpers;
    using EnsayoCrudNetAngular.Infrastructure.Interface;
    using Microsoft.Data.SqlClient;
    using System.Data;

    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly string _connectionString;

        public EmpleadoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Empleado>> ObtenerListaEmpleadosAsync()
        {
            var empleados = new List<Empleado>();

            using var connection = new SqlConnection(_connectionString);

            try
            {
                using var command = new SqlCommand("Usp_ListaEmpleados", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var empleado = reader.MapToEntity<Empleado>();
                    empleados.Add(empleado);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
                connection.Close();
            }

            return empleados;
        }

        public async Task<Empleado?> ObtenerEmpleadoPorIdAsync(int idEmpleado)
        {
            var empleado = new Empleado();

            using var connection = new SqlConnection(_connectionString);

            try
            {
                using var command = new SqlCommand("Usp_ObtenerEmpleadoPorId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@IdEmpleado", SqlDbType.Int) { Value = idEmpleado });

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                
                if (reader.HasRows && await reader.ReadAsync())
                {
                    empleado = reader.MapToEntity<Empleado>();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return empleado;
        }

        //public async Task<int> InsertarAsync(EmpleadoDto dto)
        //{
        //    using var conn = new SqlConnection(_connectionString);
        //    using var cmd = new SqlCommand("Usp_InsertaEmpleado", conn)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };

        //    cmd.Parameters.AddWithValue("@NombreCompleto", dto.NombreCompleto);
        //    cmd.Parameters.AddWithValue("@Correo", dto.Correo);
        //    cmd.Parameters.AddWithValue("@Sueldo", dto.Sueldo);
        //    cmd.Parameters.AddWithValue("@FechaContrato", dto.FechaContrato);

        //    await conn.OpenAsync();
        //    var result = await cmd.ExecuteScalarAsync();
        //    return Convert.ToInt32(result);
        //}

        //public async Task<bool> EditarAsync(EmpleadoDto dto)
        //{
        //    using var conn = new SqlConnection(_connectionString);
        //    using var cmd = new SqlCommand("Usp_EditarEmpleado", conn)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };

        //    cmd.Parameters.AddWithValue("@IdEmpleado", dto.IdEmpleado);
        //    cmd.Parameters.AddWithValue("@NombreCompleto", dto.NombreCompleto);
        //    cmd.Parameters.AddWithValue("@Correo", dto.Correo);
        //    cmd.Parameters.AddWithValue("@Sueldo", dto.Sueldo);
        //    cmd.Parameters.AddWithValue("@FechaContrato", dto.FechaContrato);

        //    await conn.OpenAsync();
        //    return await cmd.ExecuteNonQueryAsync() > 0;
        //}

        //public async Task<bool> EliminarAsync(int id)
        //{
        //    using var conn = new SqlConnection(_connectionString);
        //    using var cmd = new SqlCommand("Usp_EliminarEmpleado", conn)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };

        //    cmd.Parameters.AddWithValue("@IdEmpleado", id);

        //    await conn.OpenAsync();
        //    return await cmd.ExecuteNonQueryAsync() > 0;
        //}
    }


}

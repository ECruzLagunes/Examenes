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

        public async Task<(int idGenerado, string mensaje)> InsertarEmpleadoAsync(Empleado empleado)
        {
            int idGenerado = 0;
            string mensaje = "";

            using var connection = new SqlConnection(_connectionString);
            try
            {
                using var command = new SqlCommand("Usp_InsertaEmpleado", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                command.Parameters.AddWithValue("@Correo", empleado.Correo);
                command.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);
                command.Parameters.AddWithValue("@FechaContrato", empleado.FechaContrato);

                var idGeneradoParam = new SqlParameter("@IdGenerado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

                command.Parameters.Add(idGeneradoParam);
                command.Parameters.Add(mensajeParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                idGenerado = (int)(idGeneradoParam.Value ?? 0);
                mensaje = mensajeParam.Value?.ToString() ?? "Error desconocido";
            }
            catch (Exception ex)
            {
                idGenerado = 0;
                mensaje = $"Error inesperado: {ex.Message}";
            }
            finally
            {
                connection.Close();
            }

            return (idGenerado, mensaje);
        }

        public async Task<(int codigo, string mensaje)> EditarEmpleadoAsync(Empleado empleado)
        {
            int resultado = 0;
            string mensaje = string.Empty;

            using var connection = new SqlConnection(_connectionString);
            try
            {
                using var command = new SqlCommand("Usp_EditaEmpleado", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado);
                command.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                command.Parameters.AddWithValue("@Correo", empleado.Correo);
                command.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);
                command.Parameters.AddWithValue("@FechaContrato", empleado.FechaContrato);

                var resultadoParam = new SqlParameter("@Resultado", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(resultadoParam);

                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(mensajeParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                resultado = (int)(resultadoParam.Value ?? -1);
                mensaje = mensajeParam.Value?.ToString() ?? "Error desconocido";
            }
            catch (Exception e)
            {
                resultado = -1;
                mensaje = $"Error inesperado: {e.Message}";
            }
            finally
            {
                await connection.CloseAsync();
            }

            return (resultado, mensaje);
        }

        public async Task<(int idEliminado, int resultado, string mensaje)> EliminarEmpleadoAsync(int idEmpleado)
        {
            int resultado = 0;
            int idEliminado = 0;
            string mensaje = string.Empty;

            using var connection = new SqlConnection(_connectionString);

            try
            {
                using var command = new SqlCommand("Usp_EliminaEmpleado", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@IdEmpleado", SqlDbType.Int) { Value = idEmpleado });

                var resultadoParam = new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
                var idEliminadoParam = new SqlParameter("@IdEmpleadoEliminado", SqlDbType.Int) { Direction = ParameterDirection.Output };

                command.Parameters.Add(resultadoParam);
                command.Parameters.Add(mensajeParam);
                command.Parameters.Add(idEliminadoParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                resultado = (int)(resultadoParam.Value ?? 0);
                mensaje = mensajeParam.Value?.ToString() ?? "Error desconocido.";
                idEliminado = (int)(idEliminadoParam.Value ?? 0);
            }
            catch (Exception ex)
            {
                resultado = -1;
                mensaje = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return (idEliminado, resultado, mensaje);
        }
    }
}

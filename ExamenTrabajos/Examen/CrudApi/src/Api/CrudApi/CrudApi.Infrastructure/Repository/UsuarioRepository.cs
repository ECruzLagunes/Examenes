using CrudApi.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApi.Infrastructure.Interface
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<(int resultado, string mensaje)> InsertarPokemonFavoritoAsync(UsuarioPokemon favorito)
        {
            int resultado = 0;
            string mensaje = string.Empty;

            using var connection = new SqlConnection(_connectionString);

            try
            {
                using var command = new SqlCommand("Usp_InsertarPokemonFavorito", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = favorito.IdUsuario });
                command.Parameters.Add(new SqlParameter("@IdPokemon", SqlDbType.Int) { Value = favorito.PokemonId });

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(paramResultado);
                command.Parameters.Add(paramMensaje);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                resultado = (int)paramResultado.Value;
                mensaje = paramMensaje.Value.ToString() ?? "";
            }
            catch (Exception ex)
            {
                resultado = -1;
                mensaje = $"Error inesperado en el repositorio: {ex.Message}";
            }
            finally
            {
                connection.Close();
            }

            return (resultado, mensaje);
        }
        public async Task<(int resultado, string mensaje, int idUsuario)> ValidarUsuarioAsync(string nombreUsuario, string contrasenaHash)
        {
            var resultado = 0;
            var mensaje = "";
            var idUsuario = 0;

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("Usp_ValidarUsuario", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
            command.Parameters.AddWithValue("@Contraseña", contrasenaHash);

            var outputResultado = new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var outputMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var outputIdUsuario = new SqlParameter("@IdUsuario", SqlDbType.Int) { Direction = ParameterDirection.Output };

            command.Parameters.Add(outputResultado);
            command.Parameters.Add(outputMensaje);
            command.Parameters.Add(outputIdUsuario);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                resultado = Convert.ToInt32(outputResultado.Value);
                mensaje = outputMensaje.Value.ToString()!;
                idUsuario = Convert.ToInt32(outputIdUsuario.Value);
            }
            catch (Exception e)
            {
                resultado = -1;
                mensaje = $"Error inesperado: {e.Message}";
            }
            finally
            {
                connection.Close();
            }

            return (resultado, mensaje, idUsuario);
        }
    }
}

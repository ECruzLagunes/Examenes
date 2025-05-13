using CrudApi.Application.Interface;
using CrudApi.Domain.Entities;
using CrudApi.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrudApi.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(int idUser, int resultado, string mensaje)> ValidarUsuarioAsync(string usuario, string contrasena)
        {
            var resultado = 0;
            var mensaje = "";

            try
            {
                var (result, mens, idusuario) =  await _usuarioRepository.ValidarUsuarioAsync(usuario, CalcularSHA512(contrasena));

                if (result > 0)
                {
                    resultado = 0;
                    mensaje = mens;
                }
                else
                {
                    resultado = 1;
                    mensaje = mens;
                }
            }
            catch (Exception e)
            {
                resultado = -1;
                mensaje = $"Ocurrió un error inesperado: {e.Message}";
            }

            return (resultado, resultado, mensaje);
        }

        public async Task<(int resultado, string mensaje)> InsertarPokemonFavoritoAsync(UsuarioPokemon favorito)
        {
            try
            {
                return await _usuarioRepository.InsertarPokemonFavoritoAsync(favorito);
            }
            catch (Exception ex)
            {
                return (-1, $"Error inesperado: {ex.Message}");
            }
        }
        public static string CalcularSHA512(string texto)
        {
            using var sha512 = SHA512.Create();
            var bytes = Encoding.UTF8.GetBytes(texto);
            var hash = sha512.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}

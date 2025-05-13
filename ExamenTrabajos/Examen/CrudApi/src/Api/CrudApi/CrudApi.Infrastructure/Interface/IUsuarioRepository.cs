using CrudApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApi.Infrastructure.Interface
{
    public interface IUsuarioRepository
    {
        Task<(int resultado, string mensaje, int idUsuario)> ValidarUsuarioAsync(string nombreUsuario, string contrasenaHash);
        Task<(int resultado, string mensaje)> InsertarPokemonFavoritoAsync(UsuarioPokemon favorito);
    }
}

using CrudApi.Domain.Entities;

namespace CrudApi.Application.Interface
{
    public interface IUsuarioService
    {
        Task<(int idUser, int resultado, string mensaje)> ValidarUsuarioAsync(string usuario, string contrasena);
        Task<(int resultado, string mensaje)> InsertarPokemonFavoritoAsync(UsuarioPokemon favorito);
    }
}

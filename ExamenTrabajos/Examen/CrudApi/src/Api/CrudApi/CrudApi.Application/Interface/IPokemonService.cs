using CrudApi.Domain.Entities;

namespace CrudApi.Application.Interface
{
    public interface IPokemonService
    {
        Task<(List<PokemonResumen> lista, int codigo, string mensaje)> FiltrarPorNombreOIdAsync(string filtro);
        Task<(PokemonDetail? detalle, int codigo, string mensaje)> ObtenerPokemonPorIdAsync(int id);
    }
}

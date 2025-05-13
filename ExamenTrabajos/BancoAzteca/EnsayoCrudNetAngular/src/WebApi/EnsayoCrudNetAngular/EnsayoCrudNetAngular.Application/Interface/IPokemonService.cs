using EnsayoCrudNetAngular.Domain.Entities;

namespace EnsayoCrudNetAngular.Application.Interface
{
    public interface IPokemonService
    {
        Task<(PokemonDetail? detalle, int codigo, string mensaje)> ObtenerPokemonPorNombreAsync(string nombre);
        Task<List<PokemonResumen>> FiltrarPorNombreConIdAsync(string nombreParcial);
    }
}

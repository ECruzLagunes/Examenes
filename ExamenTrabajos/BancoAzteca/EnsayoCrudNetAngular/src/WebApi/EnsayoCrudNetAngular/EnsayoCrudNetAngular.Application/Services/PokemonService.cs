using EnsayoCrudNetAngular.Application.Interface;
using EnsayoCrudNetAngular.Domain.Entities;
using System.ComponentModel;
using System.Net.Http.Json;

namespace EnsayoCrudNetAngular.Application.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<(PokemonDetail? detalle, int codigo, string mensaje)> ObtenerPokemonPorNombreAsync(string nombre)
        {
            try
            {
                var detalle = await _httpClient.GetFromJsonAsync<PokemonDetail>($"pokemon/{nombre.ToLower()}");

                if (detalle is null)
                    return (null, 1, "No se encontró el Pokémon.");

                return (detalle, 0, "Pokémon obtenido correctamente.");
            }
            catch (HttpRequestException e)
            {
                return (null, -1, $"Error al conectar con PokéAPI: {e.Message}");
            }
        }

        public async Task<List<PokemonResumen>> FiltrarPorNombreConIdAsync(string nombreParcial)
        {
            var resultado = new List<PokemonResumen>();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<PokeApiResponse>("pokemon?limit=100000&offset=0");

                var coincidencias = response?.Results
                    .Where(p => p.Name.Contains(nombreParcial, StringComparison.OrdinalIgnoreCase))
                    .Select(p => p.Name)
                    .ToList() ?? new List<string>();

                foreach (var nombre in coincidencias)
                {
                    try
                    {
                        var detalle = await _httpClient.GetFromJsonAsync<PokemonDetail>($"pokemon/{nombre}");
                        if (detalle != null)
                        {
                            resultado.Add(new PokemonResumen
                            {
                                Id = detalle.Id,
                                Nombre = detalle.Name
                            });
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch
            {
            }

            return resultado;
        }
    }
}

using CrudApi.Application.Interface;
using CrudApi.Domain.Entities;
using System.Net.Http.Json;

namespace CrudApi.Application.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<(List<PokemonResumen> lista, int codigo, string mensaje)> FiltrarPorNombreOIdAsync(string filtro)
        {
            var resultado = new List<PokemonResumen>();

            try
            {
                var response = await _httpClient.GetFromJsonAsync<PokeApiResponse>("pokemon?limit=100000&offset=0");

                if (response?.Results == null)
                    return ([], 1, "No se encontraron pokémons.");

                var coincidencias = response.Results
                    .Where(p => p.Name.Contains(filtro, StringComparison.OrdinalIgnoreCase))
                    .Select(p => p.Name)
                    .ToList();

                if (int.TryParse(filtro, out int idFiltro))
                {
                    try
                    {
                        var porId = await _httpClient.GetFromJsonAsync<PokemonDetail>($"pokemon/{idFiltro}");
                        if (porId != null && !coincidencias.Contains(porId.Name))
                        {
                            coincidencias.Insert(0, porId.Name);
                        }
                    }
                    catch { }
                }

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

                return (resultado, 0, "Pokémons filtrados correctamente.");
            }
            catch (Exception ex)
            {
                return ([], -1, $"Error al obtener los pokémons: {ex.Message}");
            }
        }

        public async Task<(PokemonDetail? detalle, int codigo, string mensaje)> ObtenerPokemonPorIdAsync(int id)
        {
            try
            {
                var detalle = await _httpClient.GetFromJsonAsync<PokemonDetail>($"pokemon/{id}");

                if (detalle is null)
                    return (null, 1, "No se encontró el Pokémon con ese ID.");

                return (detalle, 0, "Pokémon obtenido correctamente.");
            }
            catch (HttpRequestException e)
            {
                return (null, -1, $"Error al conectar con PokéAPI: {e.Message}");
            }
        }
    }
}

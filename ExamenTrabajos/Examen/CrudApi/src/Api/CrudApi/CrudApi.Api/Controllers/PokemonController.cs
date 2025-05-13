using CrudApi.Application.Interface;
using CrudApi.Application.Services;
using CrudApi.Domain.Entities;
using CrudApi.Domain.Servicices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IUsuarioService _usuariosService;
        private readonly IJwtService _jwtService;
        private readonly IPokemonService _pokemonService;

        public PokemonController(IUsuarioService empleadoService, IJwtService jwtService, IPokemonService pokemonService)
        {
            _usuariosService = empleadoService;
            _jwtService = jwtService;
            _pokemonService = pokemonService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Domain.Entities.LoginRequest request)
        {
            var user = await _usuariosService.ValidarUsuarioAsync(request.Usuario, request.Contraseña);
            if (user.resultado == 0)
            {                
                var token = _jwtService.GenerateToken("1", "Admin");

                return Ok(new
                {
                    token
                });
            }

            return Unauthorized("Usuario o contraseña incorrectos");
        }


        [HttpPost("ObtenerPokemon")]
        [Authorize]
        public async Task<IActionResult> ObtenerPokemon(string nombre)
        {
            var response = new ServiceResponse();

            var listaPokemons = await _pokemonService.FiltrarPorNombreOIdAsync(nombre);

            if (listaPokemons.codigo == 0)
            {
                response.Mensaje = listaPokemons.mensaje;
                response.Codigo = 200;
                response.Resultado = listaPokemons.lista;
            }
            else if (listaPokemons.codigo == 1)
            {
                response.Mensaje = listaPokemons.mensaje;
                response.Codigo = 400;
                response.Resultado = listaPokemons.lista;
            }
            else if (listaPokemons.codigo == -1)
            {
                response.Mensaje = listaPokemons.mensaje;
                response.Codigo = 500;
                response.Resultado = listaPokemons.lista;
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPost("insertar-pokemon-favorito")]
        public async Task<IActionResult> InsertarPokemonFavorito([FromBody] UsuarioPokemon favorito)
        {
            var response = new ServiceResponse();
            var (resultado, mensaje) = await _usuariosService.InsertarPokemonFavoritoAsync(favorito);

            if (resultado == 1)
            {
                response.Codigo = 200;
                response.Mensaje = mensaje;
                response.Resultado = null;
            }
            else if (resultado == 0)
            {
                response.Codigo = 400;
                response.Mensaje = mensaje;
                response.Resultado = null;
            }
            else
            {
                response.Codigo = 500;
                response.Mensaje = mensaje;
                response.Resultado = null;
            }

            return Ok(response);
        }
    }
}


using EnsayoCrudNetAngular.Application.Interface;
using EnsayoCrudNetAngular.Application.Services;
using EnsayoCrudNetAngular.Domain.Entities;
using EnsayoCrudNetAngular.Domain.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnsayoCrudNetApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IJwtService _jwtService;
        private readonly IPokemonService _pokemonService;

        public EmpleadosController(IEmpleadoService empleadoService, IJwtService jwtService, IPokemonService pokemonService)
        {
            _empleadoService = empleadoService; 
            _jwtService = jwtService; 
            _pokemonService = pokemonService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Usuario == "admin" && request.Password == "1234")
            {
                var token = _jwtService.GenerateToken("1", "Admin");

                return Ok(new
                {
                    token
                });
            }

            return Unauthorized("Usuario o contraseña incorrectos");
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new ServiceResponse();

            var (lista, resultado, mensaje) = await _empleadoService.ObtenerListaEmpleadosAsync();
            var a = await _pokemonService.FiltrarPorNombreConIdAsync("on");

            var (detalle, cod, msg) = await _pokemonService.ObtenerPokemonPorNombreAsync("pikachu");

            if (resultado == 0)
            {
                response.Mensaje = mensaje;
                response.Codigo = 200;
                response.Resultado = lista;
            }
            else if (resultado == 1)
            {
                response.Mensaje = mensaje;
                response.Codigo = 400;
                response.Resultado = lista;
            }
            else if (resultado == -1)
            {
                response.Mensaje = mensaje;
                response.Codigo = 500;
                response.Resultado = lista;
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ServiceResponse();

            var (empleado, codigo, mensaje) = await _empleadoService.ObtenerEmpleadoPorIdAsync(id);

            if (codigo == 0)
            {
                response.Mensaje = mensaje;
                response.Codigo = 200;
                response.Resultado = empleado;
            }
            else if (codigo == 1)
            {
                response.Mensaje = mensaje;
                response.Codigo = 400;
                response.Resultado = empleado;
            }
            else if (codigo == -1)
            {
                response.Mensaje = mensaje;
                response.Codigo = 500;
                response.Resultado = empleado;
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Empleado empleado)
        {
            var response = new ServiceResponse();

            var (idGenerado, codigo, mensaje) = await _empleadoService.InsertarEmpleadoAsync(empleado);

            response.Codigo = codigo switch
            {
                0 => 200,
                1 => 400,
                -1 => 500,
                _ => 500
            };
            response.Mensaje = mensaje;
            response.Resultado = idGenerado;

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Empleado empleado)
        {
            var response = new ServiceResponse();
            empleado.IdEmpleado = id;

            var (codigo, mensaje) = await _empleadoService.EditarEmpleadoAsync(empleado);

            response.Codigo = codigo switch
            {
                0 => 400,
                1 => 200,
                -1 => 500,
                _ => 500
            };
            response.Mensaje = mensaje;

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new ServiceResponse();

            var (codigo, mensaje, idEliminado) = await _empleadoService.EliminarEmpleadoAsync(id);

            response.Codigo = codigo switch
            {
                1 => 200,
                0 => 404,
                -1 => 500,
                _ => 400
            };

            response.Mensaje = mensaje;
            response.Resultado = idEliminado;

            return Ok(response);
        }
    }
}

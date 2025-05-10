
using EnsayoCrudNetAngular.Application.Interface;
using EnsayoCrudNetAngular.Domain.Entities;
using EnsayoCrudNetAngular.Domain.Servicios;
using EnsayoCrudNetAngular.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace EnsayoCrudNetApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadosController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new ServiceResponse();

            var (lista, resultado, mensaje) = await _empleadoService.ObtenerListaEmpleadosAsync();

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

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var result = await _data.ObtenerEmpleadoPorIdAsync(id);

        //    switch (result.codigo)                
        //    { 
        //        case 0:
        //    }

        //    return Ok(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] EmpleadoDto dto)
        //{
        //    var id = await _data.InsertarAsync(dto);
        //    return CreatedAtAction(nameof(Get), new { id }, dto);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] EmpleadoDto dto)
        //{
        //    dto.IdEmpleado = id;
        //    var success = await _data.EditarAsync(dto);
        //    return success ? NoContent() : NotFound();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var success = await _data.EliminarAsync(id);
        //    return success ? NoContent() : NotFound();
        //}
    }
}

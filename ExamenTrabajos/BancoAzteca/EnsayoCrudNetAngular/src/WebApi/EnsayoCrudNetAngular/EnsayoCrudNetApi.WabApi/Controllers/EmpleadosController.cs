using EnsayoCrudNetAngular.Application.Dto;
using EnsayoCrudNetAngular.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EnsayoCrudNetApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoDataAccess _data;

        public EmpleadosController(EmpleadoDataAccess data)
        {
            _data = data;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _data.ObtenerListaEmpleadosAsync();

            return Ok(resultado);
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _data.ObtenerEmpleadoPorIdAsync(id);
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpleadoDto dto)
        {
            var id = await _data.InsertarAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmpleadoDto dto)
        {
            dto.IdEmpleado = id;
            var success = await _data.EditarAsync(dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _data.EliminarAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}

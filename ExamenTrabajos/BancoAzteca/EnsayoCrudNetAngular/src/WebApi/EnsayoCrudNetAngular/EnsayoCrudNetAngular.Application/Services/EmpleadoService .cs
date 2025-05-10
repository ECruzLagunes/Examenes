using EnsayoCrudNetAngular.Application.Interface;
using EnsayoCrudNetAngular.Domain.Entities;
using EnsayoCrudNetAngular.Infrastructure.Interface;
using System.Collections;
using System.Collections.Generic;


namespace EnsayoCrudNetAngular.Application.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }
        public async Task<(List<Empleado> lista, int resultado, string mensaje)> ObtenerListaEmpleadosAsync()
        {
            var lista = await _empleadoRepository.ObtenerListaEmpleadosAsync();

            var resultado = 0;
            var mensaje = "";

            try
            {

                if (lista.Any())
                {
                    resultado = 0;
                    mensaje = "Empleados recuperados correctamente.";
                }
                else
                {
                    resultado = 1;
                    mensaje = "No se encontraron empleados.";
                }
            }
            catch (Exception e)
            {
                resultado = -1;
                mensaje = $"Ocurrió un error inesperado: {e.Message}";
            }

            return (lista, resultado, mensaje);
        }

        public async Task<(Empleado? empleado, int codigo, string mensaje)> ObtenerEmpleadoPorIdAsync(int idEmpleado)
        {
            var empleado = await _empleadoRepository.ObtenerEmpleadoPorIdAsync(idEmpleado);

            var resultado = 0;
            var mensaje = "";

            try
            {
                if (empleado is not null)
                {
                    resultado = 0;
                    mensaje = "Empleados recuperados correctamente.";
                }
                else
                {
                    resultado = 1;
                    mensaje = "No se encontraron empleados.";
                }
            }
            catch (Exception e)
            {
                resultado = -1;
                mensaje = $"Ocurrió un error inesperado: {e.Message}";
            }

            return (empleado, resultado, mensaje);
        }
    }
}

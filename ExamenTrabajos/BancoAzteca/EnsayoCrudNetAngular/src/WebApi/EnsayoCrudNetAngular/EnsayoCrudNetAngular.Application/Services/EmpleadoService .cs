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
            var lista = new List<Empleado>();

            var resultado = 0;
            var mensaje = "";

            try
            {
                lista = await _empleadoRepository.ObtenerListaEmpleadosAsync();

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
            var empleado = new Empleado();

            var resultado = 0;
            var mensaje = "";

            try
            {
                empleado = await _empleadoRepository.ObtenerEmpleadoPorIdAsync(idEmpleado);

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

        public async Task<(int idGenerado, int codigo, string mensaje)> InsertarEmpleadoAsync(Empleado empleado)
        {
            int codigo;
            string mensaje;
            int idGenerado;

            try
            {
                (idGenerado, mensaje) = await _empleadoRepository.InsertarEmpleadoAsync(empleado);

                if (idGenerado > 0)
                {
                    codigo = 0;
                    mensaje = "Empleado insertado correctamente.";
                }
                else
                {
                    codigo = 1;
                    mensaje = "No se pudo insertar el empleado.";
                }
            }
            catch (Exception ex)
            {
                codigo = -1;
                idGenerado = 0;
                mensaje = $"Error inesperado: {ex.Message}";
            }

            return (idGenerado, codigo, mensaje);
        }

        public async Task<(int codigo, string mensaje)> EditarEmpleadoAsync(Empleado empleado)
        {
            int codigo = 0;
            string mensaje = string.Empty;

            try
            {
                (codigo, mensaje) = await _empleadoRepository.EditarEmpleadoAsync(empleado);
            }
            catch (Exception ex)
            {
                codigo = -1;
                mensaje = $"Ocurrió un error inesperado: {ex.Message}";
            }

            return (codigo, mensaje);
        }

        public async Task<(int codigo, string mensaje, int idEliminado)> EliminarEmpleadoAsync(int idEmpleado)
        {
            var resultado = 0;
            var mensaje = "";
            var idEliminado = 0;

            try
            {
                (idEliminado, resultado, mensaje) = await _empleadoRepository.EliminarEmpleadoAsync(idEmpleado);

                if (resultado == 1)
                {
                    mensaje = "Empleado eliminado correctamente.";
                }
                else if (resultado == 0)
                {
                    mensaje = "Empleado no encontrado.";
                }
            }
            catch (Exception ex)
            {
                resultado = -1;
                mensaje = $"Error al eliminar el empleado: {ex.Message}";
            }

            return (resultado, mensaje, idEliminado);
        }
    }
}

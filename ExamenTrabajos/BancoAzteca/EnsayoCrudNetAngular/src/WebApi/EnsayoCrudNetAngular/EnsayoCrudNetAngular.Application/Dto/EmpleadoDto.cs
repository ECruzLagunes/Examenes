namespace EnsayoCrudNetAngular.Application.Dto
{
    public class EmpleadoDto
    {
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public int Sueldo { get; set; }
        public DateTime FechaContrato { get; set; }
    }
}

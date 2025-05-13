namespace CrudApi.Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }
    }
}

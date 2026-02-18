namespace EscuelaMusica.Api.Models;

public sealed class ProfesorCreateDto
{
    public string CodigoProfesor { get; init; } = default!;
    public string Nombre { get; init; } = default!;
    public string Apellido { get; init; } = default!;
    public int? IdEscuela { get; init; }
}

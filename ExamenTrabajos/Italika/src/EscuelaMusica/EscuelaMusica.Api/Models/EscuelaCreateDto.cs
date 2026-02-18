namespace EscuelaMusica.Api.Models;

public sealed class EscuelaCreateDto
{
    public string CodigoEscuela { get; init; } = default!;
    public string Nombre { get; init; } = default!;
    public string? Descripcion { get; init; }
}

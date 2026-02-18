namespace EscuelaMusica.Api.Models;

public sealed class EscuelaUpdateDto
{
    public int IdEscuela { get; init; }
    public string CodigoEscuela { get; init; } = default!;
    public string Nombre { get; init; } = default!;
    public string? Descripcion { get; init; }
}

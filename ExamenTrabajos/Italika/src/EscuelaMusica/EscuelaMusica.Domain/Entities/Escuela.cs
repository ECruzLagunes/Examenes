namespace EscuelaMusica.Domain.Entities;

public record Escuela
{
    public int IdEscuela { get; init; }
    public string CodigoEscuela { get; init; } = default!;
    public string Nombre { get; init; } = default!;
    public string? Descripcion { get; init; }
}
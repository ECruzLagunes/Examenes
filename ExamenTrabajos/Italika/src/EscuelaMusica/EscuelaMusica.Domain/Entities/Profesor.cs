namespace EscuelaMusica.Domain.Entities;

public record Profesor
{
    public int IdProfesor { get; init; }
    public string CodigoProfesor { get; init; } = default!;
    public string Nombre { get; init; } = default!;
    public string Apellido { get; init; } = default!;
    public int? IdEscuela { get; init; }
}
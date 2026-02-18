using System;

namespace EscuelaMusica.Api.Models;

public sealed class AlumnoCreateDto
{
    public string CodigoAlumno { get; init; } = default!;
    public string Nombre { get; init; } = default!;
    public string Apellido { get; init; } = default!;
    public DateOnly FechaNacimiento { get; init; }
    public int? IdEscuela { get; init; }
}

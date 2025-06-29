using EscuelaMusica.Domain.Entities;

internal static class TestData
{
    internal static Escuela NewEscuela(int id = 1) => new()
    {
        IdEscuela = id,
        CodigoEscuela = $"ES{id:D3}",
        Nombre = $"Escuela {id}",
        Descripcion = "Mock"
    };

    internal static Profesor NewProfesor(int id = 10, int? esc = 1) => new()
    {
        IdProfesor = id,
        CodigoProfesor = $"PR{id:D3}",
        Nombre = "Nombre",
        Apellido = "Apellido",
        IdEscuela = esc
    };

    internal static Alumno NewAlumno(int id = 100, int? esc = 1) => new()
    {
        IdAlumno = id,
        CodigoAlumno = $"AL{id:D3}",
        Nombre = "Alumno",
        Apellido = "Apellido",
        FechaNacimiento = DateOnly.FromDateTime(DateTime.Today.AddYears(-10)),
        IdEscuela = esc
    };
}
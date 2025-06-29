using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Entities;

namespace EscuelaMusica.Domain.Contracts;

public interface IAsignacionRepository
{
    Task<OperationResult> AsignarAsync(int idAlumno, int idProfesor, CancellationToken ct);
    Task<OperationResult> DesasignarAsync(int idAlumno, int idProfesor, CancellationToken ct);

    Task<IReadOnlyList<Alumno>> AlumnosPorProfesorAsync(int idProfesor, CancellationToken ct);
    Task<IReadOnlyList<Profesor>> ProfesoresPorAlumnoAsync(int idAlumno, CancellationToken ct);
}
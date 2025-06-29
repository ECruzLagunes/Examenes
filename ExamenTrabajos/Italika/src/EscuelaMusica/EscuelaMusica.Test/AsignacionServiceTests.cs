using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using Moq;

namespace EscuelaMusica.Application.Tests.Services;

public class AsignacionServiceTests
{
    private readonly Mock<IAsignacionRepository> _repo = new();
    private readonly IAsignacionService _svc;

    public AsignacionServiceTests() => _svc = new AsignacionService(_repo.Object);

    [Fact]
    public async Task AsignarAsync_Ok()
    {
        _repo.Setup(r => r.AsignarAsync(100, 10, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new OperationResult(0, "OK", 100));

        Assert.Equal(0, (await _svc.AsignarAsync(100, 10, default)).Codigo);
    }

    [Fact]
    public async Task DesasignarAsync_Ok()
    {
        _repo.Setup(r => r.DesasignarAsync(100, 10, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new OperationResult(0, "OK", 100));

        Assert.Equal(0, (await _svc.DesasignarAsync(100, 10, default)).Codigo);
    }

    [Fact]
    public async Task AlumnosPorProfesor_Count2()
    {
        _repo.Setup(r => r.AlumnosPorProfesorAsync(10, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new[]
             {
                 TestData.NewAlumno(),
                 TestData.NewAlumno(101)
             });

        Assert.Equal(2, (await _svc.AlumnosPorProfesorAsync(10, default)).Count);
    }

    [Fact]
    public async Task ProfesoresPorAlumno_Count1()
    {
        _repo.Setup(r => r.ProfesoresPorAlumnoAsync(100, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new[] { TestData.NewProfesor() });

        Assert.Single(await _svc.ProfesoresPorAlumnoAsync(100, default));
    }
}
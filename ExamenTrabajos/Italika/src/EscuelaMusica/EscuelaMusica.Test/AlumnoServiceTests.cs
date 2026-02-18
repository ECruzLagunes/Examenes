using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using Moq;
using Microsoft.Extensions.Logging;
using EscuelaMusica.Application.Interface;

namespace EscuelaMusica.Application.Tests.Services;

public class AlumnoServiceTests
{
    private readonly Mock<IAlumnoRepository> _repo = new();
    private readonly IAlumnoService _svc;
    private readonly Mock<ILogger<AlumnoService>> _logger = new();

    public AlumnoServiceTests() => _svc = new AlumnoService(_repo.Object, _logger.Object);

    [Fact]
    public async Task CrearAsync_Ok()
    {
        var alumno = TestData.NewAlumno(100);
        _repo.Setup(r => r.InsertarAsync(alumno, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new OperationResult(0, "OK", 100));

        var res = await _svc.CrearAsync(alumno, default);

        Assert.Equal(0, res.Codigo);
        Assert.Equal(100, res.Id);
    }

    [Fact]
    public async Task EditarAsync_Ok()
    {
        var alumno = TestData.NewAlumno();
        _repo.Setup(r => r.ActualizarAsync(alumno, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new OperationResult(0, "OK", alumno.IdAlumno));

        var res = await _svc.EditarAsync(alumno, default);

        Assert.Equal(0, res.Codigo);
    }

    [Fact]
    public async Task EliminarAsync_Ok()
    {
        _repo.Setup(r => r.EliminarAsync(100, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new OperationResult(0, "OK", 100));

        var res = await _svc.EliminarAsync(100, default);

        Assert.Equal(0, res.Codigo);
    }

    [Fact]
    public async Task ListarAsync_Count1()
    {
        _repo.Setup(r => r.ObtenerTodasAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(new[] { TestData.NewAlumno() });
        Assert.Single(await _svc.ListarAsync(default));
    }

    [Fact]
    public async Task ObtenerAsync_ReturnsEntity()
    {
        _repo.Setup(r => r.ObtenerPorIdAsync(100, It.IsAny<CancellationToken>()))
             .ReturnsAsync(TestData.NewAlumno());
        Assert.NotNull(await _svc.ObtenerAsync(100, default));
    }
}
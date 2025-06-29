using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using Moq;
using EscuelaMusica.Application.Interface;

namespace EscuelaMusica.Application.Tests.Services;

public class AlumnoServiceTests
{
    private readonly Mock<IAlumnoRepository> _repo = new();
    private readonly IAlumnoService _svc;

    public AlumnoServiceTests() => _svc = new AlumnoService(_repo.Object);

    //[Fact]
    //public async Task CrearAsync_Ok()
    //{
    //    var alumno = TestData.NewAlumno(100);

    //    Assert.Equal(0, (await _svc.CrearAsync(
    //        alumno, default)).Codigo);
    //}

    //[Fact]
    //public async Task EditarAsync_Ok() =>
    //    Assert.Equal(0, (await _svc.EditarAsync(
    //        TestData.NewAlumno(), default)).Codigo);

    //[Fact]
    //public async Task EliminarAsync_Ok() =>
    //    Assert.Equal(0, (await _svc.EliminarAsync(100, default)).Codigo);

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
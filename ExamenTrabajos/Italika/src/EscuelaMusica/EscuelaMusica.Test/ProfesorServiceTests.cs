using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Application.Interface;
using Moq;

namespace EscuelaMusica.Application.Tests.Services;

public class ProfesorServiceTests
{
    private readonly Mock<IProfesorRepository> _repo = new();
    private readonly IProfesorService _svc;

    public ProfesorServiceTests() => _svc = new ProfesorService(_repo.Object);

    //[Fact]
    //public async Task CrearAsync_Ok() =>
    //    Assert.Equal(0, (await _svc.CrearAsync(
    //        TestData.NewProfesor(0), default)).Codigo);

    //[Fact]
    //public async Task EditarAsync_Ok() =>
    //    Assert.Equal(0, (await _svc.EditarAsync(
    //        TestData.NewProfesor(), default)).Codigo);

    //[Fact]
    //public async Task EliminarAsync_Ok() =>
    //    Assert.Equal(0, (await _svc.EliminarAsync(10, default)).Codigo);

    [Fact]
    public async Task ListarAsync_Count1()
    {
        _repo.Setup(r => r.ObtenerTodasAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(new[] { TestData.NewProfesor() });
        Assert.Single(await _svc.ListarAsync(default));
    }

    [Fact]
    public async Task ObtenerAsync_ReturnsEntity()
    {
        _repo.Setup(r => r.ObtenerPorIdAsync(10, It.IsAny<CancellationToken>()))
             .ReturnsAsync(TestData.NewProfesor());
        Assert.NotNull(await _svc.ObtenerAsync(10, default));
    }
}
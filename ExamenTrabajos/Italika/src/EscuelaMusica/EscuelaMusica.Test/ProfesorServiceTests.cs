using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Contracts;
using EscuelaMusica.Application.Interface;
using Moq;
using Microsoft.Extensions.Logging;

namespace EscuelaMusica.Application.Tests.Services;

public class ProfesorServiceTests
{
    private readonly Mock<IProfesorRepository> _repo = new();
    private readonly Mock<ILogger<ProfesorService>> _logger = new();
    private readonly IProfesorService _svc;

    public ProfesorServiceTests() => _svc = new ProfesorService(_repo.Object, _logger.Object);

    [Fact]
    public async Task CrearAsync_Ok()
    {
        var p = TestData.NewProfesor(0);
        _repo.Setup(r => r.InsertarAsync(p, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new EscuelaMusica.Domain.Common.OperationResult(0, "OK", p.IdProfesor));

        Assert.Equal(0, (await _svc.CrearAsync(p, default)).Codigo);
    }

    [Fact]
    public async Task EditarAsync_Ok()
    {
        var p = TestData.NewProfesor();
        _repo.Setup(r => r.ActualizarAsync(p, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new EscuelaMusica.Domain.Common.OperationResult(0, "OK", p.IdProfesor));

        Assert.Equal(0, (await _svc.EditarAsync(p, default)).Codigo);
    }

    [Fact]
    public async Task EliminarAsync_Ok()
    {
        _repo.Setup(r => r.EliminarAsync(10, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new EscuelaMusica.Domain.Common.OperationResult(0, "OK", 10));

        Assert.Equal(0, (await _svc.EliminarAsync(10, default)).Codigo);
    }

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
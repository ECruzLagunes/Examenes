using EscuelaMusica.Application.Services;
using EscuelaMusica.Domain.Common;
using EscuelaMusica.Domain.Contracts;
using Moq;
using EscuelaMusica.Application.Interface;

namespace EscuelaMusica.Application.Tests.Services;

public class EscuelaServiceTests
{
    private readonly Mock<IEscuelaRepository> _repo = new();
    private readonly IEscuelaService _svc;

    public EscuelaServiceTests() => _svc = new EscuelaService(_repo.Object);

    //[Fact]
    //public async Task CrearAsync_InsertSuccess_ReturnsCodigoCero()
    //{
    //    var esc = TestData.NewEscuela(0);
    //    _repo.Setup(r => r.InsertarAsync(esc, It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(new OperationResult(0, "OK", 7));

    //    var res = await _svc.CrearAsync(esc, default);

    //    Assert.Equal(0, res.Codigo);
    //    Assert.Equal(7, res.Id);
    //}

    //[Fact]
    //public async Task EditarAsync_UpdateNotFound_ReturnsCodigoUno()
    //{
    //    var esc = TestData.NewEscuela();
    //    _repo.Setup(r => r.ActualizarAsync(esc, It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(new OperationResult(1, "No existe", null));

    //    var res = await _svc.EditarAsync(esc, default);

    //    Assert.Equal(1, res.Codigo);
    //    Assert.Null(res.Id);
    //}

    //[Fact]
    //public async Task EliminarAsync_ReturnsOk()
    //{
    //    _repo.Setup(r => r.EliminarAsync(1, It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(new OperationResult(0, "Eliminado", 1));

    //    var res = await _svc.EliminarAsync(1, default);

    //    Assert.Equal(0, res.Codigo);
    //}

    [Fact]
    public async Task ListarAsync_ReturnsCollection()
    {
        _repo.Setup(r => r.ObtenerTodasAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(new[] { TestData.NewEscuela() });

        var list = await _svc.ListarAsync(default);

        Assert.Single(list);
    }

    [Fact]
    public async Task ObtenerAsync_ReturnsEntity()
    {
        _repo.Setup(r => r.ObtenerPorIdAsync(1, It.IsAny<CancellationToken>()))
             .ReturnsAsync(TestData.NewEscuela());

        var esc = await _svc.ObtenerAsync(1, default);

        Assert.NotNull(esc);
        Assert.Equal(1, esc!.IdEscuela);
    }
}
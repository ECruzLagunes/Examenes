using Microsoft.AspNetCore.Mvc;
using EscuelaMusica.Domain.Common;

namespace EscuelaMusica.Api.Utils;

public static class ApiResponse
{
    public static IActionResult FromResult(OperationResult r, object? data = null) =>
        new OkObjectResult(new { codigo = r.Codigo, mensaje = r.Mensaje, id = r.Id, data });

    public static IActionResult FromData(object? data) =>
        new OkObjectResult(new { codigo = 0, mensaje = "OK", data });

    public static IActionResult FromException(Exception ex) =>
        new OkObjectResult(new
        {
            codigo = -1,
            mensaje = "Excepción no controlada",
            detalle = ex.Message,
            stackTrace = ex.StackTrace
        });
}
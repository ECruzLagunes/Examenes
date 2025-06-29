namespace EscuelaMusica.Domain.Common
{
    /// <summary> Resultado estandarizado para los Stored Procedures: (Codigo, Mensaje, Id) </summary>
    public sealed record OperationResult(int Codigo, string Mensaje, int? Id);
}

CREATE PROCEDURE Usp_EliminaUsuario
    @IdUsuario INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE FROM UsuariosPokemones WHERE IdUsuario = @IdUsuario;
        DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario;

        SET @Resultado = 1;
        SET @Mensaje = 'Usuario eliminado correctamente.';

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SET @Resultado = -1;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
CREATE PROCEDURE Usp_ValidarUsuario
    @NombreUsuario VARCHAR(50),
    @Contrase�a VARCHAR(128),
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT,
    @IdUsuario INT OUTPUT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (
            SELECT 1 FROM Usuarios
            WHERE NombreUsuario = @NombreUsuario
            AND ContrasenaHash = @Contrase�a
            AND Activo = 1
        )
        BEGIN
            SELECT @IdUsuario = IdUsuario FROM Usuarios WHERE NombreUsuario = @NombreUsuario;
            SET @Resultado = 1;
            SET @Mensaje = 'Usuario v�lido.';
        END
        ELSE
        BEGIN
			SET @IdUsuario = 0;
            SET @Resultado = 0;
            SET @Mensaje = 'Credenciales inv�lidas.';
        END
    END TRY
    BEGIN CATCH
        SET @Resultado = -1;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
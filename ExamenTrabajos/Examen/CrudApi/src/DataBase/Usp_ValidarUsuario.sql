CREATE PROCEDURE Usp_ValidarUsuario
    @NombreUsuario VARCHAR(50),
    @Contraseña VARCHAR(128),
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT,
    @IdUsuario INT OUTPUT
AS
BEGIN
    BEGIN TRY
        IF EXISTS (
            SELECT 1 FROM Usuarios
            WHERE NombreUsuario = @NombreUsuario
            AND ContrasenaHash = @Contraseña
            AND Activo = 1
        )
        BEGIN
            SELECT @IdUsuario = IdUsuario FROM Usuarios WHERE NombreUsuario = @NombreUsuario;
            SET @Resultado = 1;
            SET @Mensaje = 'Usuario válido.';
        END
        ELSE
        BEGIN
			SET @IdUsuario = 0;
            SET @Resultado = 0;
            SET @Mensaje = 'Credenciales inválidas.';
        END
    END TRY
    BEGIN CATCH
        SET @Resultado = -1;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
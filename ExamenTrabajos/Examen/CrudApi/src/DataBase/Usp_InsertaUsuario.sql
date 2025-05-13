CREATE PROCEDURE Usp_InsertaUsuario
    @NombreUsuario VARCHAR(50),
    @Contraseña VARCHAR(128),
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Usuarios WHERE NombreUsuario = @NombreUsuario)
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'El nombre de usuario ya existe.';
            RETURN;
        END

        INSERT INTO Usuarios (NombreUsuario, ContrasenaHash)
        VALUES (@NombreUsuario, @Contraseña);

        SET @Resultado = 1;
        SET @Mensaje = 'Usuario insertado correctamente.';

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SET @Resultado = -1;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
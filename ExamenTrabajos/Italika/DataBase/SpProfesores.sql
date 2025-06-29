-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 28/06/25
-- Description:	StoredProcedure dedicado a Profesores con las operaciones de un crud
-- Accion:
-- I: Insert
-- U: Update
-- D: Delete
-- R: GetAll
-- B: GetBy
-- =============================================
CREATE OR ALTER PROCEDURE dbo.SpProfesores
    @Accion          CHAR(1),
    @IdProfesor      INT            = NULL,
    @CodigoProfesor  CHAR(8)        = NULL,
    @Nombre          NVARCHAR(60)   = NULL,
    @Apellido        NVARCHAR(60)   = NULL,
    @IdEscuela       INT            = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Codigo INT = -1, @Mensaje NVARCHAR(200) = N'', @Id INT = NULL;

    BEGIN TRY
        IF @Accion IN ('I','U','D') BEGIN TRAN;

        IF @Accion = 'I'
        BEGIN
            INSERT INTO dbo.Profesores (CodigoProfesor, Nombre, Apellido, IdEscuela)
            VALUES (@CodigoProfesor, @Nombre, @Apellido, @IdEscuela);

            SELECT @Id = SCOPE_IDENTITY();
            SELECT @Codigo = 0, @Mensaje = N'Profesor insertado correctamente.';
        END
        ELSE IF @Accion = 'U'
        BEGIN
            UPDATE dbo.Profesores
               SET CodigoProfesor = COALESCE(@CodigoProfesor, CodigoProfesor),
                   Nombre         = COALESCE(@Nombre,         Nombre),
                   Apellido       = COALESCE(@Apellido,       Apellido),
                   IdEscuela      = COALESCE(@IdEscuela,      IdEscuela)
             WHERE IdProfesor = @IdProfesor;

            IF @@ROWCOUNT = 0
                SELECT @Codigo = 1, @Mensaje = N'No se encontró el profesor a actualizar.';
            ELSE
                SELECT @Codigo = 0, @Mensaje = N'Profesor actualizado correctamente.', @Id = @IdProfesor;
        END
        ELSE IF @Accion = 'D'
        BEGIN
            DELETE FROM dbo.Profesores WHERE IdProfesor = @IdProfesor;

            IF @@ROWCOUNT = 0
                SELECT @Codigo = 1, @Mensaje = N'No se encontró el profesor a eliminar.';
            ELSE
                SELECT @Codigo = 0, @Mensaje = N'Profesor eliminado correctamente.', @Id = @IdProfesor;
        END
        ELSE IF @Accion = 'R'
        BEGIN
            SELECT * FROM dbo.Profesores ORDER BY Apellido, Nombre;
            RETURN;
        END
        ELSE IF @Accion = 'B'
        BEGIN
            SELECT * FROM dbo.Profesores WHERE IdProfesor = @IdProfesor;
            RETURN;
        END

        IF @Accion IN ('I','U','D') COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRAN;
        SELECT @Codigo = ERROR_NUMBER(), @Mensaje = ERROR_MESSAGE();
    END CATCH

    SELECT @Codigo AS Codigo, @Mensaje AS Mensaje, @Id AS Id;
END
GO

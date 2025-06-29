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
CREATE OR ALTER PROCEDURE dbo.SpAlumnos
    @Accion          CHAR(1),
    @IdAlumno        INT            = NULL,
    @CodigoAlumno    CHAR(10)       = NULL,
    @Nombre          NVARCHAR(60)   = NULL,
    @Apellido        NVARCHAR(60)   = NULL,
    @FechaNacimiento DATE           = NULL,
    @IdEscuela       INT            = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Codigo INT = -1, @Mensaje NVARCHAR(200) = N'', @Id INT = NULL;

    BEGIN TRY
        IF @Accion IN ('I','U','D') BEGIN TRAN;

        IF @Accion = 'I'
        BEGIN
            INSERT INTO dbo.Alumnos (CodigoAlumno, Nombre, Apellido, FechaNacimiento, IdEscuela)
            VALUES (@CodigoAlumno, @Nombre, @Apellido, @FechaNacimiento, @IdEscuela);

            SELECT @Id = SCOPE_IDENTITY();
            SELECT @Codigo = 0, @Mensaje = N'Alumno insertado correctamente.';
        END
        ELSE IF @Accion = 'U'
        BEGIN
            UPDATE dbo.Alumnos
               SET CodigoAlumno    = COALESCE(@CodigoAlumno,    CodigoAlumno),
                   Nombre          = COALESCE(@Nombre,          Nombre),
                   Apellido        = COALESCE(@Apellido,        Apellido),
                   FechaNacimiento = COALESCE(@FechaNacimiento, FechaNacimiento),
                   IdEscuela       = COALESCE(@IdEscuela,       IdEscuela)
             WHERE IdAlumno = @IdAlumno;

            IF @@ROWCOUNT = 0
                SELECT @Codigo = 1, @Mensaje = N'No se encontro el alumno a actualizar.';
            ELSE
                SELECT @Codigo = 0, @Mensaje = N'Alumno actualizado correctamente.', @Id = @IdAlumno;
        END
        ELSE IF @Accion = 'D'
        BEGIN
            DELETE FROM dbo.Alumnos WHERE IdAlumno = @IdAlumno;

            IF @@ROWCOUNT = 0
                SELECT @Codigo = 1, @Mensaje = N'No se encontro el alumno a eliminar.';
            ELSE
                SELECT @Codigo = 0, @Mensaje = N'Alumno eliminado correctamente.', @Id = @IdAlumno;
        END
        ELSE IF @Accion = 'R'
        BEGIN
            SELECT * FROM dbo.Alumnos ORDER BY Apellido, Nombre;
            RETURN;
        END
        ELSE IF @Accion = 'B'
        BEGIN
            SELECT * FROM dbo.Alumnos WHERE IdAlumno = @IdAlumno;
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
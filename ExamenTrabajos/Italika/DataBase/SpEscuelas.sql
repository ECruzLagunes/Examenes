-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 28/06/25
-- Description:	StoredProcedure dedicado a Escuelas con las operaciones de un crud
-- Accion:
-- I: Insert
-- U: Update
-- D: Delete
-- R: GetAll
-- B: GetBy
-- =============================================
CREATE OR ALTER PROCEDURE dbo.SpEscuelas
    @Accion        CHAR(1),           
    @IdEscuela     INT            = NULL,
    @CodigoEscuela CHAR(6)        = NULL,
    @Nombre        NVARCHAR(120)  = NULL,
    @Descripcion   NVARCHAR(400)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Codigo INT = -1, @Mensaje NVARCHAR(200) = N'', @Id INT = NULL;

    BEGIN TRY
        IF @Accion IN ('I','U','D') BEGIN TRAN;

        /* INSERTAR */
        IF @Accion = 'I'
        BEGIN
            INSERT INTO dbo.Escuelas (CodigoEscuela, Nombre, Descripcion)
            VALUES (@CodigoEscuela, @Nombre, @Descripcion);

            SELECT @Id = SCOPE_IDENTITY();
            SELECT @Codigo = 0, @Mensaje = N'Escuela insertada correctamente.';
        END
        /* ACTUALIZAR */
        ELSE IF @Accion = 'U'
        BEGIN
            UPDATE dbo.Escuelas
               SET CodigoEscuela = COALESCE(@CodigoEscuela, CodigoEscuela),
                   Nombre        = COALESCE(@Nombre,        Nombre),
                   Descripcion   = COALESCE(@Descripcion,   Descripcion)
             WHERE IdEscuela = @IdEscuela;

            IF @@ROWCOUNT = 0
                SELECT @Codigo = 1, @Mensaje = N'No se encontro la escuela a actualizar.';
            ELSE
                SELECT @Codigo = 0, @Mensaje = N'Escuela actualizada correctamente.', @Id = @IdEscuela;
        END
        /* ELIMINAR */
        ELSE IF @Accion = 'D'
        BEGIN
            DELETE FROM dbo.Escuelas WHERE IdEscuela = @IdEscuela;

            IF @@ROWCOUNT = 0
                SELECT @Codigo = 1, @Mensaje = N'No se encontro la escuela a eliminar.';
            ELSE
                SELECT @Codigo = 0, @Mensaje = N'Escuela eliminada correctamente.', @Id = @IdEscuela;
        END
        /* LEER TODOS */
        ELSE IF @Accion = 'R'
        BEGIN
            SELECT * FROM dbo.Escuelas ORDER BY Nombre;
            RETURN;
        END
        /* BUSCAR POR ID */
        ELSE IF @Accion = 'B'
        BEGIN
            SELECT * FROM dbo.Escuelas WHERE IdEscuela = @IdEscuela;
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
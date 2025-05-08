-- =============================================
-- Author:      Esteban Cruz Lagunes
-- Create date: 
-- Description: Elimina un empleado por ID
-- =============================================

-- Elimina el SP si ya existe
IF OBJECT_ID('Usp_EliminaEmpleado', 'P') IS NOT NULL
    DROP PROCEDURE Usp_EliminaEmpleado;
GO

-- Crea el SP
CREATE PROCEDURE Usp_EliminaEmpleado
    @IdEmpleado             INT,
    @Resultado              INT OUTPUT,
    @Mensaje                NVARCHAR(500) OUTPUT,
    @IdEmpleadoEliminado    INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar existencia del empleado
        IF NOT EXISTS (SELECT 1 FROM Empleados WHERE IdEmpleado = @IdEmpleado)
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'El empleado no existe.';
            SET @IdEmpleadoEliminado = 0;
            RETURN;
        END

        -- Eliminar al empleado
        DELETE FROM Empleados
        WHERE IdEmpleado = @IdEmpleado;

        COMMIT TRANSACTION;

        SET @Resultado = 1;
        SET @Mensaje = 'Empleado eliminado correctamente.';
        SET @IdEmpleadoEliminado = @IdEmpleado;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        SET @Resultado = -1;
        SET @Mensaje = ERROR_MESSAGE();
        SET @IdEmpleadoEliminado = 0;
    END CATCH
END
GO

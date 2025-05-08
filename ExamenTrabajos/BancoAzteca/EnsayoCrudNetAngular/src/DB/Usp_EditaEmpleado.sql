-- =============================================
-- Author:      Esteban Cruz Lagunes
-- Create date: 
-- Description: Edita los datos de un empleado por su ID
-- =============================================

IF OBJECT_ID('Usp_EditaEmpleado', 'P') IS NOT NULL
    DROP PROCEDURE Usp_EditaEmpleado;
GO

CREATE PROCEDURE Usp_EditaEmpleado
    @IdEmpleado         INT,
    @NombreCompleto     VARCHAR(50),
    @Correo             VARCHAR(50),
    @Sueldo             INT,
    @FechaContrato      DATE,
	@Resultado              INT OUTPUT,
    @Mensaje            NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Empleados WHERE IdEmpleado = @IdEmpleado)
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'No se encontró el empleado con el ID proporcionado.';
            
            RETURN;
        END
		
        UPDATE Empleados
        SET 
            NombreCompleto = @NombreCompleto,
            Correo = @Correo,
            Sueldo = @Sueldo,
            FechaContrato = @FechaContrato
        WHERE 
			IdEmpleado = @IdEmpleado;

        COMMIT TRANSACTION;

        SET @Resultado = 1;
        SET @Mensaje = 'Empleado actualizado correctamente.';

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        SET @Resultado = -1;
        SET @Mensaje = ERROR_MESSAGE();

    END CATCH
END
GO

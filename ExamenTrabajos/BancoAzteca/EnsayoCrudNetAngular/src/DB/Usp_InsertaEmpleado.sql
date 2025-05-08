-- =============================================
-- Author:      Esteban Cruz Lagunes
-- Create date: 04/05/2025
-- Description: Inserta un nuevo empleado y retorna su ID
-- =============================================

IF OBJECT_ID('Usp_InsertaEmpleado', 'P') IS NOT NULL
	DROP PROCEDURE Usp_InsertaEmpleado;
GO

CREATE PROCEDURE Usp_InsertaEmpleado 
	@NombreCompleto   VARCHAR(50),
	@Correo           VARCHAR(50),
	@Sueldo           INT,
	@FechaContrato    DATE,
	@IdGenerado       INT OUTPUT,
	@Mensaje          NVARCHAR(500) OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		INSERT INTO Empleados (
			  NombreCompleto
			, Correo
			, Sueldo
			, FechaContrato
			)
		VALUES (
			  @NombreCompleto
			, @Correo
			, @Sueldo
			, @FechaContrato
			);

		SET @IdGenerado = SCOPE_IDENTITY();

		SET @Mensaje = 'Empleado insertado correctamente.';

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;

		SET @IdGenerado = 0;
		SET @Mensaje = ERROR_MESSAGE();
	END CATCH
END
GO

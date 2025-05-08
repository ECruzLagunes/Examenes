-- =============================================
-- Author:      Esteban Cruz Lagunes
-- Create date: 04/05/2025
-- Description: Obtiene los datos de un empleado por su Id
-- =============================================

IF OBJECT_ID('Usp_ObtenerEmpleadoPorId', 'P') IS NOT NULL
	DROP PROCEDURE Usp_ObtenerEmpleadoPorId;
GO

CREATE PROCEDURE Usp_ObtenerEmpleadoPorId
	@IdEmpleado INT
AS
BEGIN	
	SELECT	
		  IdEmpleado		
		, NombreCompleto	
		, Correo			
		, Sueldo			
		, FechaContrato	
	FROM 
		Empleado
	WHERE 
		IdEmpleado = @IdEmpleado;	
END
GO

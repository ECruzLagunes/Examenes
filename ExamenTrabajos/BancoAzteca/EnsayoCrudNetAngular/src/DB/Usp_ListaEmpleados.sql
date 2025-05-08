-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 04/05/2025
-- Description:	Obtiene un Listado de Empleados
-- =============================================
CREATE PROCEDURE Usp_ListaEmpleados 
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
END
GO

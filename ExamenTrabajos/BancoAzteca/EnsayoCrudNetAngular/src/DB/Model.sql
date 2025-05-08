--Data Definition Lenguage DDL
Create Table Empleado(
	IdEmpleado		INT PRIMARY KEY IDENTITY,
	NombreCompleto	VARCHAR,
	Correo			VARCHAR(50),
	Sueldo			DECIMAL(10,2),
	FechaContrato	DATE
)

GO

--Data Manipulation Leguage DML

INSERT INTO Empleado (NombreCompleto, Correo, Sueldo, FechaContrato) VALUES ('Maria Mendez', 'Maria@gmail.com', 4500, '2024-01-12')

--Data Retrieval Leanguage DRL

SELECT * FROM Empleado
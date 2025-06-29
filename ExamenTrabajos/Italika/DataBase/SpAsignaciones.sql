-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 28/06/25
-- Description:	StoredProcedure dedicado a Profesores con las operaciones de un crud
-- Accion:
-- A = Asignar, D = Desasignar, P = AlumnosPorProfesor, F = ProfesoresPorAlumno
-- =============================================
CREATE OR ALTER PROCEDURE dbo.SpAsignaciones
    @Accion      CHAR(1),     
    @IdAlumno    INT  = NULL,
    @IdProfesor  INT  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Codigo INT = -1, @Mensaje NVARCHAR(200) = N'', @Id INT = NULL;

    BEGIN TRY
        IF @Accion = 'A'      
        BEGIN
            INSERT INTO dbo.AlumnosProfesores (IdAlumno, IdProfesor)
            VALUES (@IdAlumno, @IdProfesor);
            SELECT @Codigo = 0, @Mensaje = N'Asignacion creada.', @Id = @IdAlumno;
        END
        ELSE IF @Accion = 'D' 
        BEGIN
            DELETE FROM dbo.AlumnosProfesores
             WHERE IdAlumno = @IdAlumno AND IdProfesor = @IdProfesor;
            IF @@ROWCOUNT = 0
                 SELECT @Codigo = 1, @Mensaje = N'Asignacion no encontrada.';
            ELSE SELECT @Codigo = 0, @Mensaje = N'Asignacion eliminada.', @Id = @IdAlumno;
        END
        ELSE IF @Accion = 'P' 
        BEGIN
            SELECT  a.IdAlumno, a.Nombre, a.Apellido,
                    e.IdEscuela, e.Nombre AS Escuela
            FROM dbo.AlumnosProfesores ap
            JOIN dbo.Alumnos   a ON ap.IdAlumno   = a.IdAlumno
            JOIN dbo.Profesores p ON ap.IdProfesor = p.IdProfesor
            JOIN dbo.Escuelas   e ON p.IdEscuela  = e.IdEscuela
            WHERE p.IdProfesor = @IdProfesor
            ORDER BY a.Apellido, a.Nombre;
            RETURN;
        END
        ELSE IF @Accion = 'F' 
        BEGIN
            EXEC dbo.SP_Asignaciones_CRUD @Accion='P', @IdProfesor=@IdProfesor;
            RETURN;
        END
    END TRY
    BEGIN CATCH
        SELECT @Codigo = ERROR_NUMBER(), @Mensaje = ERROR_MESSAGE();
    END CATCH

    SELECT @Codigo AS Codigo, @Mensaje AS Mensaje, @Id AS Id;
END
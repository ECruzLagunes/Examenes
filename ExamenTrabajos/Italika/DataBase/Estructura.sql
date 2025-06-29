CREATE TABLE dbo.Escuelas (
    IdEscuela      INT           IDENTITY(1,1) PRIMARY KEY,
    CodigoEscuela  CHAR(6)       NOT NULL UNIQUE,
    Nombre         NVARCHAR(1000) NOT NULL,
    Descripcion    NVARCHAR(1000) NULL,
    FechaCreacion  DATETIME2     NOT NULL DEFAULT SYSDATETIME()
);

CREATE TABLE dbo.Profesores (
    IdProfesor      INT           IDENTITY(1,1) PRIMARY KEY,
    CodigoProfesor  CHAR(8)       NOT NULL UNIQUE,
    Nombre          NVARCHAR(1000)  NOT NULL,
    Apellido        NVARCHAR(1000)  NOT NULL,
    IdEscuela       INT           NULL,
    FechaCreacion   DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_Profesores_Escuelas
        FOREIGN KEY (IdEscuela) REFERENCES dbo.Escuelas(IdEscuela)
);

CREATE TABLE dbo.Alumnos (
    IdAlumno        INT           IDENTITY(1,1) PRIMARY KEY,
    CodigoAlumno    CHAR(10)      NOT NULL UNIQUE,
    Nombre          NVARCHAR(1000)  NOT NULL,
    Apellido        NVARCHAR(1000)  NOT NULL,
    FechaNacimiento DATE          NOT NULL,
    IdEscuela       INT           NULL,
    FechaCreacion   DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_Alumnos_Escuelas
        FOREIGN KEY (IdEscuela) REFERENCES dbo.Escuelas(IdEscuela)
);

CREATE TABLE dbo.AlumnosProfesores (
    IdAlumno        INT NOT NULL,
    IdProfesor      INT NOT NULL,
    FechaAsignacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT PK_AlumnosProfesores PRIMARY KEY (IdAlumno, IdProfesor),
    CONSTRAINT FK_AP_Alumno   FOREIGN KEY (IdAlumno)   REFERENCES dbo.Alumnos(IdAlumno),
    CONSTRAINT FK_AP_Profesor FOREIGN KEY (IdProfesor) REFERENCES dbo.Profesores(IdProfesor)
);
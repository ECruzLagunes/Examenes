INSERT INTO dbo.Escuelas (CodigoEscuela, Nombre, Descripcion) VALUES
('ES1001', N'Horizontes Armónicos',  N'Especializada en teoría de la armonía moderna.'),
('ES1002', N'Forja Rítmica',         N'Percusiones del mundo e improvisación rítmica.'),
('ES1003', N'Creadoras de Melodías', N'Programa integral de canto e instrumentos.'),
('ES1004', N'Semillas Sinfónicas',   N'Iniciación lúdica para niños.'),
('ES1005', N'Cruce del Jazz',        N'Taller permanente de jazz experimental.'),
('ES1006', N'Lienzo de Acordes',     N'Música contemporánea y composición.'),
('ES1007', N'Tesoro del Tempo',      N'Laboratorio de métricas complejas.'),
('ES1008', N'Enclave Eco',           N'Producción y sonido en vivo.'),
('ES1009', N'Santuario de Escalas',  N'Intensivo de técnicas avanzadas.'),
('ES1010', N'Granero BPM',           N'Groove, DJ y música electrónica.');

INSERT INTO dbo.Profesores (CodigoProfesor, Nombre, Apellido, IdEscuela) VALUES
('PR2001', N'Azul',      N'Montenegro', 1),
('PR2002', N'Santiago',  N'Olea',       2),
('PR2003', N'Renata',    N'Quintal',    3),
('PR2004', N'Gael',      N'Ríos',       4),
('PR2005', N'Inés',      N'Salvatierra',5),
('PR2006', N'Lauro',     N'Benítez',    1),
('PR2007', N'Magdalena', N'Velasco',    2),
('PR2008', N'Ulises',    N'Farfán',     6),
('PR2009', N'Lía',       N'Carrión',    7),
('PR2010', N'Iván',      N'Domenech',   8);

INSERT INTO dbo.Alumnos (CodigoAlumno, Nombre, Apellido, FechaNacimiento, IdEscuela) VALUES
('AL3001', N'Misael',   N'Gálvez',    '2010-05-14', 1),
('AL3002', N'Ariadna',  N'Solís',     '2009-11-20', 1),
('AL3003', N'Ismael',   N'Téllez',    '2008-04-01', 2),
('AL3004', N'Ilse',     N'Contreras', '2011-09-19', 2),
('AL3005', N'Rogelio',  N'Zúñiga',    '2007-12-02', 3),
('AL3006', N'Perla',    N'Bautista',  '2012-03-30', 4),
('AL3007', N'Jerónimo', N'Cuevas',    '2008-07-22', 5),
('AL3008', N'Paloma',   N'Valera',    '2009-10-11', 3),
('AL3009', N'Bruno',    N'Escobar',   '2011-06-05', 4),
('AL3010', N'Samira',   N'Lugo',      '2013-01-18', 5);

INSERT INTO dbo.AlumnosProfesores (IdAlumno, IdProfesor) VALUES
(1,1),(2,1),
(3,2),(4,2),
(5,3),(8,3),
(6,4),(9,4),
(7,5),(10,5),
(1,6),(3,6),
(4,7),(5,7),
(6,8),(7,8),(8,8),
(9,9),(10,9),
(2,10),(6,10);
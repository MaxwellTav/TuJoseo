--Crear la base de datos.
Create Database TuJoseoDB

--_________________________________________________________________________________________________________
--Usar la base de datos (Cada vez que inicies SQL Server hay que ejecutar esta l�nea).
Use TuJoseoDB

--_________________________________________________________________________________________________________
--Crear las tablas que se utilizar�n.
--######################################
--Usuario
Create Table UserTable
(UserID int Primary Key Identity,
UserName Varchar(30) Not Null Unique,
UserCompleteName Varchar(100) Not Null Unique,
UserPassword Varchar(30) Not Null,
UserEmail Varchar(100) Not Null Unique,

UserRememberMe int,

UserJoseosRealized int,
UserJobQuality int,
UserSimpaty int,
UserStalkers int,
UserRelevance int,
UserKnowlegde Varchar(300),
UserLastLogin int,

UserUnreadNotifications int,
UserUnreadNotificationsTime int,
UserUnreadMessages int,
UserUnreadMessagesTime int,
UserUnreadReports int,
UserUnreadReportsTime int,

UserEducation Varchar(500),
UserLocation Varchar(500),
UserHabilities Varchar(500),
UserNotes Varchar(500),
UserRol Varchar(100),
UserPhone Varchar(50),
UserPhoto Varchar(50)
);

--Ejecutar pruebas.
--Pueba de los usuarios
SELECT *
  FROM [TuJoseoDB].[dbo].[UserTable];
--_________________________________________________________________________________________________________

--Insertarle datos de prueba a la tabla.
Insert Into UserTable
values (
--UserName
'Admin',
--UserCompleteName
'Admin',
--UserPassword
'Admin',

--UserEmail
'admin@gmail.com',

--UserRememberMe
1,

--UserJoseosRealized
53,
--UserJobQuality
84,
--UserSimpaty
92,
--UserStalkers
650,
--UserRelevance (Deprecated) Relevancia de esta persona en la comunidad
1,
--UserKnowlegde
'He hecho proyectos para grandes empresas... Solo soy un programador que est� buscando nuevos desaf�os para superar.',
--UserLastLogin (En d�as), ha iniciado sesi�n hace 3 d�as.
3,

--UserUnreadNotifications
5,
--UserUnreadNotificationsTime (En d�as) (tiempo del �ltimo mensaje enviado)
2,
--UserUnreadMessages
5,
--UserUnreadMessagesTime (En d�as) (tiempo del �ltimo mensaje enviado)
3,
--UserUnreadReports
4,
--UserUnreadReportsTime (En d�as) (tiempo del �ltimo mensaje enviado)
1,

--UserEducation
'Estudiante graduado del ITLA, Liceo Parroquial San Pablo Apostol e Infotep',
--UserLocation
'Rep�blica Dominicana',
--UserHabilities
'C#, Asp .Net Core, SQL, NonSQL, MongoDB, Arquitectura MVC, Full Stack, 5 a�os de experiencia.',
--UserNotes
'Soporte t�cnico, programador y dominio del idioma ingl�s B1.',
--UserRol
'Programador y soporte t�cnico',
--Tel�fono
'8296820160',
--Foto
'D:\Max\Desktop\Foto.jpg'
);

--_________________________________________________________________________________________________________
--Notas
Create Table NotesTable
(NoteID int Primary Key Identity,
NoteUserID int,
NoteDescription Varchar(Max),
NoteTime DATETIME DEFAULT GETDATE(),
NoteDone Bit Default 0)

INSERT INTO NotesTable (NoteUserID, NoteDescription, NoteTime, NoteDone)
VALUES (1, 'Tarea hecha', GETDATE(), 0);

--Prueba de la tabla de notas.
Select * From NotesTable Where NoteUserID = 1;

--_________________________________________________________________________________________________________

--Joseos
Create Table JoseosTable
(JoseoID int Primary Key Identity,
JoseoTitle Varchar(Max) Not Null,
JoseoDescription Varchar(Max) Not Null,
JoseoPrice Varchar(10) Not Null,
JoseadorID Varchar(10) Not Null,
JoseoStartTime DATETIME DEFAULT GETDATE() Not Null,
JoseoEstimatedTime DateTime Not Null,
JoseoFinishTime DateTime,
JoseoContratoID Varchar(10),
JoseoStatus Varchar(50) Not Null,
JoseadorRealID Varchar(10));

-- Ejemplo 1
INSERT INTO JoseosTable (JoseoTitle, JoseoDescription, JoseoPrice, JoseadorID, JoseoStartTime, JoseoEstimatedTime, JoseoFinishTime, JoseoContratoID, JoseoStatus)
VALUES ('Desarrollador Full Stack para Plataforma Educativa Online', 'Buscamos un desarrollador Full Stack apasionado por la educaci�n online...', '55000', '1', GETDATE(), DATEADD(MONTH, 3, GETDATE()), '2023-07-01', 'C001', 'Abierto');

-- Ejemplo 2
INSERT INTO JoseosTable (JoseoTitle, JoseoDescription, JoseoPrice, JoseadorID, JoseoStartTime, JoseoEstimatedTime, JoseoFinishTime, JoseoContratoID, JoseoStatus)
VALUES ('Ingeniero de Datos para Proyecto de An�lisis de Big Data en Ciencias Biom�dicas', '�nete a nuestro equipo como ingeniero de datos para un proyecto innovador en el an�lisis de Big Data en el campo de las ciencias biom�dicas...', '70000', '1', GETDATE(), DATEADD(MONTH, 1, GETDATE()), '2023-09-01', 'C002', 'Abierto');

-- Ejemplo 3
INSERT INTO JoseosTable (JoseoTitle, JoseoDescription, JoseoPrice, JoseadorID, JoseoStartTime, JoseoEstimatedTime, JoseoFinishTime, JoseoContratoID, JoseoStatus)
VALUES ('Desarrollador de Aplicaciones M�viles para Proyecto de Salud Mental', 'Estamos en busca de un desarrollador m�vil experto para contribuir a un proyecto centrado en la salud mental...', '65000', '1', GETDATE(), DATEADD(MONTH, 1, GETDATE()), '2023-08-01', 'C003', 'Abierto');

-- Ejemplo 4
INSERT INTO JoseosTable (JoseoTitle, JoseoDescription, JoseoPrice, JoseadorID, JoseoStartTime, JoseoEstimatedTime, JoseoFinishTime, JoseoContratoID, JoseoStatus)
VALUES ('Desarrollador Blockchain para Plataforma de Contratos Inteligentes', '�nete a nuestro equipo como desarrollador especializado en blockchain. Participar�s en el desarrollo de una plataforma de contratos inteligentes para facilitar transacciones seguras y transparentes...', '75000', '1', GETDATE(), DATEADD(MONTH, 3, GETDATE()), '2023-10-01', 'C004', 'Abierto');

-- Ejemplo 5
INSERT INTO JoseosTable (JoseoTitle, JoseoDescription, JoseoPrice, JoseadorID, JoseoStartTime, JoseoEstimatedTime, JoseoFinishTime, JoseoContratoID, JoseoStatus)
VALUES ('Dise�ador UX/UI para Plataforma de Realidad Virtual en Educaci�n', 'Buscamos un dise�ador UX/UI apasionado por la realidad virtual y la educaci�n. Ser�s responsable de crear interfaces inmersivas y atractivas para una plataforma educativa basada en realidad virtual...', '60000', '1', GETDATE(), DATEADD(MONTH, 1, GETDATE()), '2023-09-01', 'C005', 'Abierto');

-- Ejemplo 6
INSERT INTO JoseosTable (JoseoTitle, JoseoDescription, JoseoPrice, JoseadorID, JoseoStartTime, JoseoEstimatedTime, JoseoFinishTime, JoseoContratoID, JoseoStatus)
VALUES ('Ingeniero de Seguridad Cibern�tica para Protecci�n de Datos de Investigaci�n', '�nete a nuestro equipo como ingeniero de seguridad cibern�tica para un proyecto cr�tico en la protecci�n de datos de investigaci�n...', '80000', '1', GETDATE(), DATEADD(MONTH, 3, GETDATE()), '2023-11-01', 'C006', 'Abierto');

-- Ejemplo 7
INSERT INTO JoseosTable (JoseoTitle, JoseoDescription, JoseoPrice, JoseadorID, JoseoStartTime, JoseoEstimatedTime, JoseoFinishTime, JoseoContratoID, JoseoStatus)
VALUES ('Desarrollador de Juegos para Proyecto de Simulaci�n M�dica Interactiva', 'Estamos en busca de un desarrollador de juegos apasionado por la simulaci�n m�dica. Ser�s parte de un equipo que desarrolla una plataforma interactiva para entrenamiento m�dico...', '70000', '1', GETDATE(), DATEADD(MONTH, 1, GETDATE()), '2023-10-01', 'C007', 'Abierto');


INSERT INTO JoseosTable (JoseoTitle, JoseoDescription, JoseoPrice, JoseadorID, JoseoStartTime, JoseoEstimatedTime, JoseoFinishTime, JoseoContratoID, JoseoStatus)
VALUES ('Desarrollador Full Stack para Plataforma Educativa Online', 'Buscamos un desarrollador Full Stack apasionado por la educaci�n online...', '55000', '1', GETDATE(), DATEADD(YEAR, 2, GETDATE()), '2025-07-01', 'C001', 'Abierto');


Drop Table JoseosTable;
SELECT TOP 5 * FROM JoseosTable Where JoseadorRealID = '0';
SELECT * FROM JoseosTable Where JoseoID = '1';
SELECT * FROM JoseosTable Where JoseadorRealID = 1;
--_________________________________________________________________________________________________________

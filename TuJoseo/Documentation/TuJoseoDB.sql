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
UserRol Varchar(100)
);
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
'Programador y soporte t�cnico'
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

Select * From NotesTable Where NoteUserID = 1;

Drop Table NotesTable;

--Ejecutar pruebas.
SELECT
      [UserName] as 'Usuario'
      ,[UserCompleteName] as 'Nombre Completo'
      ,[UserPassword] as 'Contrase�a'
      ,[UserEmail] as 'Correo'
      ,[UserRememberMe] as 'Mantener sesi�n'
      ,[UserJoseosRealized] as 'Joseos realizados'
      ,[UserJobQuality] as 'Calidad de trabajo'
      ,[UserSimpaty] as 'Simpat�a'
      ,[UserStalkers] as 'Stalkers'
      ,[UserRelevance] as 'Relevancia'
      ,[UserKnowlegde] as 'Conocimientos'
      ,[UserLastLogin] as '�ltima vez'
      ,[UserUnreadNotifications] as 'Notificaciones sin leer'
      ,[UserUnreadNotificationsTime] as '�ltima notificaci�n'
      ,[UserUnreadMessages] as 'Mensajes sin leer'
      ,[UserUnreadMessagesTime] as '�ltimo mensaje'
      ,[UserUnreadReports] as 'Reportes sin leer'
      ,[UserUnreadReportsTime] as '�ltimo reporte'
      ,[UserEducation] as 'Educaci�n'
      ,[UserLocation] as 'Ubicaci�n'
      ,[UserHabilities] as 'Habilidades'
      ,[UserNotes] as 'Notas'
      ,[UserRol] as 'Rol'
  FROM [TuJoseoDB].[dbo].[UserTable];
--_________________________________________________________________________________________________________
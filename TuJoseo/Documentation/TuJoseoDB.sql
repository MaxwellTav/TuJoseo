--Crear la base de datos.
Create Database TuJoseoDB

--_________________________________________________________________________________________________________
--Usar la base de datos (Cada vez que inicies SQL Server hay que ejecutar esta línea).
Use TuJoseoDB

--_________________________________________________________________________________________________________
--Crear las tablas que se utilizarán.
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
'He hecho proyectos para grandes empresas... Solo soy un programador que está buscando nuevos desafíos para superar.',
--UserLastLogin (En días), ha iniciado sesión hace 3 días.
3,

--UserUnreadNotifications
5,
--UserUnreadNotificationsTime (En días) (tiempo del último mensaje enviado)
2,
--UserUnreadMessages
5,
--UserUnreadMessagesTime (En días) (tiempo del último mensaje enviado)
3,
--UserUnreadReports
4,
--UserUnreadReportsTime (En días) (tiempo del último mensaje enviado)
1,

--UserEducation
'Estudiante graduado del ITLA, Liceo Parroquial San Pablo Apostol e Infotep',
--UserLocation
'República Dominicana',
--UserHabilities
'C#, Asp .Net Core, SQL, NonSQL, MongoDB, Arquitectura MVC, Full Stack, 5 años de experiencia.',
--UserNotes
'Soporte técnico, programador y dominio del idioma inglés B1.',
--UserRol
'Programador y soporte técnico'
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
      ,[UserPassword] as 'Contraseña'
      ,[UserEmail] as 'Correo'
      ,[UserRememberMe] as 'Mantener sesión'
      ,[UserJoseosRealized] as 'Joseos realizados'
      ,[UserJobQuality] as 'Calidad de trabajo'
      ,[UserSimpaty] as 'Simpatía'
      ,[UserStalkers] as 'Stalkers'
      ,[UserRelevance] as 'Relevancia'
      ,[UserKnowlegde] as 'Conocimientos'
      ,[UserLastLogin] as 'Última vez'
      ,[UserUnreadNotifications] as 'Notificaciones sin leer'
      ,[UserUnreadNotificationsTime] as 'Última notificación'
      ,[UserUnreadMessages] as 'Mensajes sin leer'
      ,[UserUnreadMessagesTime] as 'Último mensaje'
      ,[UserUnreadReports] as 'Reportes sin leer'
      ,[UserUnreadReportsTime] as 'Último reporte'
      ,[UserEducation] as 'Educación'
      ,[UserLocation] as 'Ubicación'
      ,[UserHabilities] as 'Habilidades'
      ,[UserNotes] as 'Notas'
      ,[UserRol] as 'Rol'
  FROM [TuJoseoDB].[dbo].[UserTable];
--_________________________________________________________________________________________________________
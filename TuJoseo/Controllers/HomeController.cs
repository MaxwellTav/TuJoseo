using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using TuJoseo.Models;

namespace TuJoseo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private MainHomeModel _mainHomeModel;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DB");
        }

        public async Task<IActionResult> Index(MainHomeModel? homeModel)
        {
            #region Setear usuario
            int userID = Convert.ToInt32(TempData["UserID"]);
            string query = $@"SELECT
       [UserID] as 'ID'
      ,[UserName] as 'Usuario'
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
      ,[UserPhone] as 'Phone'
  FROM [TuJoseoDB].[dbo].[UserTable]
  Where [TuJoseoDB].[dbo].[UserTable].[UserID] = '{userID}';";

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        #region User
                        UserModel exUserModel = new UserModel()
                        {
                            UserID = userID
                        };
                        homeModel.USER = exUserModel;
                        #endregion

                        // Utiliza parámetros de consulta parametrizados
                        cmd.Parameters.AddWithValue("@UserID", homeModel.USER.UserID);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    homeModel.USER.UserID = reader.GetInt32(0);
                                    homeModel.USER.UserName = reader.GetString(1);
                                    homeModel.USER.UserCompleteName = reader.GetString(2);
                                    homeModel.USER.UserPassword = reader.GetString(3);
                                    homeModel.USER.UserEmail = reader.GetString(4);

                                    homeModel.USER.UserRememberMe = Convert.ToBoolean(reader.GetInt32(5));
                                    homeModel.USER.UserJoseosRealized = reader.GetInt32(6);
                                    homeModel.USER.UserJobQuality = reader.GetInt32(7);
                                    homeModel.USER.UserSimpaty = reader.GetInt32(8);
                                    homeModel.USER.UserStalkers = reader.GetInt32(9);
                                    homeModel.USER.UserRelevance = reader.GetInt32(10);
                                    homeModel.USER.UserKnowledge = reader.GetString(11);
                                    homeModel.USER.UserLastLogin = reader.GetInt32(12);

                                    homeModel.USER.UserUnreadNotification = reader.GetInt32(13);
                                    homeModel.USER.UserUnreadNotificationTime = reader.GetInt32(14);
                                    homeModel.USER.UserUnreadMessages = reader.GetInt32(15);
                                    homeModel.USER.UserUnreadMessagesTime = reader.GetInt32(16);
                                    homeModel.USER.UserUnreadReports = reader.GetInt32(17);
                                    homeModel.USER.UserUnreadReportsTime = reader.GetInt32(18);

                                    homeModel.USER.UserEducation = reader.GetValue(19).ToString();
                                    homeModel.USER.UserLocation = reader.GetValue(20).ToString();
                                    homeModel.USER.UserHabilities = reader.GetValue(21).ToString();
                                    homeModel.USER.UserNotes = reader.GetValue(22).ToString();
                                    homeModel.USER.UserRol = reader.GetValue(23).ToString();
                                    homeModel.USER.UserPhone = reader.GetString(24);
                                }
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "No se encontró el usuario.";
                                return RedirectToAction("Index", "Login");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                TempData["ErrorMessage"] = "Hubo un error al momento de buscar el usuario.";
                return RedirectToAction("Index", "Login");
            }
            #endregion

            #region Notas
            string queryNote = @$"Select * From NotesTable Where NoteUserID = {userID};";

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(queryNote, con))
                    {
                        NotesModel notes = new NotesModel();
                        Note note = new Note();

                        using (SqlCommand cmdnote = new SqlCommand(queryNote, con))
                        {
                            using (SqlDataReader readernote = await cmd.ExecuteReaderAsync())
                            {
                                if (readernote.HasRows)
                                {
                                    notes.NoteList = new List<Note>();
                                    while (readernote.Read())
                                    {
                                        note = new Note()
                                        {
                                            NoteID = readernote.GetInt32(0),
                                            NoteUserID = readernote.GetInt32(1),
                                            NoteDescription = readernote.GetString(2),
                                            NoteTime = readernote.GetDateTime(3),
                                            NoteDone = readernote.GetBoolean(4),
                                        };

                                        notes.NoteList.Add(note);
                                    }
                                    homeModel.NOTES = notes;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Notas";
            }
            #endregion

            #region Joseos
            string queryJoseo = "SELECT TOP 5 * FROM JoseosTable WHERE JoseadorRealID IS NULL;";

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(queryJoseo, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<JoseoModel> joseos = new List<JoseoModel>();

                                while (reader.Read())
                                {
                                    JoseoModel joseo = new JoseoModel()
                                    {
                                        JoseoID = reader.GetInt32(0),
                                        JoseoTitle = reader.GetString(1),
                                        JoseoDescription = reader.GetString(2),
                                        JoseoPrice = reader.GetString(3),
                                        JoseadorID = reader.GetString(4),
                                        JoseoStartTime = reader.GetDateTime(5),
                                        JoseoEstimatedTime = reader.GetDateTime(6),
                                        JoseoFinishTime = reader.GetDateTime(7),
                                        JoseoContratoID = reader.GetString(8),
                                        JoseoStatus = reader.GetString(9)
                                    };

                                    joseos.Add(joseo);
                                }
                                homeModel.JOSEOS = new List<JoseoModel>();
                                homeModel.JOSEOS = joseos;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            #endregion

            #region Claims (Deprecated)
            //ClaimsPrincipal c = HttpContext.User;
            //if (c.Identity != null)
            //{
            //    if (c.Identity.IsAuthenticated)
            //        return RedirectToAction("Index", "Login");
            //}

            //List<Claim> claims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
            //};

            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //AuthenticationProperties authenticationProperties = new AuthenticationProperties();

            //authenticationProperties.AllowRefresh = true;

            //authenticationProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7);

            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authenticationProperties);

            #endregion

            #region Mandar usuario a TempData
            _mainHomeModel = homeModel;
            TempData["UserID"] = homeModel.USER.UserID;
            return View(homeModel);
            #endregion
        }

        public IActionResult IndexHTML()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult DeleteNote(int NoteID)
        {
            string query = @$"
                              DELETE FROM NotesTable
                              WHERE NoteID = {NoteID};";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch
                { }
                finally { con.Close(); }
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> AddNote(string noteDescription)
        {
            if (string.IsNullOrEmpty(noteDescription))
                return RedirectToAction("Index", "Home");

            string query = @$"
                    INSERT INTO NotesTable (NoteUserID, NoteDescription, NoteTime, NoteDone)
                    VALUES ({TempData["UserID"]}, '{noteDescription}', GETDATE(), 0);";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    // Maneja la excepción o regístrala para su posterior análisis
                }
                finally
                {
                    con.Close();
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult MarkNoteAsDone(int noteID)
        {
            string query = $@"UPDATE NotesTable 
                            SET NoteDone = 1
                            WHERE NoteID = {noteID};";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch
                { }
                finally { con.Close(); }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
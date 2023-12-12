using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TuJoseo.Models;

namespace TuJoseo.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DB");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
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
  FROM [TuJoseoDB].[dbo].[UserTable]
  Where [TuJoseoDB].[dbo].[UserTable].[UserEmail] = '{user.UserEmail}' 
  And [TuJoseoDB].[dbo].[UserTable].[UserPassword] = '{user.UserPassword}';
";

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Utiliza parámetros de consulta parametrizados
                        cmd.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                        cmd.Parameters.AddWithValue("@UserPassword", user.UserPassword);

                        con.Open();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    user.UserID = reader.GetInt32(0);
                                    #region .
                                    //user.UserName = reader.GetString(1);
                                    //user.UserCompleteName = reader.GetString(2);
                                    //user.UserPassword = reader.GetString(3);
                                    //user.UserEmail = reader.GetString(4);

                                    //user.UserRememberMe = Convert.ToBoolean(reader.GetInt32(5));
                                    //user.UserJoseosRealized = reader.GetInt32(6);
                                    //user.UserJobQuality = reader.GetInt32(7);
                                    //user.UserSimpaty = reader.GetInt32(8);
                                    //user.UserStalkers = reader.GetInt32(9);
                                    //user.UserRelevance = reader.GetInt32(10);
                                    //user.UserKnowledge = reader.GetString(11);
                                    //user.UserLastLogin = reader.GetInt32(12);

                                    //user.UserUnreadNotification = reader.GetInt32(13);
                                    //user.UserUnreadNotificationTime = reader.GetInt32(14);
                                    //user.UserUnreadMessages = reader.GetInt32(15);
                                    //user.UserUnreadMessagesTime = reader.GetInt32(16);
                                    //user.UserUnreadReports = reader.GetInt32(17);

                                    //user.UserEducation = reader.GetValue(18).ToString();
                                    //user.UserLocation = reader.GetValue(19).ToString();
                                    //user.UserHabilities = reader.GetValue(20).ToString();
                                    //user.UserNotes = reader.GetValue(21).ToString();
                                    #endregion
                                }

                                TempData["Success"] = "Nos alegra tenerte de vuelta.";
                                TempData["UserID"] = user.UserID;
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                TempData["Error"] = "Nada concuerda, verifica bien...";
                                return RedirectToAction("Index", "Login");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                TempData["Error"] = "Ocurrió un error durante el inicio de sesión. " + ex.Message;
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult RealForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RealForgotPassword(UserModel user)
        {
            return View();
        }

        public IActionResult RegisterNewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(UserModel user)
        {
            int trueorfalseformatted;
            if (user.Terms)
                trueorfalseformatted = 1;
            else
                trueorfalseformatted = 0;


            string query = @$"
        INSERT INTO UserTable 
        (UserName, UserCompleteName, UserPassword, UserEmail, UserRememberMe,
         UserJoseosRealized, UserJobQuality, UserSimpaty, UserStalkers, UserRelevance,
         UserKnowlegde, UserLastLogin, UserUnreadNotifications, UserUnreadNotificationsTime,
         UserUnreadMessages, UserUnreadMessagesTime, UserUnreadReports, UserUnreadReportsTime,
         UserEducation, UserLocation, UserHabilities, UserNotes, UserRol)
        VALUES 
        ('{user.UserName}', '{user.UserCompleteName}', '{user.UserPassword}', '{user.UserEmail}', {trueorfalseformatted},
         {user.UserJoseosRealized}, {user.UserJobQuality}, {user.UserSimpaty}, {user.UserStalkers}, {user.UserRelevance},
         '{user.UserKnowledge}', {user.UserLastLogin}, {user.UserUnreadNotification}, {user.UserUnreadNotificationTime},
         {user.UserUnreadMessages}, {user.UserUnreadMessagesTime}, {user.UserUnreadReports}, {user.UserUnreadReportsTime},
         '{user.UserEducation}', '{user.UserLocation}', '{user.UserHabilities}', '{user.UserNotes}', '{user.UserRol}');";

            try
            {
                if (user.UserConfirmPassword != user.UserPassword)
                {
                    TempData["Error"] = "Las contrasenas no coinciden";
                }
                else if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.UserPassword) || string.IsNullOrEmpty(user.UserConfirmPassword) || string.IsNullOrEmpty(user.UserCompleteName))
                {
                    TempData["Error"] = "Por que hay campos vacios?";
                }
                else if (!user.Terms)
                {
                    TempData["Error"] = "Es obligatorio aceptar todos nuestros terminos y condiciones.";
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            TempData["Success"] = "El usuario ha sido creado con exito.";
                            return RedirectToAction("Index", "Login");
                        }
                    }
                }
            }
            catch (Exception error)
            {
                TempData["Error"] = "Hubo un error grave " + error.Message;
                return View();
            }

            return View();
        }

        public async Task<IActionResult> TermsAndConditions()
        {
            return View();
        }
    }
}

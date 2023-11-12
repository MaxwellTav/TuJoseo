using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
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
            ConnectionString = _configuration.GetConnectionString("TestDB");
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
                                    user.UserName = reader.GetString(1);
                                    user.UserCompleteName = reader.GetString(2);
                                    user.UserPassword = reader.GetString(3);
                                    user.UserEmail = reader.GetString(4);

                                    user.UserRememberMe = Convert.ToBoolean(reader.GetInt32(5));
                                    user.UserJoseosRealized = reader.GetInt32(6);
                                    user.UserJobQuality = reader.GetInt32(7);
                                    user.UserSimpaty = reader.GetInt32(8);
                                    user.UserStalkers = reader.GetInt32(9);
                                    user.UserRelevance = reader.GetInt32(10);
                                    user.UserKnowledge = reader.GetString(11);
                                    user.UserLastLogin = reader.GetInt32(12);

                                    user.UserUnreadNotification = reader.GetInt32(13);
                                    user.UserUnreadNotificationTime = reader.GetInt32(14);
                                    user.UserUnreadMessages = reader.GetInt32(15);
                                    user.UserUnreadMessagesTime = reader.GetInt32(16);
                                    user.UserUnreadReports = reader.GetInt32(17);

                                    user.UserEducation = reader.GetValue(18).ToString();
                                    user.UserLocation = reader.GetValue(19).ToString();
                                    user.UserHabilities = reader.GetValue(20).ToString();
                                    user.UserNotes = reader.GetValue(21).ToString();
                                }

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Usuario o contraseña incorrectos.";
                                return RedirectToAction("Index", "Login");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                TempData["ErrorMessage"] = "Ocurrió un error durante el inicio de sesión.";
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

        public IActionResult RegisterNewUser()
        {
            return View();
        }
    }
}

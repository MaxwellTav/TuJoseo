﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using TuJoseo.Models;

namespace TuJoseo.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public ProfileController(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DB");
        }

        public async Task<IActionResult> Index(UserModel user)
        {
            #region Setear usuario
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
  FROM [TuJoseoDB].[dbo].[UserTable]
  Where [TuJoseoDB].[dbo].[UserTable].[UserID] = '{TempData["UserID"]}';";

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Utiliza parámetros de consulta parametrizados
                        cmd.Parameters.AddWithValue("@UserID", user.UserID);

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
                                    user.UserUnreadReportsTime = reader.GetInt32(18);

                                    user.UserEducation = reader.GetValue(19).ToString();
                                    user.UserLocation = reader.GetValue(20).ToString();
                                    user.UserHabilities = reader.GetValue(21).ToString();
                                    user.UserNotes = reader.GetValue(22).ToString();
                                    user.UserRol = reader.GetValue(23).ToString();
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

            TempData["UserID"] = user.UserID;
            return View(user);
        }

        [HttpPost]
        public ActionResult GuardarUserID(string userID)
        {
            TempData["UserID"] = userID;
            return Json(new { success = true });
        }
    }
}

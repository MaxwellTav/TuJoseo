using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TuJoseo.Models;

namespace TuJoseo.Controllers
{
    public class JoseosController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public JoseosController(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DB");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchJoseo()
        {
            return View();
        }

        public IActionResult SearchJoseador()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SeeJoseo(int joseoID)
        {
            MainJoseoModel mjm = new MainJoseoModel();
            JoseoModel joseo = new JoseoModel();
            string query = @$"SELECT * FROM JoseosTable Where JoseoID = '{joseoID}';";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                joseo = new JoseoModel()
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
                            }
                        }
                    }
                }
                con.Close();
                mjm.JOSEO = joseo;
            }

            string queryUser = $@"SELECT *
  FROM [TuJoseoDB].[dbo].[UserTable]
  Where [TuJoseoDB].[dbo].[UserTable].[UserID] = '{joseo.JoseadorID}';
";
            UserModel user = new UserModel();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryUser, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user.UserID = reader.GetInt32(0);
                                #region .
                                user.UserName = reader.GetString(1);
                                user.UserCompleteName = reader.GetString(2);
                                user.UserPassword = "Sin información";
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
                                user.UserPhone = reader.GetValue(24).ToString();
                                user.UserPhoto = reader.GetValue(25).ToString();
                                #endregion
                            }

                            TempData["UserID"] = user.UserID;
                            mjm.JOSEADOR = user;
                        }
                        else
                        {
                            TempData["Error"] = "Error al tener al joseador, verifica bien...";
                        }
                    }
                }
            }

            return View(mjm);
        }

        public IActionResult SeeJoseador()
        {
            return View();
        }
    }
}

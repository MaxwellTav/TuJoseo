using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.Xml;
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

        //Buscar Joseador
        public IActionResult Index()
        {
            List<UserModel> users = new List<UserModel>();
            string query = "SELECT UserID, UserRol, UserName, UserHabilities, UserLocation, UserPhone, UserJoseosRealized, UserJobQuality, UserSimpaty FROM [TuJoseoDB].[dbo].[UserTable];";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserModel user = new UserModel()
                                {
                                    UserID = reader.GetInt32(0),
                                    UserRol = reader.GetString(1),
                                    UserName = reader.GetString(2),
                                    UserHabilities = reader.GetString(3),
                                    UserLocation = reader.GetString(4),
                                    UserPhone = reader.GetString(5),
                                    UserJoseosRealized = reader.GetInt32(6),
                                    UserJobQuality = reader.GetInt32(7),
                                    UserSimpaty = reader.GetInt32(8),
                                };

                                users.Add(user);
                            }
                        }
                    }
                }
            }

            return View(users);
        }

        public IActionResult SearchJoseo()
        {
            string userID = TempData["UserID"].ToString();

            List<JoseoModel> joseos = new List<JoseoModel>();
            string query = @$"SELECT * FROM JoseosTable WHERE JoseadorRealID IS NULL AND JoseadorID != '{userID}';";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                JoseoModel joseo = new JoseoModel()
                                {
                                    JoseoID = reader.GetInt32(0),
                                    JoseoTitle = reader.GetString(1),
                                    JoseoDescription = reader.GetString(2),
                                    JoseoPrice = reader.GetString(3),
                                    JoseadorID = reader.GetString(4)
                                };

                                joseos.Add(joseo);
                            }
                        }
                    }
                }
            }

            TempData["UserID"] = userID;
            return View(joseos);
        }

        public IActionResult SearchOwnJoseo()
        {
            string userID = TempData["UserID"].ToString();

            List<JoseoModel> joseos = new List<JoseoModel>();
            MainOwnJoseoModel mainOwnJoseoModel = new MainOwnJoseoModel()
            {
                MisJoseos = new List<JoseoModel>(),
                JoseosCreados = new List<JoseoModel>()
            };
            string queryMisJoseos = @$"SELECT * FROM JoseosTable Where JoseadorRealID = {userID};";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryMisJoseos, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                    JoseoModel joseo = new JoseoModel()
                                {
                                    JoseoID = reader.GetInt32(0),
                                    JoseoTitle = reader.GetString(1),
                                    JoseoDescription = reader.GetString(2),
                                    JoseoPrice = reader.GetString(3),
                                    JoseadorID = reader.GetString(4),
                                    JoseadorRealID = reader.GetString(10)
                                };

                                joseos.Add(joseo);
                            }
                            mainOwnJoseoModel.MisJoseos = joseos;
                        }
                    }
                }
            }

            List<JoseoModel> joseoscreado = new List<JoseoModel>();

            string queryJoseosCreados = @$"SELECT * FROM JoseosTable Where JoseadorID = {userID};";
            using (SqlConnection concreado = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmdcreado = new SqlCommand(queryJoseosCreados, concreado))
                {
                    concreado.Open();
                    using (SqlDataReader reader = cmdcreado.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                JoseoModel joseo = new JoseoModel()
                                {
                                    JoseoID = reader.GetInt32(0),
                                    JoseoTitle = reader.GetString(1),
                                    JoseoDescription = reader.GetString(2),
                                    JoseoPrice = reader.GetString(3),
                                    JoseadorID = reader.GetString(4)
                                };

                                joseoscreado.Add(joseo);
                            }
                            mainOwnJoseoModel.JoseosCreados = joseoscreado;
                        }
                    }
                }
            }

            TempData["UserID"] = userID;
            return View(mainOwnJoseoModel);
        }

        public IActionResult SearchJoseador()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SeeJoseo(int joseoID)
        {
            var userID = TempData["UserID"].ToString();

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

            mjm.ME = new UserModel()
            {
                UserID = Convert.ToInt32(userID),
            };
            return View(mjm);
        }

        public IActionResult CreateJoseo()
        {
            string userID = TempData["UserID"].ToString();
            TempData["UserID"] = userID;

            return View();
        }

        [HttpPost]
        public IActionResult CreateNewJoseo([FromBody] CreateJoseoModel joseo)
        {
            if (joseo == null)
            {
                TempData["Error"] = "Ninguno de los campos pueden estar nulos";
                return View(joseo);
            }

            // Verifica que cada propiedad tenga un valor
            if (string.IsNullOrEmpty(joseo.JoseoTitle) ||
                string.IsNullOrEmpty(joseo.JoseoDescription) ||
                string.IsNullOrEmpty(joseo.JoseoStatus) ||
                joseo.JoseoPrice == null ||
                joseo.JoseoEstimatedTime == null)
            {
                // Al menos una propiedad es nula o vacía, realiza la lógica necesaria (puede ser un error)
                TempData["Error"] = "Ninguno de los campos pueden estar nulos";
                return View(joseo);
            }

            string query = $@"Insert Into JoseosTable (JoseoTitle, JoseoDescription, JoseoStatus, JoseoPrice, JoseoEstimatedTime, JoseadorID, JoseoFinishTime, JoseoContratoID) Values
                            ('{joseo.JoseoTitle}', 
                            '{joseo.JoseoDescription}', 
                            '{joseo.JoseoStatus}', 
                            '{joseo.JoseoPrice}', 
                            '{joseo.JoseoEstimatedTime}', 
                            '{joseo.JoseadorID}', 
                            '{DateTime.Now}', 
                            '0090239');";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            TempData["Success"] = "Joseo creado con exito!";
            TempData["UserID"] = joseo.JoseadorID;
            return RedirectToAction("Index", "Home");
        }
    }
}

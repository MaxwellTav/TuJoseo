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
            }

            return View(joseo);
        }

        public IActionResult SeeJoseador()
        {
            return View();
        }
    }
}

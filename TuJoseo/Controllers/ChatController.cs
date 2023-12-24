 using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace TuJoseo.Controllers
{
    public class ChatController : Controller
    {
        public static Dictionary<int, string> Rooms =
            new Dictionary<int, string>();

        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public ChatController(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DB");

            #region Obtener las salas
            if (Rooms.Count < 1)
            {
                string query = "Select UserID, UserName From UserTable;";
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
                                {//Obteniendo        el ID      |      El nombre
                                    Rooms.Add(reader.GetInt32(0), reader.GetString(1));
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Room(int room)
        {
            return View("Room",  room);
        }
    }
}

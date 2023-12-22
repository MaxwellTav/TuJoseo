using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TuJoseo.Models;

namespace TuJoseo.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        public InvoiceController(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DB");
        }

        public IActionResult Index()
        {
            // Recuperar el valor de la cookie
            var modelDataCookie = HttpContext.Request.Cookies["modelData"];

            if (modelDataCookie != null)
            {
                // Deserializar los datos si la cookie existe
                var model = JsonConvert.DeserializeObject<MainJoseoModel>(Uri.UnescapeDataString(modelDataCookie));

                // Limpiar la cookie después de usar los datos (opcional)
                Response.Cookies.Delete("modelData");

                string query = @$"Update JoseosTable Set JoseadorRealID = '{model.ME.UserID}' Where JoseoID = '{model.JOSEO.JoseoID}';";
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                TempData["Success"] = "JOSEO CONSEGUIDO";
                TempData["UserID"] = model.ME.UserID;

                return View(model); // Redirigir a una acción adecuada o manejar el caso según tus necesidades
            }

            TempData["Error"] = "Error inesperado.";

            // Manejar el caso en que la cookie no contiene datos
            return RedirectToAction("Index", "Error"); // Redirigir a una acción adecuada o manejar el caso según tus necesidades
        }

    }
}

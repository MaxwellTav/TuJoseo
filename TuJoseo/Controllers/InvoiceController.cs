using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TuJoseo.Models;

namespace TuJoseo.Controllers
{
    public class InvoiceController : Controller
    {
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

                TempData["Success"] = "JOSEO CONSEGUIDO";
                return View(model); // Redirigir a una acción adecuada o manejar el caso según tus necesidades
            }

            TempData["Error"] = "Error inesperado.";

            // Manejar el caso en que la cookie no contiene datos
            return RedirectToAction("Index", "Error"); // Redirigir a una acción adecuada o manejar el caso según tus necesidades
        }

    }
}

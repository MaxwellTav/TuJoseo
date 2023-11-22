using Microsoft.AspNetCore.Mvc;

namespace TuJoseo.Controllers
{
    public class JoseosController : Controller
    {
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
    }
}

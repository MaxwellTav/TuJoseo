using Microsoft.AspNetCore.Mvc;

namespace TuJoseo.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace TuJoseo.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

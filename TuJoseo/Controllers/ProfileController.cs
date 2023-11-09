using Microsoft.AspNetCore.Mvc;

namespace TuJoseo.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

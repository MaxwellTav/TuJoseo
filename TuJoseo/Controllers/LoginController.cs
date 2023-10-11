using Microsoft.AspNetCore.Mvc;

namespace TuJoseo.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult RealForgotPassword()
        {
            return View();
        }

        public IActionResult RegisterNewUser()
        {
            return View();
        }
    }
}

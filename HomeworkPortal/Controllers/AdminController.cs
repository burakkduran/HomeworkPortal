using Microsoft.AspNetCore.Mvc;

namespace HomeworkPortal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

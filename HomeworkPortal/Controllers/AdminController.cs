using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkPortal.Controllers
{
    [Authorize]

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

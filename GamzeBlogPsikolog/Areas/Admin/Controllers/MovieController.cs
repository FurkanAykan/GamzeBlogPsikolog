using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

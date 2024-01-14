using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

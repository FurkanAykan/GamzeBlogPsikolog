using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

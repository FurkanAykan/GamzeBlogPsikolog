using GamzeBlogPsikolog.Entity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _aboutService.GetAboutAsync();
            return View(model);
        }
    }
}

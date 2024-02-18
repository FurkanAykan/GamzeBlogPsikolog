using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using GamzeBlogPsikolog.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly IEducationService educationService;

        public EducationController(IEducationService educationService)
        {
            this.educationService = educationService;
        }

        public async Task<IActionResult> EducationIndex()
        {
            var education = await educationService.GetAll();
            return View(education);
        }
        [HttpPost]
        public async Task<IActionResult> AddEducations([FromBody]EducationViewModel m)
        {
            AlertContent alert = new AlertContent();
            if (ModelState.IsValid)
            {
                var educationAdd = await educationService.AddEducation(m);
                alert.Message = "Kayıt Başarıyla Eklendi.";
            }
            return Json(alert);
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int id)
        {
            var geteducation = await educationService.GetByIdAsync(id);
            return Json(geteducation);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await educationService.DeleteEducation(id);
            return Json("Ok");
        }
    }
}

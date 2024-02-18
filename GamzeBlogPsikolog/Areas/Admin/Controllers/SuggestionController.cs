using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using GamzeBlogPsikolog.Models;
using GamzeBlogPsikolog.Services;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SuggestionController : Controller
    {
        private readonly IMovieService _movieService;

        public SuggestionController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult SuggestionIndex()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSuggestion([FromBody] SuggestionViewModel m)
        {
            AlertContent alert = new AlertContent();
            if (ModelState.IsValid)
            {
                var suggestionAdd = await _movieService.AddSuggestion(m);
                return Json(suggestionAdd);

            }
            return Json(alert);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int id)
        {
            var getsuggestion = await _movieService.GetAll(id);
            return Json(getsuggestion);
        }
        [HttpGet]
        public async Task<IActionResult> GetData(int id)
        {
            var getsuggestion = await _movieService.GetByIdAsync(id);
            return Json(getsuggestion);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
           var suggestion =  await _movieService.DeleteSuggestion(id);
            
            return Json(suggestion);
        }
        [HttpPost]
        public string UploadImage(int id, IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (file != null && file.Length > 0)
            {
                // wwwroot dizini içindeki Hakkında klasörünü oluştur
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Oneriler");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Dosya adındaki türkçe karakterleri veya boşlukları kaldır
                var fileName = Path.GetFileNameWithoutExtension(file.FileName); // Uzantıyı dahil etmemek için FileNameWithoutExtension kullanılır
                fileName = RemoveTurkishCharactersAndSpaces(fileName); // Türkçe karakterleri ve boşlukları kaldıran fonksiyonu çağır
                var fileExtension = Path.GetExtension(file.FileName);
                fileName = fileName + fileExtension;

                var imagePath = Path.Combine(uploadsFolder, fileName);

                // Dosyayı kaydet
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Resmi yeniden boyutlandır
                var width = 1920; // İstediğiniz genişlik
                var height = 1280; // İstediğiniz yükseklik
                using (var image = Image.Load(imagePath))
                {
                    // İstenilen boyuta resmi yeniden boyutlandır
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(width, height),
                        Mode = ResizeMode.Max
                    }));
                    // Yeniden boyutlandırılmış resmi kaydet
                    image.Save(imagePath);
                }

                return ("/Oneriler/" + fileName);
            }

            return null;
        }

        private string RemoveTurkishCharactersAndSpaces(string input)
        {
            // Türkçe karakterleri ve boşlukları kaldır
            string[] turkishChars = { "ç", "ğ", "ı", "i", "ö", "ş", "ü", " " };
            string[] replacementChars = { "c", "g", "i", "i", "o", "s", "u", "_" };

            for (int i = 0; i < turkishChars.Length; i++)
            {
                input = input.Replace(turkishChars[i], replacementChars[i]);
            }

            return input;
        }
    }
}

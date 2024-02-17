using AutoMapper;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.Entity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using GamzeBlogPsikolog.EntityViewModels;
using GamzeBlogPsikolog.Models;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IGenericRepostory<About> _about;
        private readonly IMapper mapper;
        private readonly IAboutService aboutService;

        public AboutController(IGenericRepostory<About> about, IMapper mapper, IAboutService aboutService)
        {
            _about = about;
            this.mapper = mapper;
            this.aboutService = aboutService;
        }

        public async Task<IActionResult> AboutIndex()
        {
            var hakkindaDeger = await _about.GetByIdAsync(x=>x.AboutId==1);
            var mapHakkinda = mapper.Map<AdminAboutViewModel>(hakkindaDeger);
            return View(mapHakkinda);
        }
        [HttpPost]
        public async Task<IActionResult> AddAbout([FromBody] AdminAboutViewModel m)
        {
            AlertContent alert = new AlertContent();
            if (ModelState.IsValid)
            {
                
                try
                {
                    if (m.AboutId == 0)
                    {
                        var aboutAdd = mapper.Map<About>(m);
                        _about.Add(aboutAdd);
                        alert.Message = "Kayıt Başarıyla Eklendi.";
                    }
                    else
                    {
                        var hakkinda = await _about.GetByIdAsync(x => x.AboutId == 1);
                        if (m.AboutImage==null)
                        {
                            m.AboutImage= hakkinda.AboutImage;
                            hakkinda.AboutTitle = m.AboutTitle;
                            hakkinda.AboutContent = m.AboutContent;
                            hakkinda.AboutImage = m.AboutImage;
                        }
                        else
                        {
                            hakkinda.AboutTitle = m.AboutTitle;
                            hakkinda.AboutContent = m.AboutContent;
                            hakkinda.AboutImage = m.AboutImage;
                        }
                       
                        _about.Update(hakkinda);
                        alert.Message = "Kayıt Başarıyla Güncellendi.";
                    }
                    return Json(alert);
                }
                catch (Exception ex)
                {
                    alert.Message = "Bir hata oluştu: " + ex.Message;
                    return StatusCode(500, alert); 
                }
            }
            else
            {
                return BadRequest(ModelState); 
            }
        }

        [HttpPost]
        public string UploadImage(int id, IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (file != null && file.Length > 0)
            {
                // wwwroot dizini içindeki Hakkında klasörünü oluştur
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Hakkında");

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

                return ("/Hakkında/" + fileName);
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

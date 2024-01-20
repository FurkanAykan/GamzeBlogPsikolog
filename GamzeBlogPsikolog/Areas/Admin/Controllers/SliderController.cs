using AutoMapper;
using GamzeBlogPsikolog.Entity;
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
    public class SliderController : Controller
    {
        private readonly IGenericRepostory<Slider> rp;
        private readonly IMapper _mapper;
        private readonly ISlider _sliderPostServis;

        public SliderController(IGenericRepostory<Slider> rp, IMapper mapper, ISlider sliderPostServis)
        {
            this.rp = rp;
            _mapper = mapper;
            _sliderPostServis = sliderPostServis;
        }


        public async Task<IActionResult> SliderIndex()
        {
            var sliderList = await rp.GetAll();
            var sl = _mapper.Map<List<SliderViewModel>>(sliderList);
            return View(sl);

       

        [HttpPost]
        public IActionResult AddSlider([FromBody] SliderViewModel m)
        {
            AlertContent alert = new AlertContent();
            if (ModelState.IsValid)
            {
                if (m.SliderId == 0)
                {
                    try
                    {
                        var sliderAdd = _mapper.Map<Slider>(m);
                        rp.Add(sliderAdd);
                        alert.Message = "Kayıt Başarıyla Eklendi.";
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                   
                }
                else
                {
                    Slider sliderUpdate = _mapper.Map<Slider>(m);
                    rp.Update(sliderUpdate);
                    alert.Message = "Kayıt Başarıyla Güncellendi.";
                }
            }
            return Json(alert);
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int id)
        {
            var getSlider = await rp.GetByIdAsync(x => x.SliderId == id);
            return Json(getSlider);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int postId)
        {
            var getSlider = await rp.GetByIdAsync(x => x.SliderId == postId);

            if (getSlider != null)
            {
                if (getSlider.Status)
                {
                    getSlider.Status = false;
                    rp.Update(getSlider);
                    return Json(false);

                }
                else
                {
                    getSlider.Status = true;
                    rp.Update(getSlider);
                    return Json(true);

                }
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UploadImage(int id, IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (file != null && file.Length > 0)
            {
                // wwwroot dizini içindeki SliderBig klasörünü oluştur
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "SliderBig");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Path.GetFileName(file.FileName);
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

                return Json(new { OriginalPath = "/SliderBig/" + fileName });
            }

            return Json(null);
        }

    }
}

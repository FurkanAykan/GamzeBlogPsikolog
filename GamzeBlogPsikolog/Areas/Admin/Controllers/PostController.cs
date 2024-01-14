using AutoMapper;
using GamzeBlogPsikolog.Context;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using GamzeBlogPsikolog.Models;
using GamzeBlogPsikolog.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly IGenericRepostory<BlogPost> rp;
        private readonly IMapper mapper;
        public PostController(IGenericRepostory<BlogPost> rp, IMapper mapper)
        {
            this.rp = rp;
            this.mapper = mapper;
        }

        [Area("Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bloglist = await rp.GetAll();
            List<BlogPostViewModel> blog = mapper.Map<List<BlogPostViewModel>>(bloglist); 
            return View(blog);
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult AddPost([FromBody] BlogPostViewModel m)
        {
            AlertContent alert = new AlertContent();
            m.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (m.BlogId == 0)
                {
                    BlogPost deger =  mapper.Map<BlogPost>(m);
                    rp.Add(deger);
                    alert.Message = "Kayıt Başarıyla Eklendi.";
                }
                else
                {
                    BlogPost blogUpdate = mapper.Map<BlogPost>(m);
                    rp.Update(blogUpdate);
                    alert.Message = "Kayıt Başarıyla Güncellendi.";
                }
            }
            
            
            return Json(alert);
        }
        [HttpPost]
        [Area("Admin")]
        public IActionResult UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var imagePath = "/uploads/" + file.FileName;
                return Json(imagePath);
            }
            return Json(null);
        }
    }
}

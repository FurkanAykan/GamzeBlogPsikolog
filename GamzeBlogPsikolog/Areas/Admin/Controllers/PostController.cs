using AutoMapper;
using GamzeBlogPsikolog.Context;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using GamzeBlogPsikolog.Models;
using GamzeBlogPsikolog.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly IGenericRepostory<BlogPost> rp;
        private readonly IMapper mapper;
        private readonly IBlogPostService blogPostService;
        public PostController(IGenericRepostory<BlogPost> rp, IMapper mapper, IBlogPostService blogPostService)
        {
            this.rp = rp;
            this.mapper = mapper;
            this.blogPostService = blogPostService;
        }
        [Area("Admin")]
        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            int pageNo = id ?? 1;
            int pageSize = 10;
            ViewBag.pagenumber = (pageNo * 10) - pageSize;
            AdminBlogList bloglist = await blogPostService.GetAll(pageNo, pageSize);
            bloglist.BlogList = bloglist.BlogList.OrderByDescending(x => x.CreateDate).ToList();
            return View(bloglist);
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
        public IActionResult UploadImage(int id,IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
           
            if (file != null && file.Length > 0)
            {
                // wwwroot dizini içindeki uploads ve thumb klasörlerini oluştur
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                var thumbFolder = Path.Combine(webHostEnvironment.WebRootPath, "thumb");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (!Directory.Exists(thumbFolder))
                {
                    Directory.CreateDirectory(thumbFolder);
                }

                var fileName = Path.GetFileName(file.FileName);
                var imagePath = Path.Combine(uploadsFolder, fileName);

                // Dosyayı kaydet
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Resmi yeniden boyutlandır
                var thumbImagePath = Path.Combine(thumbFolder, fileName);
                using (var image = Image.Load(imagePath))
                {
                    // İstenilen boyuta resmi yeniden boyutlandır
                    var width = 100; // İstediğiniz genişlik
                    var height = 100; // İstediğiniz yükseklik
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(width, height),
                        Mode = ResizeMode.Max
                    }));

                    // Yeniden boyutlandırılmış resmi kaydet
                    image.Save(thumbImagePath);
                }

                return Json(new { OriginalPath = "/uploads/" + fileName, ThumbnailPath = "/thumb/" + fileName });
            }

            return Json(null);
        }
     
        [HttpGet]
        [Area("Admin")]
        public async Task<IActionResult> GetData(int id)
        {
            var deger = await rp.GetByIdAsync(x => x.BlogId == id);
            return Json(deger);
        }


        [Area("Admin")]
        [HttpPost]
        public async Task<JsonResult> Delete(int postId)
        {
            var blogPost = await rp.GetByIdAsync(x=>x.BlogId==postId);

            if (blogPost != null)
            {
                if (blogPost.Status)
                {
                    blogPost.Status = false;
                    rp.Update(blogPost);
                    return Json(false);

                }
                else
                {
                    blogPost.Status = true;
                    rp.Update(blogPost);
                    return Json(true);

                }
            }
            return Json(new { success = true });
        }
    }
}

using AutoMapper;
using GamzeBlogPsikolog.Context;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using GamzeBlogPsikolog.Models;
using GamzeBlogPsikolog.Services;
using Microsoft.AspNetCore.Mvc;
using PagedList;

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
        public IActionResult UploadImage(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (file != null && file.Length > 0)
            {
                // wwwroot dizini içindeki uploads klasörüne kaydedelim
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");

                // Eğer uploads klasörü yoksa oluştur
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var imagePath = Path.Combine(uploadsFolder, file.FileName);

                // Dosyayı kaydet
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                // Burada imagePath'i isteğe bağlı olarak "/uploads/" + file.FileName şeklinde kullanabilirsiniz
                return Json("/uploads/" + file.FileName);
            }

            return Json(null);
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

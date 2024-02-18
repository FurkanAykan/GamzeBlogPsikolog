using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GamzeBlogPsikolog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPostService _blogPostService;
        public BlogController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        public async Task<IActionResult> Index(int id, string search)
        {
            if(id == 0)
            {
                id = 1;
            }
            if (search!=null)
            {
               var blogs = await _blogPostService.SearchBlog(search);
                if (blogs.Count()>0)
                {
                    ViewBag.totalItem = blogs.Count();
                    ViewBag.pageNumber = 1;
                    return View(blogs);
                }
            }
            var result = await _blogPostService.GetPaginatedBlogPosts(id);
            var blogList = result.Item1; // Sayfalı blog gönderileri
            var totalItems = result.Item2; // Toplam öğe sayısı
            ViewBag.totalItem = totalItems;
            ViewBag.pageNumber = id;
            return View(blogList);
        }
        [HttpGet]
        public async Task<IActionResult> BlogDetail(int id)
        {
            var blog = await _blogPostService.GetBlogById(id);
      
            return View(blog);
        }
    }
}

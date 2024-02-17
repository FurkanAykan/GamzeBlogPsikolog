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

        public async Task<IActionResult> Index(int pageNumber)
        {
            if(pageNumber==0)
            {
                pageNumber = 1;
            }
            var result = await _blogPostService.GetPaginatedBlogPosts(pageNumber);
            var blogList = result.Item1; // Sayfalı blog gönderileri
            var totalItems = result.Item2; // Toplam öğe sayısı
            ViewBag.totalItem = totalItems;
            ViewBag.pageNumber = pageNumber;
            return View(blogList);
        }
        public async Task<IActionResult> BlogDetail(int id)
        {
            var blog = await _blogPostService.GetBlogById(id);
      
            return View(blog);
        }
    }
}

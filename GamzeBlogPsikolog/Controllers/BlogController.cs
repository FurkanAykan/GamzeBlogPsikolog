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

        public async Task<IActionResult> Index()
        {
            List<BlogPostViewModel> blogList = await _blogPostService.GetAllBlog();
            return View(blogList);
        }
        public async Task<IActionResult> BlogDetail(int id)
        {
            var blog = await _blogPostService.GetBlogById(id);
      
            return View(blog);
        }
    }
}

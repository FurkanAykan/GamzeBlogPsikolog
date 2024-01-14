using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface IBlogPostService
    {
        Task<List<BlogPostViewModel>> GetAllBlog();
        Task<BlogPostViewModel> GetBlogById(int id);
    }
}

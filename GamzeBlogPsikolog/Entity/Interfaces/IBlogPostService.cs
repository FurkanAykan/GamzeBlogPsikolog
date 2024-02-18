using GamzeBlogPsikolog.EntityViewModels;

using System.Linq.Expressions;


namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface IBlogPostService
    {

        Task<(List<BlogPostViewModel> blogPosts, int totalItems)> GetPaginatedBlogPosts(int pageNumber);
        Task<BlogPostViewModel> GetBlogById(int id);

        Task<AdminBlogList> GetAll(int pageNumber, int pageSize, Expression<Func<BlogPost, bool>> filter = null, Func<IQueryable<BlogPost>, IOrderedQueryable<BlogPost>> orderby = null, params Expression<Func<BlogPost, object>>[] includes);
        Task<BlogPostViewModel> RandomPost();
        Task<List<BlogPostViewModel>> LastFiveBlog();
        Task SendMail(BlogPost post);
        Task<List<BlogPostViewModel>> SearchBlog(string search);
    }
}

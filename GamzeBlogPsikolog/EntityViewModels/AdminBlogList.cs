namespace GamzeBlogPsikolog.EntityViewModels
{
    public class AdminBlogList
    {
        public List<BlogPostViewModel> BlogList { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

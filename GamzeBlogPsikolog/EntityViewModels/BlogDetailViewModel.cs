namespace GamzeBlogPsikolog.EntityViewModels
{
    public class BlogDetailViewModel
    {
       public BlogPostViewModel BlogPost { get; set; }   
       public List<CommentViewModel> Comment { get; set; }
    }
}

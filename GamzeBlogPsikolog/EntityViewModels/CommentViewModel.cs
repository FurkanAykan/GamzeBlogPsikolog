using GamzeBlogPsikolog.Entity;

namespace GamzeBlogPsikolog.EntityViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentUserName { get; set; }
        public bool CommentStatus { get; set; }
        public string CommentContent { get; set; }
        public string CommentEmail { get; set; }
        public string CommentImage { get; set; }
        public DateTime CreateDate { get; set; }
        public int BlogPostId { get; set; }
        public BlogPostViewModel BlogPost { get; set; }
        public List<ReplyCommentViewModel> ReplyComments { get; set; }
    }
}

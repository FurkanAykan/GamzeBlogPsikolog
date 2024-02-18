using System.ComponentModel.DataAnnotations;

namespace GamzeBlogPsikolog.Entity
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentUserName { get; set; }
        public bool CommentStatus { get; set; }
        public bool IsSeen { get; set; }
        public string CommentContent { get; set; }
        public string CommentEmail { get; set; }
        public string CommentImage { get; set; }
        public DateTime CreateDate { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public List<ReplyComment> ReplyComments { get; set; }
    }
}

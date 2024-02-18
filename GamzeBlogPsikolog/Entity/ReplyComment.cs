using System.ComponentModel.DataAnnotations;

namespace GamzeBlogPsikolog.Entity
{
    public class ReplyComment
    {
        [Key]
        public int ReplyCommentId { get; set; }
        public string ReplyCommentUserName { get; set; }
        public bool ReplyCommentStatus { get; set; }
        public bool IsSeen { get; set; }
        public string ReplyCommentContent { get; set; }
        public string ReplyCommentEmail { get; set; }
        public string ReplyCommentImage { get; set; }
        public DateTime ReplyCreateDate { get; set; }
        public int CommentId { get; set; }     
    }
}

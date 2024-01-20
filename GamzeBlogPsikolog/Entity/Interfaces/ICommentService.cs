using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface ICommentService
    {
        Task<string> AddComment(CommentViewModel comment); 
        Task<string> AddReplyComment(ReplyCommentViewModel replyComment); 
        Task<List<CommentViewModel>> GetAllCommentByPostId(int postId); 
        Task<List<CommentViewModel>> GetAllCommentByBlogIdAdmin(int id); 
        Task<CommentViewModel> GetCommentByIdAdmin(int id); 
        Task EditComment(CommentViewModel comment);
        Task<ReplyCommentViewModel> GetReplyCommentByIdAdmin(int id);
        Task EditReplyComment(ReplyCommentViewModel comment);

    }
}

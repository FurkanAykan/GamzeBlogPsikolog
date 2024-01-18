using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface ICommentService
    {
        Task<string> AddComment(CommentViewModel comment); 
        Task<string> AddReplyComment(ReplyCommentViewModel replyComment); 
        Task<List<CommentViewModel>> GetAllCommentByPostId(int postId); 
        Task<List<CommentViewModel>> GetAllCommentAdmin(); 
        Task<CommentViewModel> GetCommentByIdAdmin(int id); 
 
 
    }
}

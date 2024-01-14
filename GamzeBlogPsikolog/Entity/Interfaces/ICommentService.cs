using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface ICommentService
    {
        Task<string> AddComment(CommentViewModel comment); 
 
    }
}

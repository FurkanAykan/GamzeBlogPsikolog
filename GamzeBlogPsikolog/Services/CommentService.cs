using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepostory<Comment> _commentRepo;
        private readonly IMapper _mapper;

        public CommentService(IGenericRepostory<Comment> commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }

        public async Task<string> AddComment(CommentViewModel comment)
        {
            try
            {
                Comment newComment = _mapper.Map<Comment>(comment);
                newComment.CommentImage = "baseimage";
                newComment.CommentStatus = false;
                await _commentRepo.Add(newComment);
                return "Ok";
            }
            catch (Exception ex)
            {
                
                return ex.Message;
            }
            
        }
    }
}

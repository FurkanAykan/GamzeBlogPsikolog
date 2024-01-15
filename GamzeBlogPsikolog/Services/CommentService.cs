using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepostory<Comment> _commentRepo;
        private readonly IGenericRepostory<ReplyComment> _replyCommentRepo;
        private readonly IMapper _mapper;

        public CommentService(IGenericRepostory<Comment> commentRepo, IMapper mapper, IGenericRepostory<ReplyComment> replyCommentRepo)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
            _replyCommentRepo = replyCommentRepo;
        }

        public async Task<string> AddComment(CommentViewModel comment)
        {
            try
            {
                Comment newComment = _mapper.Map<Comment>(comment);
                newComment.CommentImage = "baseimage";
                newComment.CommentStatus = false;
                newComment.CreateDate = DateTime.Now;
                await _commentRepo.Add(newComment);
                return "Ok";
            }
            catch (Exception ex)
            {
                
                return ex.Message;
            }
            
        }

        public async Task<string> AddReplyComment(ReplyCommentViewModel replyComment)
        {
            try
            {
                ReplyComment newComment = _mapper.Map<ReplyComment>(replyComment);
                newComment.ReplyCommentImage = "baseimage";
                newComment.ReplyCommentStatus = false;
                newComment.ReplyCreateDate = DateTime.Now;
                await _replyCommentRepo.Add(newComment);
                return "Ok";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<List<CommentViewModel>> GetAllCommentByPostId(int postId)
        {
            var commentlist=await _commentRepo.GetAll(x=>x.BlogPostId == postId && x.CommentStatus==true,null,x=>x.ReplyComments);
            List<CommentViewModel> mappedCommentList = _mapper.Map<List<CommentViewModel>>(commentlist);
            return mappedCommentList;
        }
    }
}

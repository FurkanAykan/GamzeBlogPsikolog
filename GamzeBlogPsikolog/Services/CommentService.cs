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

        public async Task EditComment(CommentViewModel comment)
        {
            var com = await _commentRepo.GetByIdAsync(x => x.CommentId == comment.CommentId);
            com.CommentContent = comment.CommentContent;
            com.CommentStatus = comment.CommentStatus;
            com.CommentEmail = comment.CommentEmail;
            com.CommentUserName = comment.CommentUserName;
             _commentRepo.Update(com);
        }

        public async Task EditReplyComment(ReplyCommentViewModel comment)
        {
            var com = await _replyCommentRepo.GetByIdAsync(x => x.ReplyCommentId == comment.ReplyCommentId);
            com.ReplyCommentContent = comment.ReplyCommentContent;
            com.ReplyCommentStatus = comment.ReplyCommentStatus;
            com.ReplyCommentEmail = comment.ReplyCommentEmail;
            com.ReplyCommentUserName = comment.ReplyCommentUserName;
            _replyCommentRepo.Update(com);
        }

        public async Task<List<CommentViewModel>> GetAllCommentByBlogIdAdmin(int id)
        {
            var commentlist = await _commentRepo.GetAll(x => x.BlogPostId == id, null, x => x.ReplyComments);
            List<CommentViewModel> mappedCommentList = _mapper.Map<List<CommentViewModel>>(commentlist);
            return mappedCommentList;
        }

        public async Task<List<CommentViewModel>> GetAllCommentByPostId(int postId)
        {
            var commentlist = await _commentRepo.GetAll(x => x.BlogPostId == postId && x.CommentStatus == true, null, x => x.ReplyComments);
            List<CommentViewModel> mappedCommentList = _mapper.Map<List<CommentViewModel>>(commentlist);
            return mappedCommentList;
        }

        public async Task<CommentViewModel> GetCommentByIdAdmin(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(x => x.CommentId == id, null, x => x.ReplyComments);
            CommentViewModel mappedComment = _mapper.Map<CommentViewModel>(comment);
            return mappedComment;
        }

        public async Task<ReplyCommentViewModel> GetReplyCommentByIdAdmin(int id)
        {
            var replyComment = await _replyCommentRepo.GetByIdAsync(x => x.ReplyCommentId == id);
            ReplyCommentViewModel mappedComment = _mapper.Map<ReplyCommentViewModel>(replyComment);
            return mappedComment;
        }
    }
}

using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<JsonResult> AddComment(CommentViewModel model)
        {
            string msg= await _commentService.AddComment(model);           
            return Json(msg);
        } 
        [HttpPost]
        public async Task<JsonResult> AddReplyComment(ReplyCommentViewModel model)
        {
            string msg = await _commentService.AddReplyComment(model);
            return Json(msg);
        }
    }
}

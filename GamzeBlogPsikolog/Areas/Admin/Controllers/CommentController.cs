using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Area("Admin")]
        public async Task<IActionResult> AllComment(int id)
        {
           var commentList= await _commentService.GetAllCommentByBlogIdAdmin(id);
            return View(commentList);
        } 
        [Area("Admin")]
        [HttpGet]
        public async Task<JsonResult> GetCommentById(int id)
        {
           CommentViewModel model= await _commentService.GetCommentByIdAdmin(id);
            return Json(model);
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<JsonResult> EditComment([FromBody]CommentViewModel comment)
        {
            await _commentService.EditComment(comment);
            return Json("");
        }
        [Area("Admin")]
        [HttpGet]
        public async Task<JsonResult> GetReplyCommentById(int id)
        {
            ReplyCommentViewModel model = await _commentService.GetReplyCommentByIdAdmin(id);
            return Json(model);
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<JsonResult> EditReplyComment([FromBody] ReplyCommentViewModel comment)
        {
            await _commentService.EditReplyComment(comment);
            return Json("");
        }
    }
}

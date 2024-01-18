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
        public async Task<IActionResult> AllComment()
        {
           var commentList= await _commentService.GetAllCommentAdmin();
            return View(commentList);
        } 
        [Area("Admin")]
        [HttpGet]
        public async Task<JsonResult> GetCommentById(int id)
        {
           CommentViewModel model= await _commentService.GetCommentByIdAdmin(id);
            return Json(model);
        }
    }
}

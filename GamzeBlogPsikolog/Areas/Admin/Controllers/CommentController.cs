using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
     
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        public async Task<IActionResult> AllComment(int id)
        {
           var commentList= await _commentService.GetAllCommentByBlogIdAdmin(id);
            return View(commentList);
        } 

        [HttpGet]
        public async Task<JsonResult> GetCommentById(int id)
        {
           CommentViewModel model= await _commentService.GetCommentByIdAdmin(id);
            return Json(model);
        }
   
        [HttpPost]
        public async Task<JsonResult> EditComment([FromBody]CommentViewModel comment)
        {
            await _commentService.EditComment(comment);
            return Json("");
        }

        [HttpGet]
        public async Task<JsonResult> GetReplyCommentById(int id)
        {
            ReplyCommentViewModel model = await _commentService.GetReplyCommentByIdAdmin(id);
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> EditReplyComment([FromBody] ReplyCommentViewModel comment)
        {
            await _commentService.EditReplyComment(comment);
            return Json("");
        }

        public async Task<IActionResult> AllMessage()
        {
            var mesageList= await _commentService.GetAllMessage();
            return View(mesageList);
        }
       
    }
}

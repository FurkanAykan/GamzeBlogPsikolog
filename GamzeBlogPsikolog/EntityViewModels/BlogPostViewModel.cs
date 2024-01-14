using GamzeBlogPsikolog.Entity;
using System.ComponentModel.DataAnnotations;

namespace GamzeBlogPsikolog.EntityViewModels
{
    public class BlogPostViewModel
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public string? BlogTag { get; set; }
        public string? BlogImage { get; set; }
        public string? BlogThumbImage { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}

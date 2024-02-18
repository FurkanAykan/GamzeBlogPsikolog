
namespace GamzeBlogPsikolog.EntityViewModels
{
    public class HomeViewModel
    {
        public List<SliderViewModel> Slider { get; set; }
        public List<BlogPostViewModel> Posts { get; set; }
        public BlogPostViewModel RandomPost { get; set; }
        public List<SuggestionViewModel> Suggestions { get; set; }
    }
}

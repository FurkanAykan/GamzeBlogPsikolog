
namespace GamzeBlogPsikolog.EntityViewModels
{
    public class SuggestionViewModel
    {
        public int SuggestionId { get; set; }
        public string? SuggestionTitle { get; set; }
        public string? SuggestionDescription { get; set; }
        public string? SuggestionImage { get; set; }
        public string? MovieUrl { get; set; }
        public int OgrId { get; set; }
        public bool Statu { get; set; }
    }
}

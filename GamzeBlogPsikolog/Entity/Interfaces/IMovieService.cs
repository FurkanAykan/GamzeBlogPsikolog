using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface IMovieService
    {
        Task<(List<SuggestionViewModel> Movies, int totalItems)> GetPaginatedSuggestion(int pageNumber, int ogrId);
        Task<SuggestionViewModel> GetById(int id);
        Task<List<SuggestionViewModel>> GetRandom();
    }
}

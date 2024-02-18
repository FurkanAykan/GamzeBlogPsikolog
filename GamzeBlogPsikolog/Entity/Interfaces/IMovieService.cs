﻿using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface IMovieService
    {
        Task<(List<SuggestionViewModel> Movies, int totalItems)> GetPaginatedSuggestion(int pageNumber, int ogrId);
        Task<SuggestionViewModel> GetById(int id, int ogrId);
        Task<List<SuggestionViewModel>> GetAlltrue();
        Task<List<SuggestionViewModel>> GetAll(int id);
        Task<SuggestionViewModel> GetByIdAsync(int id);
        Task<int> AddSuggestion(SuggestionViewModel s);
        Task EditSuggestion(SuggestionViewModel s);
        Task<SuggestionViewModel> DeleteSuggestion(int id);
    }
}

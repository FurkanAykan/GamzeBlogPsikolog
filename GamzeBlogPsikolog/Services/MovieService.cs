using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Services
{
    public class MovieService : IMovieService
    {
        private readonly IGenericRepostory<Suggestion> _Movierepo;
        private readonly IMapper _mapper;

        public MovieService(IGenericRepostory<Suggestion> movierepo, IMapper mapper)
        {
            _Movierepo = movierepo;
            _mapper = mapper;
        }

        public async Task<(List<SuggestionViewModel> Movies, int totalItems)> GetPaginatedSuggestion(int pageNumber ,int ogrId)
        {
            int pageSize = 6; // Her sayfada gösterilecek blog gönderisi sayısı

            var movieList = await _Movierepo.GetAll(x=>x.OgrId==ogrId); // Tüm film listesini al
            var totalItems = movieList.ToList().Count; // Toplam film sayısı

            var paginatedMovies = movieList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); // Belirli sayfa ve sayfa boyutuna göre filmleri al

            var mappedMovies = _mapper.Map<List<SuggestionViewModel>>(paginatedMovies); // ViewModel'e dönüştür

            return (mappedMovies, totalItems); // Sayfalanan filmleri ve toplam öğe sayısını döndür
        }
        public async Task<SuggestionViewModel> GetById(int id, int ogrId)
        {
            var moive = await _Movierepo.GetByIdAsync(x => x.SuggestionId == id && x.OgrId==ogrId);
            var mappedMovie = _mapper.Map<SuggestionViewModel>(moive);
            return mappedMovie;
        }

        public async Task<List<SuggestionViewModel>> GetAlltrue()
        {
            var suggestions = await _Movierepo.GetAll(x => x.Statu == true);
            var mapsuggestions = _mapper.Map<List<SuggestionViewModel>>(suggestions);
            return mapsuggestions;
        }
        public async Task<List<SuggestionViewModel>> GetAll(int id)
        {
            var suggestions = await _Movierepo.GetAll(x=>x.OgrId==id);
            var mapsuggestions = _mapper.Map<List<SuggestionViewModel>>(suggestions);
            return mapsuggestions;
        }
        public async Task<SuggestionViewModel> GetByIdAsync(int id)
        {
            var suggestion = await _Movierepo.GetByIdAsync(x => x.SuggestionId == id);
            var mapsuggestion = _mapper.Map<SuggestionViewModel>(suggestion);
            return mapsuggestion;
        }
        public async Task<int> AddSuggestion(SuggestionViewModel s)
        {
            try
            {
                if (s.SuggestionId == 0)
                {
                    if (s.OgrId!=1)
                    {
                        s.MovieUrl = null;
                    }
                   
                    Suggestion newSuggestion = _mapper.Map<Suggestion>(s);
                    await _Movierepo.Add(newSuggestion);
                    return newSuggestion.OgrId;
                }
                else
                {
                    await EditSuggestion(s);
                    return s.OgrId;

                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task EditSuggestion(SuggestionViewModel s)
        {
            var com = await _Movierepo.GetByIdAsync(x => x.SuggestionId == s.SuggestionId);
            com.SuggestionDescription = s.SuggestionDescription;
            com.SuggestionImage = s.SuggestionImage;
            com.SuggestionTitle = s.SuggestionTitle;
            com.MovieUrl = s.MovieUrl;
            com.Statu = true;
            _Movierepo.Update(com);
        }

        public async Task<SuggestionViewModel> DeleteSuggestion(int id)
        {
            var com = await _Movierepo.GetByIdAsync(x => x.SuggestionId == id);
            if (com.Statu)
            {
                com.Statu = false;
            }
            else
            {
                com.Statu = true;
            }
            _Movierepo.Update(com);
            SuggestionViewModel newSuggestion = _mapper.Map<SuggestionViewModel>(com);

            return newSuggestion;
        }

    }
}

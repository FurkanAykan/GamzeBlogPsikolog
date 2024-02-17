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
    }
}

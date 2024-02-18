using GamzeBlogPsikolog.Entity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Controllers
{
   
    public class SuggestionController : Controller
    {
        private readonly  IMovieService _movieService;

        public SuggestionController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Movies(int id)
        {
            if (id==0)
            {
                id = 1;
            }
            var movies = await _movieService.GetPaginatedSuggestion(id,1);
            ViewBag.totalItem = movies.totalItems;
            ViewBag.pageNumber = id;
            return View(movies.Movies);
        }
        public async Task<IActionResult> MovieDetail(int id)
        {
            var movie = await _movieService.GetById(id);
            return View(movie);
        }
        public async Task<IActionResult> Games(int id)
        {
            if (id == 0)
            {
                id = 1;
            }
            var game = await _movieService.GetPaginatedSuggestion(id, 2);
            ViewBag.totalItem = game.totalItems;
            ViewBag.pageNumber = id;
            return View(game.Movies);
        }
        public async Task<IActionResult> GameDetail(int id)
        {
            var game = await _movieService.GetById(id);
            return View(game);
        }
        public async Task<IActionResult> Books(int id)
        {
            if (id == 0)
            {
                id = 1;
            }
            var game = await _movieService.GetPaginatedSuggestion(id, 3);
            ViewBag.totalItem = game.totalItems;
            ViewBag.pageNumber = id;
            return View(game.Movies);
        }
        public async Task<IActionResult> BookDetail(int id)
        {
            var game = await _movieService.GetById(id);
            return View(game);
        }
    }
}

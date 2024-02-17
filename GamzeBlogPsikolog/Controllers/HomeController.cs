using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using GamzeBlogPsikolog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GamzeBlogPsikolog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepostory<Slider> _slider;
        private readonly IBlogPostService _blogService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IGenericRepostory<Slider> slider, IMapper mapper, IBlogPostService blogService)
        {
            _logger = logger;
            _slider = slider;
            _mapper = mapper;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var getSlider = await _slider.GetAll();
            List<SliderViewModel> sliderList = _mapper.Map<List<SliderViewModel>>(getSlider);
            HomeViewModel model = new HomeViewModel()
            {
                Slider = sliderList,
                RandomPost = await _blogService.RandomPost(),
                Posts= await _blogService.LastFiveBlog()                
            };
            return View(model);
 
        }
    }
}

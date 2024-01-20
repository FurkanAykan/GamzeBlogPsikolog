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
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IGenericRepostory<Slider> slider, IMapper mapper)
        {
            _logger = logger;
            _slider = slider;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var getSlider = await _slider.GetAll();
            List<SliderViewModel> sliderList = _mapper.Map<List<SliderViewModel>>(getSlider);
            return View(sliderList);
 
        }
    }
}

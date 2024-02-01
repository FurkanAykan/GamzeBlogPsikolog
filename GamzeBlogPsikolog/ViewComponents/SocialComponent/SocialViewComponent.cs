using AutoMapper;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.EntityViewModels;
using Microsoft.AspNetCore.Mvc;
using GamzeBlogPsikolog.Controllers;

namespace GamzeBlogPsikolog.ViewComponents.SocialViewComponent
{
    public class SocialViewComponent:ViewComponent
    {
        private readonly IGenericRepostory<Social> _social;
        private readonly IMapper _mapper;

        public SocialViewComponent(IGenericRepostory<Social> social,IMapper mapper)
        {
            _social = social;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var socialList = await _social.GetAll();
            var social = socialList.FirstOrDefault();
            var socials = _mapper.Map<SocialViewModel>(social);
            return View(socials); 
        }
    }
}

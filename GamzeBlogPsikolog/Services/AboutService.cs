using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Services
{
    public class AboutService : IAboutService
    {
        private readonly IGenericRepostory<BlogPost> _postRepo;
        private readonly IGenericRepostory<About> _aboutRepo;
        private readonly IMapper _mapper;

        public AboutService(IGenericRepostory<BlogPost> postRepo, IMapper mapper, IGenericRepostory<About> aboutRepo)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _aboutRepo = aboutRepo;
        }

        public async Task<AboutPageViewModel> GetAboutAsync()
        {
            AboutPageViewModel aboutPageViewModel = new AboutPageViewModel();
            var allPosts = await _postRepo.GetAll();
            var lastThreePosts = allPosts.TakeLast(3).ToList();
            var mappedLastThreePosts = _mapper.Map<List<BlogPostViewModel>>(lastThreePosts);
            aboutPageViewModel.Posts = mappedLastThreePosts;

            var about = await _aboutRepo.GetAll();
            var singleAbout = about.Take(1).FirstOrDefault();
            var mappedAbout = _mapper.Map<AboutViewModel>(singleAbout);
            aboutPageViewModel.About = mappedAbout;
            return aboutPageViewModel;
        }
    }
}

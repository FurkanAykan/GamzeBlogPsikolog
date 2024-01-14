using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IGenericRepostory<BlogPost> _postory;
        private readonly IMapper _mapper;

        public BlogPostService(IGenericRepostory<BlogPost> postory, IMapper mapper)
        {
            _postory = postory;
            _mapper = mapper;
        }

        public async Task<List<BlogPostViewModel>> GetAllBlog()
        {
            var blogList = await _postory.GetAll();
            return _mapper.Map<List<BlogPostViewModel>>(blogList);
        }

        public async Task<BlogPostViewModel> GetBlogById(int id)
        {
           var blog = await _postory.GetByIdAsync(x=>x.BlogId==id);
            return _mapper.Map<BlogPostViewModel>(blog);
        }
    }
}

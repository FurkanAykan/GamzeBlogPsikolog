using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<BlogPost, BlogPostViewModel>().ReverseMap();
        }
    }
}

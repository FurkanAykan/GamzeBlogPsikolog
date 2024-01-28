﻿using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<BlogPost, BlogPostViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<ReplyComment, ReplyCommentViewModel>().ReverseMap();
            CreateMap<Slider, SliderViewModel>().ReverseMap();
            CreateMap<Social, SocialViewModel>().ReverseMap();
        }
    }
}

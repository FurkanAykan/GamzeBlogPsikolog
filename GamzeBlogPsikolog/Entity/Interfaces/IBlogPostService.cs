﻿using GamzeBlogPsikolog.EntityViewModels;
using System.Linq.Expressions;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface IBlogPostService
    {
        Task<AdminBlogList> GetAll(int pageNumber, int pageSize, Expression<Func<BlogPost, bool>> filter = null, Func<IQueryable<BlogPost>, IOrderedQueryable<BlogPost>> orderby = null, params Expression<Func<BlogPost, object>>[] includes);
    }
}

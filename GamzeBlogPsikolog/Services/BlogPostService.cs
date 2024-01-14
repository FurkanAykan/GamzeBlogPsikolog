using AutoMapper;
using GamzeBlogPsikolog.Context;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GamzeBlogPsikolog.Services
{
    public class BlogPostService:IBlogPostService
    {
        private readonly GamzeBlogContext _context;
        private DbSet<BlogPost> _dbSet;
        private readonly IMapper mapper;
        public BlogPostService(GamzeBlogContext context, IMapper mapper)
        {
            this._context = context;
            _dbSet = _context.Set<BlogPost>();
            this.mapper = mapper;
        }
        public async Task<AdminBlogList> GetAll(int pageNumber, int pageSize, Expression<Func<BlogPost, bool>> filter = null, Func<IQueryable<BlogPost>, IOrderedQueryable<BlogPost>> orderby = null, params Expression<Func<BlogPost, object>>[] includes)
        {
            IQueryable<BlogPost> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            foreach (var table in includes)
            {
                query = query.Include(table);
            }

            // Toplam kayıt sayısını al
            int totalRecords = await query.CountAsync();

            // Sayfalama işlemi için skip ve take ekleniyor
            query = query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize);

            // Toplam sayfa sayısını hesapla
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var data = await query.ToListAsync();
            List<BlogPostViewModel> blog = mapper.Map<List<BlogPostViewModel>>(data);
            // PageResult nesnesini oluştur ve döndür
            return new AdminBlogList
            {
                PageNumber = pageNumber,
                PageSize = totalPages,
                BlogList = blog
            };
        }
    }
}

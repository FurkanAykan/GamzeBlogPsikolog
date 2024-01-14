using AutoMapper;

using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;

using GamzeBlogPsikolog.Context;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace GamzeBlogPsikolog.Services
{
    public class BlogPostService : IBlogPostService
    {

        private readonly IGenericRepostory<BlogPost> _postory;
        private readonly IMapper _mapper;
        private readonly GamzeBlogContext _context;
        private DbSet<BlogPost> _dbSet;

        public BlogPostService(IGenericRepostory<BlogPost> postory, IMapper mapper,GamzeBlogContext context)
        {
            _postory = postory;
            _mapper = mapper;
            this._context = context;
             _dbSet = _context.Set<BlogPost>();
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

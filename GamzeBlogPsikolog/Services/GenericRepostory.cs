﻿using GamzeBlogPsikolog.Context;
using GamzeBlogPsikolog.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GamzeBlogPsikolog.Services
{
    public class GenericRepostory<T> : IGenericRepostory<T> where T : class
    {
        private readonly GamzeBlogContext _context;
        private DbSet<T> _dbSet;

        public GenericRepostory(GamzeBlogContext context)
        {
            this._context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity); //async kayıt yapma
            await _context.SaveChangesAsync();
        }
        public void Delete(T entity)
        {
            if (entity.GetType().GetProperty("IsDeleted") != null) //silinmek istenen verinin isDeleted kolunu var mı diye bakar varsa değerini true yapar
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true); //değeri true yapar
                _dbSet.Update(entity);
                _context.SaveChanges();

            }
            else
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();

            }

        }
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);  //nesneyi bul
            this.Delete(entity);  //silmek için gönder
            _context.SaveChanges();

        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();

        }
        // filtre(x=>x) -- sıralama -- birleştirilecek tablolar
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;  //tabloyu alır filtreleri uygulayarak filtrelenmiş verileri dödürür
            if (filter != null)  //filtre varsa
            {
                query = query.Where(filter);
            }
            if (orderby != null)  //sıralama istenmişse
            {
                query = orderby(query);
            }
            foreach (var table in includes)  //ilişkili tablolar istenmişse  Eager loadind
            {
                query = query.Include(table);
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();  //EF verileri takip etmiyor değiştirmeyecemiz için
        }
        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;  //tabloyu alır filtreleri uygulayarak filtrelenmiş verileri dödürür
            if (filter != null)  //filtre varsa
            {
                query = query.Where(filter);
            }
            if (orderby != null)  //sıralama istenmişse
            {
                query = orderby(query);
            }
            foreach (var table in includes)  //ilişkili tablolar istenmişse  Eager loadind
            {
                query = query.Include(table);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}

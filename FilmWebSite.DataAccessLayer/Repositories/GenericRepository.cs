using FilmWebSite.Core.Repositories;
using FilmWebSite.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FilmWebSiteContext _context;
        
        public GenericRepository(FilmWebSiteContext context)
        {
            _context = context;
        }

        public  void Add(T entity)
        {
             _context.Set<T>().Add(entity);
        }

        public  void AddRange(IEnumerable<T> entities)
        {
             _context.Set<T>().AddRange(entities);
        }

        public  bool Any(Expression<Func<T, bool>> expression)
        {
            return  _context.Set<T>().Any(expression);
        }

        public  IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

        public  T GetById(int id)
        {
            return  _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
    }
}

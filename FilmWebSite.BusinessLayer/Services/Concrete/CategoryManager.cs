using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Context;
using FilmWebSite.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly FilmWebSiteContext _context;
        public CategoryManager(FilmWebSiteContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);

            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public ICollection<Film> GetFilmsByCategory(int categoryId)
        {
            return _context.FilmCategories.Where(fc=>fc.CategoryId == categoryId).Select(fc=>fc.Film).ToList();

        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }
    }
}

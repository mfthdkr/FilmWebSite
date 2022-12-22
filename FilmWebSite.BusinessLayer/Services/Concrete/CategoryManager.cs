using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Context;
using FilmWebSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmWebSite.Core.Repositories;
using FilmWebSite.Core.UnitOfWorks;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFilmCategoryRepository _filmCategoryRepository;
        public CategoryManager( ICategoryRepository categoryRepository, IFilmCategoryRepository filmCategoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _filmCategoryRepository = filmCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public bool CategoryExists(int categoryId)
        {
            return _categoryRepository.Any(c => c.Id == categoryId);

        }

        public bool CreateCategory(Category category)
        {
            _categoryRepository.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {   
            return _categoryRepository.GetAll().ToList();
          
        }

        public Category GetCategory(int categoryId)
        {   
            return _categoryRepository.GetById(categoryId);
        }

        public ICollection<Film> GetFilmsByCategory(int categoryId)
        {   
            return _filmCategoryRepository.Where(fc=>fc.CategoryId == categoryId)
                .Select(fc=>fc.Film)
                .ToList();

        }

        public bool Save()
        {
          return  _unitOfWork.Commit();
        }

        public bool UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _categoryRepository.Remove(category);
            return Save();
        }
    }
}

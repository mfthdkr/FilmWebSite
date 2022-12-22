using FilmWebSite.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Abstract
{
    public interface ICategoryService
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        ICollection<Film> GetFilmsByCategory(int categoryId);   
        bool CategoryExists(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}

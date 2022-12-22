using FilmWebSite.Core.Entities;
using FilmWebSite.Core.Repositories;
using FilmWebSite.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.DataAccessLayer.Repositories
{
    public class FilmCategoryRepository : GenericRepository<FilmCategory>, IFilmCategoryRepository
    {
        public FilmCategoryRepository(FilmWebSiteContext context) : base(context)
        {
        }
    }
}

using FilmWebSite.Core.UnitOfWorks;
using FilmWebSite.DataAccessLayer.Context;

namespace FilmWebSite.DataAccessLayer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FilmWebSiteContext _context;

        public UnitOfWork(FilmWebSiteContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}

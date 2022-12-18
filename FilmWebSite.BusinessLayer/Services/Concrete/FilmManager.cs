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
    public class FilmManager : IFilmService
    {
        private readonly FilmWebSiteContext _context;

        public FilmManager(FilmWebSiteContext context)
        {
            _context = context;
        }

        public bool CreateFilm(int actorId, int categoryId, Film film)
        {
            var actor = _context.Actors.Where(a => a.Id == actorId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var filmActor = new FilmActor()
            {
                Actor = actor,
                Film = film,
            };
            _context.Add(filmActor);

            var filmCategory = new FilmCategory()
            {
                Category = category,
                Film = film
            };
            _context.Add(filmCategory);

            _context.Add(film);

            return Save();
        }

        public bool DeleteFilm(Film film)
        {
            _context.Remove(film);
            return Save();
        }

        public bool FilmExists(int filmId)
        {
            return _context.Films.Any(f => f.Id == filmId);
        }

        public Film GetFilm(int filmId)
        {
            return _context.Films.Where(f => f.Id == filmId).FirstOrDefault();
        }

        public Film GetFilm(string filmName)
        {
            return _context.Films.Where(f => f.Name.ToLower() == filmName.ToLower()).FirstOrDefault();
        }

        public decimal GetFilmRating(int filmId)
        {
            var comments = _context.Comments.Where(c => c.Film.Id == filmId);

            if (comments.Count() <= 0)
                return 0;

            return (decimal)comments.Sum(c => c.Rating) / comments.Count();
        }

        public ICollection<Film> GetFilms()
        {
            return _context.Films.OrderByDescending(f => f.ReleaseDate).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFilm(Film film)
        {
            _context.Update(film);
            return Save();
        }
    }
}

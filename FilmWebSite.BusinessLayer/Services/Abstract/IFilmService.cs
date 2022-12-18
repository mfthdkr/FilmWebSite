using FilmWebSite.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Abstract
{
    public interface IFilmService
    {
        ICollection<Film> GetFilms();
        Film GetFilm(int filmId);
        Film GetFilm(string filmName);
        decimal GetFilmRating(int filmId);
        bool FilmExists(int filmId);
        bool CreateFilm(int actorId, int categoryId, Film film);
        bool UpdateFilm(Film film);
        bool DeleteFilm(Film film);
        bool Save();
    }
}

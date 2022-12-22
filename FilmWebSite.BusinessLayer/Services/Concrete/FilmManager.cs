using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.Core.Entities;
using FilmWebSite.Core.Repositories;
using FilmWebSite.Core.UnitOfWorks;
using FilmWebSite.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class FilmManager : IFilmService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilmRepository _filmRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IActorRepository _actorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFilmActorRepository _filmActorRepository;
        private readonly IFilmCategoryRepository _filmCategoryRepository;
        public FilmManager( IFilmRepository filmRepository, ICommentRepository commentRepository, IUnitOfWork unitOfWork, IActorRepository actorRepository, ICategoryRepository categoryRepository, IFilmActorRepository filmActorRepository, IFilmCategoryRepository filmCategoryRepository)
        {
            _filmRepository = filmRepository;
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            _filmActorRepository = filmActorRepository;
            _filmCategoryRepository = filmCategoryRepository;
        }

        public bool CreateFilm(int actorId, int categoryId, Film film)
        {
            var actor = _actorRepository.GetById(actorId);
            var category= _categoryRepository.GetById(categoryId);

            var filmActor = new FilmActor()
            {
                Actor = actor,
                Film = film,
            };
            _filmActorRepository.Add(filmActor);

            var filmCategory = new FilmCategory()
            {
                Category = category,
                Film = film
            };
            _filmCategoryRepository.Add(filmCategory);

            _filmRepository.Add(film);

            return Save();
        }

        public bool DeleteFilm(Film film)
        {
            _filmRepository.Remove(film);
            return Save();
        }

        public  bool FilmExists(int filmId)
        {
            return  _filmRepository.Any(f => f.Id.Equals(filmId));

        }

        public  Film GetFilm(int filmId)
        {
            return  _filmRepository.Where(f => f.Id.Equals(filmId)).FirstOrDefault();

        }

        public Film GetFilm(string filmName)
        {
            return  _filmRepository.Where(f => f.Name.ToLower() == filmName.ToLower())
                .FirstOrDefault();
        }

        public decimal GetFilmRating(int filmId)
        {
            var comments = _commentRepository.Where(c => c.Film.Id == filmId);

            if (comments.Count() <= 0)
                return 0;

            return (decimal)comments.Sum(c => c.Rating) / comments.Count();
        }

        public ICollection<Film> GetFilms()
        {
            return _filmRepository.GetAll().OrderByDescending(f => f.ReleaseDate).ToList();

        }

        public bool Save()
        {
            return _unitOfWork.Commit();
        }

        public bool UpdateFilm(Film film)
        {
            _filmRepository.Update(film);
            return Save();
        }
    }
}

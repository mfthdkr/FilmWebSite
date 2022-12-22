using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.Core.Entities;
using FilmWebSite.Core.Repositories;
using FilmWebSite.Core.UnitOfWorks;
using FilmWebSite.DataAccessLayer.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class ActorManager : IActorService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActorRepository _actorRepository;
        private readonly IFilmActorRepository _filmActorRepository;
        public ActorManager( IActorRepository actorRepository, IUnitOfWork unitOfWork, IFilmActorRepository filmActorRepository)
        {
            _actorRepository = actorRepository;
            _unitOfWork = unitOfWork;
            _filmActorRepository = filmActorRepository;
        }

        public bool ActorExists(int actorId)
        {
            return _actorRepository.Any(a => a.Id == actorId);

        }

        public bool CreateActor(Actor actor)
        {
            _actorRepository.Add(actor);
            return _unitOfWork.Commit();
        }

        public bool DeleteActor(Actor actor)
        {
            _actorRepository.Remove(actor);
            return _unitOfWork.Commit();
        }

        public Actor GetActor(int actorId)
        {
            return _actorRepository.GetById(actorId);
        }

        public ICollection<Actor> GetActors()
        {
            return _actorRepository.GetAll().ToList();

        }

        public ICollection<Actor> GetActorsOfAFilm(int filmId)
        {
            return _filmActorRepository.Where(fa => fa.FilmId == filmId)
                .Select(fa => fa.Actor)
                .ToList();

        }

        public ICollection<Film> GetFilmsOfAActor(int actorId)
        {
            return _filmActorRepository.Where(fa => fa.ActorId.Equals(actorId))
                .Select(fa => fa.Film)
                .ToList();

        }

        public bool Save()
        {
            return _unitOfWork.Commit();
        }

        public bool UpdateActor(Actor actor)
        {
            _actorRepository.Update(actor);
            return Save();
        }
    }
}

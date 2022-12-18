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
    public class ActorManager : IActorService
    {
        private readonly FilmWebSiteContext _context;

        public ActorManager(FilmWebSiteContext context)
        {
            _context = context;
        }

        public bool ActorExists(int actorId)
        {
            return _context.Actors.Any(a => a.Id == actorId);
        }

        public bool CreateActor(Actor actor)
        {
            _context.Add(actor);
            return Save();
        }

        public bool DeleteActor(Actor actor)
        {
            _context.Remove(actor);
            return Save();
        }

        public Actor GetActor(int actorId)
        {
            return _context.Actors.Where(a => a.Id == actorId).FirstOrDefault();
        }

        public ICollection<Actor> GetActors()
        {
            return _context.Actors.ToList();
        }

        public ICollection<Actor> GetActorsOfAFilm(int filmId)
        {
            return _context.FilmActors.Where(fa=>fa.FilmId == filmId).Select(fa=>fa.Actor).ToList();  
        }

        public ICollection<Film> GetFilmsOfAActor(int actorId)
        {
            return _context.FilmActors.Where(fa => fa.ActorId == actorId).Select(fa => fa.Film).ToList();
        }

        public bool Save()
        {
            var saved  = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateActor(Actor actor)
        {
            _context.Update(actor);
            return Save();
        }
    }
}

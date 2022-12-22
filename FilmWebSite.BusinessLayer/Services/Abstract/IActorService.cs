using FilmWebSite.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Abstract
{
    public interface IActorService
    {
        ICollection<Actor> GetActors();
        Actor GetActor(int actorId);
        ICollection<Actor> GetActorsOfAFilm(int filmId);
        ICollection<Film> GetFilmsOfAActor(int actorId);
        bool ActorExists(int actorId);
        bool CreateActor(Actor actor);
        bool UpdateActor(Actor actor);
        bool DeleteActor(Actor actor);
        bool Save();
    }
}

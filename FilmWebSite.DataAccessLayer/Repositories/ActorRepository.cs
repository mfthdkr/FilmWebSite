using FilmWebSite.Core.Entities;
using FilmWebSite.Core.Repositories;
using FilmWebSite.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.DataAccessLayer.Repositories
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(FilmWebSiteContext context) : base(context)
        {
        }
    }
}

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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(FilmWebSiteContext context) : base(context)
        {
        }
    }
}

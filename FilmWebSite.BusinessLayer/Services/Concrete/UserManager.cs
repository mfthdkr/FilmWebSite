using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Context;
using FilmWebSite.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly FilmWebSiteContext _context;

        public UserManager(FilmWebSiteContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public ICollection<Comment> GetCommentsByUser(int userId)
        {
            return _context.Comments.Where(c => c.User.Id == userId).ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Include(u=>u.Comments).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}

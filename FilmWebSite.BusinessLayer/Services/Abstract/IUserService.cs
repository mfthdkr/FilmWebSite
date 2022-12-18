using FilmWebSite.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Abstract
{
    public interface IUserService
    {
        ICollection<User> GetUsers();
        User GetUser(int userId);
        ICollection<Comment> GetCommentsByUser(int userId); 
        bool UserExists(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}

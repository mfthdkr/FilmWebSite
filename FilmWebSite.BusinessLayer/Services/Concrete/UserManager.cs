using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Context;
using FilmWebSite.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmWebSite.Core.UnitOfWorks;
using FilmWebSite.Core.Repositories;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class UserManager : IUserService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        public UserManager( IUnitOfWork unitOfWork, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public bool CreateUser(User user)
        {
            _userRepository.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _userRepository.Remove(user);
            return Save();
        }

        public ICollection<Comment> GetCommentsByUser(int userId)
        {
            return _commentRepository.Where(c => c.User.Id == userId).ToList();

        }

        public User GetUser(int userId)
        {
            return _userRepository.Where(u => u.Id == userId)
                .Include(u => u.Comments)
                .FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public bool Save()
        {
            return _unitOfWork.Commit();
        }

        public bool UpdateUser(User user)
        {
            _userRepository.Update(user);
            return Save();
        }

        public bool UserExists(int userId)
        {
            return _userRepository.Any(u => u.Id == userId);
            
        }
    }
}

using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Context;
using FilmWebSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmWebSite.Core.Repositories;
using FilmWebSite.Core.UnitOfWorks;

namespace FilmWebSite.BusinessLayer.Services.Concrete
{
    public class CommentManager : ICommentService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        public CommentManager( IUnitOfWork unitOfWork, ICommentRepository commentRepository)
        {
            
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
        }

        public bool CommentExists(int commentId)
        {
            return _commentRepository.Any(c => c.Id.Equals(commentId));
           
        }

        public bool CreateComment(Comment comment)
        {
            _commentRepository.Add(comment);
            return Save();
        }

        public bool DeleteComment(Comment comment)
        {
            _commentRepository.Remove(comment);
            return Save();
        }

        public bool DeleteComments(List<Comment> comments)
        {
            _commentRepository.RemoveRange(comments);
            return Save();
        }

        public Comment GetComment(int commentId)
        {
            return _commentRepository.GetById(commentId);
            
        }

        public ICollection<Comment> GetComments()
        {
            return _commentRepository.GetAll().ToList();
            
        }

        public ICollection<Comment> GetCommentsOfAFilm(int filmId)
        {
            return _commentRepository.Where(c => c.Film.Id == filmId).ToList();

        }

        public bool Save()
        {
          return  _unitOfWork.Commit();
        }

        public bool UpdateComment(Comment comment)
        {
            _commentRepository.Update(comment);
            return Save();
        }
    }
}

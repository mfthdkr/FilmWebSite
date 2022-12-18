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
    public class CommentManager : ICommentService
    {
        private readonly FilmWebSiteContext _context;

        public CommentManager(FilmWebSiteContext context)
        {
            _context = context;
        }

        public bool CommentExists(int commentId)
        {
            return _context.Comments.Any(c => c.Id == commentId);
        }

        public bool CreateComment(Comment comment)
        {
            _context.Add(comment);
            return Save();
        }

        public bool DeleteComment(Comment comment)
        {
            _context.Remove(comment);
            return Save();
        }

        public bool DeleteComments(List<Comment> comments)
        {
            _context.RemoveRange(comments);
            return Save();
        }

        public Comment GetComment(int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
        }

        public ICollection<Comment> GetComments()
        {
            return _context.Comments.ToList();
        }

        public ICollection<Comment> GetCommentsOfAFilm(int filmId)
        {
            return _context.Comments.Where(c => c.Film.Id == filmId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateComment(Comment comment)
        {
            _context.Update(comment);
            return Save();
        }
    }
}

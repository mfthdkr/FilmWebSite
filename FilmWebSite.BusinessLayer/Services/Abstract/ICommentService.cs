using FilmWebSite.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Services.Abstract
{
    public interface ICommentService
    {
        ICollection<Comment> GetComments();
        Comment GetComment(int commentId);
        ICollection<Comment> GetCommentsOfAFilm(int filmId);
        bool CommentExists(int commentId);
        bool CreateComment(Comment comment);
        bool UpdateComment(Comment comment);
        bool DeleteComment(Comment comment);
        bool DeleteComments(List<Comment> comments);
        bool Save();
    }
}

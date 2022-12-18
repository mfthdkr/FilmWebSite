using AutoMapper;
using FilmWebSite.BusinessLayer.DTOs;
using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController: Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;
        public CommentController(ICommentService commentService, IMapper mapper, IUserService userService, IFilmService filmService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _userService = userService;
            _filmService = filmService;
        }

        /// <summary>
        /// Bütün yorumları listeler.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetComments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public IActionResult GetComments()
        {
            var comments = _mapper.Map<List<CommentDto>>(_commentService.GetComments());
            
            if (comments == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }


        /// <summary>
        /// Id'si verilen yorumun bilgilerin verir.
        /// </summary>
        /// <param name="commmentId"></param>
        /// <returns></returns>
        [HttpGet("GetComment/{commmentId}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetComment(int commmentId)
        {
            if (!_commentService.CommentExists(commmentId))
                return NotFound();

            var comment = _mapper.Map<CommentDto>(_commentService.GetComment(commmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comment);
        }

        /// <summary>
        /// Bir filme ait bütün yorumları listeler.
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        [HttpGet("GetCommentsOfAFilm/{filmId}")]
        [ProducesResponseType(200, Type = typeof(List<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentsOfAFilm(int filmId)
        {
            var comments = _mapper.Map<List<CommentDto>>(_commentService.GetCommentsOfAFilm(filmId));

            if (comments.Count == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(comments);
        }


        [HttpPost("CreateComment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateComment(
            [FromQuery] int userId, [FromQuery] int filmId,
            [FromBody] CommentDto commentDto)
        {
            if (commentDto == null)
                return BadRequest(ModelState);

            var comment = _mapper.Map<Comment>(commentDto);
            comment.Film = _filmService.GetFilm(filmId);
            comment.User = _userService.GetUser(userId);

            if (!_commentService.CreateComment(comment))
            {
                ModelState.AddModelError("", "Kayıt olurken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }


            return Ok("Kayıt başarıyla oluşturuldu.");
        }


        [HttpPut("{commentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateComment(int commentId, [FromBody] CommentDto commentDto)
        {
            if (commentDto == null)
                return BadRequest(ModelState);

            if (commentId != commentDto.Id)
                return BadRequest(ModelState);

            if (!_commentService.CommentExists(commentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = _mapper.Map<Comment>(commentDto);

            if (!_commentService.UpdateComment(comment))
            {
                ModelState.AddModelError("", "Kayıt oluşurken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




        [HttpDelete("{commentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteComment(int commentId)
        {
            if (!_commentService.CommentExists(commentId))
                return NotFound();

            var comment = _commentService.GetComment(commentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_commentService.DeleteComment(comment))
            {
                ModelState.AddModelError("", "Kayıt silinirken bir hata oluştu.");
            }

            return NoContent();

        }
    }
}

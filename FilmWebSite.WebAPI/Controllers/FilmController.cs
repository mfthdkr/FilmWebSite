using AutoMapper;
using FilmWebSite.BusinessLayer.DTOs;
using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public FilmController(IFilmService filmService, IMapper mapper, ICommentService commentService)
        {
            _filmService = filmService;
            _mapper = mapper;
            _commentService = commentService;
        }

        /// <summary>
        /// Veritabanındaki bütün filmleri, gösterim tarihine göre listeler.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public IActionResult GetFilms()
        {
            var films= _mapper.Map<List<FilmDto>>(_filmService.GetFilms()); 

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (films == null)
                return NotFound("Sistemde hiç bir film kayıtlı değil.");

            return Ok(films);
        }


        /// <summary>
        /// Id'ye göre ilgili Filmi getirir.
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        [HttpGet("GetFilmById/{filmId}")]
        [ProducesResponseType(200, Type = typeof(Film))]
        [ProducesResponseType(400)]
        public IActionResult GetFilmById(int filmId)
        {
            if (!_filmService.FilmExists(filmId))
                return NotFound($"{filmId} Id'li film sistemde kayıtlı değil");

            var film = _mapper.Map<FilmDto>(_filmService.GetFilm(filmId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(film);
        }

        /// <summary>
        /// Name'e göre ilgili Filmi getirir.
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        [HttpGet("GetFilmByName/{filmName}")]
        [ProducesResponseType(200, Type = typeof(Film))]
        [ProducesResponseType(400)]
        public IActionResult GetFilmByName(string filmName)
        {
            if (!_filmService.GetFilms().Any(f=>f.Name.Trim().ToLower() == filmName.Trim().ToLower()))
                return NotFound($"{filmName} isimli film sistemde kayıtlı değil");

            var film = _mapper.Map<FilmDto>(_filmService.GetFilm(filmName));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(film);
        }

        /// <summary>
        /// Id'si verilen Filmin beğeni oranını gösterir.
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        [HttpGet("GetFilmRating/{filmId}")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetFilmRating(int filmId)
        {
            if(!_filmService.FilmExists(filmId))
                return NotFound($"{filmId} Id'li film sistemde kayıtlı değil");

            var rating = _filmService.GetFilmRating(filmId);

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok($"{filmId} id'li filmin beğeni oranı: {rating}");
        }


        [HttpPost("CreateFilm")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFilm(
            [FromQuery] int actorId, [FromQuery] int categoryId, [FromBody] FilmDto filmDto)
        {
            if (filmDto == null)
                return BadRequest(ModelState);

            var film = _filmService.GetFilms()
                .Where(f => f.Name.Trim().ToLower() == filmDto.Name.Trim().ToLower())
                .FirstOrDefault();

            if (film != null)
            {
                ModelState.AddModelError("", $"{filmDto.Name} isimli bir film sistemde mevcut.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filmMap = _mapper.Map<Film>(filmDto);
            if (!_filmService.CreateFilm(actorId, categoryId, filmMap))
            {
                ModelState.AddModelError("", "Kayıt olurken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return Ok("Başarıyla kaydedildi.");
        }



        [HttpPut("{filmId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFilm(
            int filmId, [FromBody] FilmDto filmDto)
        {
            if (filmDto == null)
                return BadRequest(ModelState);

            if (filmId != filmDto.Id)
                return BadRequest(ModelState);

            if (!_filmService.FilmExists(filmId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var film = _mapper.Map<Film>(filmDto);

            if (!_filmService.UpdateFilm(film))
            {
                ModelState.AddModelError("", "Kayıt olurken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




        [HttpDelete("{filmId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFilm(int filmId)
        {
            if (!_filmService.FilmExists(filmId))
            {
                return NotFound();
            }

            var comments = _commentService.GetCommentsOfAFilm(filmId);
            var film = _filmService.GetFilm(filmId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_commentService.DeleteComments(comments.ToList()))
            {
                ModelState.AddModelError("", "Kayıt silinirken bir hata oluştu.");
            }

            if (!_filmService.DeleteFilm(film))
            {
                ModelState.AddModelError("", "Kayıt silinirken bir hata oluştu.");
            }

            return NoContent();
        }
    }
}

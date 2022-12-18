using AutoMapper;
using FilmWebSite.BusinessLayer.DTOs;
using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : Controller
    {
        private readonly IActorService _actorService;
        private readonly IFilmService _filmService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public ActorController(IActorService actorService, IMapper mapper, IFilmService filmService, ICityService cityService)
        {
            _actorService = actorService;
            _mapper = mapper;
            _filmService = filmService;
            _cityService = cityService;
        }

        /// <summary>
        /// Sistemdeki bütün oyuncuları getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetActors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
        public IActionResult GetActors()
        {
            var actors = _mapper.Map<List<ActorDto>>(_actorService.GetActors());

            if (actors == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(actors);
        }

        /// <summary>
        /// Id' si verilen oyuncunun bilgilerini getirir.
        /// </summary>
        /// <param name="actorId"></param>
        /// <returns></returns>
        [HttpGet("GetActor/{actorId}")]
        [ProducesResponseType(200, Type = typeof(ActorDto))]
        [ProducesResponseType(400)]
        public IActionResult GetActor(int actorId)
        {
            if (!_actorService.ActorExists(actorId))
                return NotFound();

            var actor = _mapper.Map<ActorDto>(_actorService.GetActor(actorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(actor);
        }


        /// <summary>
        /// Bir oyuncunun, rol aldığı bütün filmleri listeler.
        /// </summary>
        /// <param name="actorId"></param>
        /// <returns></returns>
        [HttpGet("GetFilmsOfAActor/{actorId}")]
        [ProducesResponseType(200, Type = typeof(List<FilmDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetFilmsOfAActor(int actorId)
        {
            if (!_actorService.ActorExists(actorId))
                return NotFound();

            var films = _mapper.Map<List<FilmDto>>(_actorService.GetFilmsOfAActor(actorId));

            if (films == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(films);
        }


        /// <summary>
        /// Bir filmin bütün oyuncularını listeler.
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        [HttpGet("GetActorsOfAFilm/{filmId}")]
        [ProducesResponseType(200, Type = typeof(List<ActorDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetActorsOfAFilm(int filmId)
        {
            if (!_filmService.FilmExists(filmId))
                return NotFound();

            var actors = _mapper.Map<List<ActorDto>>(_actorService.GetActorsOfAFilm(filmId));

            if (actors == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(actors);
        }



        [HttpPost("CreateActor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateActor([FromQuery]int cityId, [FromBody]ActorDto actorDto)
        {
            if(actorDto == null)
            {
                return BadRequest();
            }

            var actor = _actorService.GetActors()
                .Where(a => a.ContactMail.Trim().ToLower() == actorDto.ContactMail.Trim().ToLower())
                .FirstOrDefault();

            if( actor != null)
            {
                ModelState.AddModelError("", $"{actorDto.ContactMail} maille daha önceden oyuncu oluşturulmuş.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actorMap = _mapper.Map<Actor>(actorDto);
            actorMap.City = _cityService.GetCity(cityId);

            if (!_actorService.CreateActor(actorMap))
            {
                ModelState.AddModelError("", "Veritabanına kayıt olurken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return Ok("Kayıt başarıyla oluşturuldu.");

        }


        [HttpPut("{actorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int actorId, [FromBody] ActorDto actorDto)
        {
            if (actorDto == null)
                return BadRequest(ModelState);

            if (actorId != actorDto.Id)
                return BadRequest(ModelState);

            if (!_actorService.ActorExists(actorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = _mapper.Map<Actor>(actorDto);

            if (!_actorService.UpdateActor(owner))
            {
                ModelState.AddModelError("", "Kayıt oluşurken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{actorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteActor(int actorId)
        {
            if (!_actorService.ActorExists(actorId))
                return NotFound();

            var actor = _actorService.GetActor(actorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_actorService.DeleteActor(actor))
            {
                ModelState.AddModelError("", "Kayıt silinirken bir hata oluştu.");
            }

            return NoContent();

        }
    }
}

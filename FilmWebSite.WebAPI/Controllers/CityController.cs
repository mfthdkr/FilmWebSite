using AutoMapper;
using FilmWebSite.BusinessLayer.DTOs;
using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace FilmWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController: Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }


        /// <summary>
        /// Sistemdeki bütün şehirleri listeler.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCities")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<City>))]
        public IActionResult GetCities()
        {
            var cities = _mapper.Map<List<CityDto>>(_cityService.GetCities());

            if (cities == null)
                return NotFound("Sistemde kayıtlı hiç bir şehir yok.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cities);  
        }

        /// <summary>
        /// Id'si verilen şehrin bilgilerini getirir.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet("GetCity/{cityId}")]
        [ProducesResponseType(200, Type = typeof(City))]
        [ProducesResponseType(400)]
        public IActionResult GetCity(int cityId)
        {
            if (!_cityService.CityExists(cityId))
                return NotFound($"{cityId} id'li bir şehir kayıtlı değil");

            var city = _mapper.Map<CityDto>(_cityService.GetCity(cityId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(city);    
        }

        /// <summary>
        /// Bir oyuncunun şehir bilgilerini getirir.
        /// </summary>
        /// <param name="actorId"></param>
        /// <returns></returns>
        [HttpGet("GetCityByActor/{actorId}")]
        [ProducesResponseType(200, Type = typeof(City))]
        [ProducesResponseType(400)]
        public IActionResult GetCityByActor(int actorId)
        {
            var city = _mapper.Map<CityDto>(_cityService.GetCityByActor(actorId));

            if (city == null) 
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(city);
        }


        [HttpPost("CreateCity")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCity([FromBody] CityDto cityDto)
        {
            if (cityDto == null)
                return BadRequest();

            var city = _cityService.GetCities()
                .Where(c=>c.Name.Trim().ToLower() == cityDto.Name.Trim().ToLower())
                .FirstOrDefault();

            if(city != null)
            {
                ModelState.AddModelError("", $"{cityDto.Name} isminde bir şehir sistemde kayıtlı.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityMap = _mapper.Map<City>(cityDto);
            if (!_cityService.CreateCity(cityMap))
            {
                ModelState.AddModelError("", "Kayıt olurken bir hata oldu.");
                return StatusCode(500, ModelState);
            }

            return Ok($"{cityDto.Name} isimli şehir kaydedildi");
        }


        [HttpPut("{cityId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCity(int cityId, [FromBody] CityDto cityDto)
        {
            if (cityDto == null)
                return BadRequest(ModelState);

            if (cityId != cityDto.Id)
                return BadRequest(ModelState);

            if (!_cityService.CityExists(cityId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = _mapper.Map<City>(cityDto);

            if (!_cityService.UpdateCity(city))
            {
                ModelState.AddModelError("", "Kayıt olurken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{cityId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCity(int cityId)
        {
            if (!_cityService.CityExists(cityId))
                return NotFound();

            var city = _cityService.GetCity(cityId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_cityService.DeleteCity(city))
            {
                ModelState.AddModelError("", "Kayıt silinirken bir hata oluştu.");
            }

            return NoContent();

        }
    }
}

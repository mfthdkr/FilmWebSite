using AutoMapper;
using FilmWebSite.BusinessLayer.DTOs;
using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Sistemdeki bütün kullanıcıları getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userService.GetUsers());

            if (users.Count == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }


        /// <summary>
        /// Bir kullanıcıya ait bilgileri getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetUser/{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_userService.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userService.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);

        }

        /// <summary>
        /// Bir kullanıcıya ait bütün yorumları listeler.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetCommentsByAUser/{userId}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentsByAUser(int userId)
        {
            if (!_userService.UserExists(userId))
                return NotFound();

            var comments = _mapper.Map<List<CommentDto>>(_userService.GetCommentsByUser(userId));

            if (comments.Count == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }

        /// <summary>
        /// Yeni bir kullanıcı kayıt eder.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            var user = _userService.GetUsers()
                .Where(u => u.Email.Trim().ToLower() == userDto.Email.Trim().ToLower())
                .FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError("", $"{userDto.Email} sahip kullanıcı vardır.Kayıt başarısız.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var userMap = _mapper.Map<User>(userDto);
            if (!_userService.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Kayıt olurken hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return Ok($"{userDto.Email}'sahip kullanıcı başarıyla kaydedildi.");
        }



        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            if (userId != userDto.Id)
                return BadRequest(ModelState);

            if (!_userService.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(userDto);

            if (!_userService.UpdateUser(user))
            {
                ModelState.AddModelError("", "Kayıt oluştulurken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userService.UserExists(userId))
                return NotFound();

            var user = _userService.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userService.DeleteUser(user))
            {
                ModelState.AddModelError("", "Kayıt silinirken bir hata oluştu.");
            }

            return NoContent();

        }
    }
}

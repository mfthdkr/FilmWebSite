using AutoMapper;
using FilmWebSite.Core.DTOs;
using FilmWebSite.BusinessLayer.Services.Abstract;
using FilmWebSite.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Sistemdeki bütün kategorileri listeler.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryService.GetCategories());

            if (categories == null)
                return NotFound("Sistemde hiç bir kategori kayıtlı değil.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }


        /// <summary>
        /// Id'si verilen kategoriyi getirir.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("GetCategory/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryService.CategoryExists(categoryId))
                return NotFound($"{categoryId} id'li kategori kayıtlı değil");

            var category = _mapper.Map<CategoryDto>(_categoryService.GetCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(category);
        }

        /// <summary>
        /// Belli bir kategoriye tüm filmleri listeler.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("GetFilmsByCategory/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(List<FilmDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetFilmsByCategory(int categoryId)
        {
            if (!_categoryService.CategoryExists(categoryId))
                return NotFound($"{categoryId} id'li kategori kayıtlı değil");

            var categories = _mapper.Map<List<FilmDto>>(_categoryService.GetFilmsByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(categories);
        }


        /// <summary>
        /// Yeni bir kategori ekler.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);

            var category =_categoryService.GetCategories()
                .Where(c=>c.Name.Trim().ToLower() == categoryDto.Name.ToLower()).FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Bu isimle bir kategori sistemde bulunmaktadır.");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryToCreate = _mapper.Map<Category>(categoryDto);
            if (!_categoryService.CreateCategory(categoryToCreate))
            {
                ModelState.AddModelError("", "Kategori eklenirken hata oluştu.");
                return StatusCode(500, ModelState);
            }
            return Ok("Kategori eklendi.");

        }


        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);

            if (categoryId != categoryDto.Id)
                return BadRequest(ModelState);

            if (!_categoryService.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category =_mapper.Map<Category>(categoryDto);

            if (!_categoryService.UpdateCategory(category))
            {
                ModelState.AddModelError("", "Kayıt yapılırken bir hata oluştu.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryService.CategoryExists(categoryId))
                return NotFound();

            var categoryToDelete = _categoryService.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryService.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Kayıt silinirken bir hata oluştu.");
            }

            return NoContent();

        }
    }
}

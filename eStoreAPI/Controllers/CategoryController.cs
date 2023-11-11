using DTO.DTO.Request.Category;
using DTO.DTO.Response.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Data;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryResponseDTO>> GetCategories()
        {
            var categoryList = categoryRepository.GetCategories();
            return Ok(categoryList);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryResponseDTO> GetCategoryById(int id)
        {
            var category = categoryRepository.GetCategoryById(id);
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddCategory(CategoryAddDTO categoryAddDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = categoryRepository.AddCategory(categoryAddDTO);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            CategoryUpdateDTO c = new CategoryUpdateDTO()
            {
                CategoryId = id,
            };
            var response = categoryRepository.DeleteCategory(c);
            return Ok(response);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            categoryUpdateDTO.CategoryId = id;
            var response = categoryRepository.UpdateCategory(categoryUpdateDTO);
            return Ok(response);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.Category.GetAllAsync(c=>true);// this filter here is temporary to get all categories successfully or you can modify the GetAllAsync method to accept null filter
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.Category
                .GetAsync(c => c.CategoryId == id);

            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.CategoryId },
                category
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId)
                return BadRequest("Id mismatch");

            var categoryFromDb = await _unitOfWork.Category
                .GetAsync(c => c.CategoryId == id);

            if (categoryFromDb == null)
                return NotFound("Category not found");

            categoryFromDb.Name = category.Name;

            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Category
                .GetAsync(c => c.CategoryId == id);

            if (category == null)
                return NotFound("Category not found");

            await _unitOfWork.Category.RemoveAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}

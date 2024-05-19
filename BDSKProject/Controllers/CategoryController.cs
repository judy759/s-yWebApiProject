
using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BDSKProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService categoryService;
        public readonly IMapper mapper;
        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
        {
            List<Category> categories = await categoryService.GetAllCategories();
            List<CategoryDTO> categoryDTOs = mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            if (categoryDTOs != null)
            {
                return Ok(categoryDTOs);
            }
            return BadRequest();
        }
    }
}

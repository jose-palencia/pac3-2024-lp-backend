using BlogUNAH.API.Constants;
using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogUNAH.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoriesController :ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this._categoriesService = categoriesService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<CategoryDto>>>> GetAll(
            string searchTerm = "", 
            int page = 1) 
        {
            var response = await _categoriesService.GetCategoriesListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<CategoryDto>>> Get(Guid id) 
        {
            var response = await _categoriesService.GetCategoryByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN, AUTHOR")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.AUTHOR}")]
        public async Task<ActionResult<ResponseDto<CategoryDto>>> Create(CategoryCreateDto dto) 
        {
            var response = await _categoriesService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.AUTHOR}")]
        public async Task<ActionResult<ResponseDto<CategoryDto>>> Edit(CategoryEditDto dto, Guid id) 
        {
            var response = await _categoriesService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.AUTHOR}")]
        public async Task<ActionResult<ResponseDto<CategoryDto>>> Delete(Guid id) 
        {
            var response = await _categoriesService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }
}

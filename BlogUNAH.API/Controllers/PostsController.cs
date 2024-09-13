using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Dtos.Posts;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogUNAH.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            this._postsService = postsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<PostDto>>>>> PaginationList(
            string searchTerm, int page = 1) 
        {
            var response = await _postsService.GetListAsync(searchTerm, page);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PostDto>>> GetOneById(Guid id) 
        {
            var response = await _postsService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<PostDto>>> Create(PostCreateDto dto) 
        {
            var response = await _postsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<PostDto>>> Edit(PostEditDto dto, 
            Guid id)
        {
            var response = await _postsService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<PostDto>>> Delete(Guid id) 
        {
            var response = await _postsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new 
            {
                response.Status,
                response.Message,
            });
        }
    }
}

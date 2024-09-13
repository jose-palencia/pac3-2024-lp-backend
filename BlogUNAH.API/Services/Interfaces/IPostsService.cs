using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Dtos.Posts;

namespace BlogUNAH.API.Services.Interfaces
{
    public interface IPostsService
    {
        Task<ResponseDto<PaginationDto<List<PostDto>>>> GetListAsync(string searchTerm = "", int page = 1);
        Task<ResponseDto<PostDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<PostDto>> CreateAsync(PostCreateDto dto);
        Task<ResponseDto<PostDto>> EditAsync(PostEditDto dto, Guid id);
        Task<ResponseDto<PostDto>> DeleteAsync(Guid id);
    }
}

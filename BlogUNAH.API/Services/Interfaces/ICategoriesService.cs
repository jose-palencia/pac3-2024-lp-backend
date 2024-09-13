using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Dtos.Common;

namespace BlogUNAH.API.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<ResponseDto<PaginationDto<List<CategoryDto>>>> GetCategoriesListAsync(string searchTerm = "", int page = 1);
        Task<ResponseDto<CategoryDto>> GetCategoryByIdAsync(Guid id);
        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto dto);
        Task<ResponseDto<CategoryDto>> EditAsync(CategoryEditDto dto, Guid id);
        Task<ResponseDto<CategoryDto>> DeleteAsync(Guid id);
    }
}
using AutoMapper;
using BlogUNAH.API.Constants;
using BlogUNAH.API.Database;
using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogUNAH.API.Services
{
    public class CategoriesSQLService : ICategoriesService
    {
        private readonly BlogUNAHContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public CategoriesSQLService(
            BlogUNAHContext context, 
            IMapper mapper, 
            IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<CategoryDto>>>> GetCategoriesListAsync(string searchTerm = "", int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var categoriesEntityQuery = _context.Categories
                .Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()));

            int totalCategories = await categoriesEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCategories / PAGE_SIZE);

            var categoriesEntity = await categoriesEntityQuery
                .OrderBy(u => u.Description)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categoriesEntity);

            return new ResponseDto<PaginationDto<List<CategoryDto>>> 
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<CategoryDto>> 
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalCategories,
                    TotalPages = totalPages,
                    Items = categoriesDtos,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }

        public async Task<ResponseDto<CategoryDto>> GetCategoryByIdAsync(Guid id)
        {
            var categoryEntity  = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (categoryEntity == null) 
            {
                return new ResponseDto<CategoryDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstant.RECORD_NOT_FOUND,
                };
            }

            var categoryDto =  _mapper.Map<CategoryDto>(categoryEntity);

            return new ResponseDto<CategoryDto> 
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORD_FOUND,
                Data = categoryDto
            };
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto dto)
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(dto);

            // TODO: Validar que la categoría no se repita

            _context.Categories.Add(categoryEntity);

            await _context.SaveChangesAsync();
            
            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

            return new ResponseDto<CategoryDto> 
            {
                StatusCode = 201,
                Status = true,
                Message = MessagesConstant.CREATE_SUCCESS,
                Data = categoryDto
            };
        }

        public async Task<ResponseDto<CategoryDto>> EditAsync(CategoryEditDto dto, Guid id)
        {
            var categoryEntity = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (categoryEntity == null)
            {
                return new ResponseDto<CategoryDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstant.RECORD_NOT_FOUND,
                };
            }            
            _mapper.Map<CategoryEditDto, CategoryEntity>(dto, categoryEntity);
            _context.Categories.Update(categoryEntity);
            await _context.SaveChangesAsync();
            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

            return new ResponseDto<CategoryDto> 
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.UPDATE_SUCCESS,
                Data = categoryDto
            };
        }

        public async Task<ResponseDto<CategoryDto>> DeleteAsync(Guid id)
        {
            var categoryEntity = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (categoryEntity == null)
            {
                return new ResponseDto<CategoryDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstant.RECORD_NOT_FOUND,
                };
            }

            _context.Categories.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CategoryDto> 
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.DELETE_SUCCESS
            };
        }

    }
}

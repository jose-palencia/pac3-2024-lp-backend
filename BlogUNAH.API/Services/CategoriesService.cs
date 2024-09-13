using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Services.Interfaces;
using Newtonsoft.Json;

namespace BlogUNAH.API.Services
{
    public class CategoriesService : ICategoriesService
    {
        public readonly string _JSON_FILE;

        public CategoriesService()
        {
            _JSON_FILE = "SeedData/categories.json"; 
        }

        public async Task<ResponseDto<PaginationDto<List<CategoryDto>>>> GetCategoriesListAsync(string searchTerm = "", int page = 1)
        {
            return new ResponseDto<PaginationDto<List<CategoryDto>>> 
            {
                StatusCode = 200,
                Status = true,
                Message = "Listado de registro obtenida correctamente.",
                Data = new PaginationDto<List<CategoryDto>> 
                {
                    Items = await ReadCategoriesFromFileAsync()
                } 
            };
        }
        public async Task<ResponseDto<CategoryDto>> GetCategoryByIdAsync(Guid id)
        {
            var categories = await ReadCategoriesFromFileAsync();
            CategoryDto category = categories.FirstOrDefault(c => c.Id == id);
            return new ResponseDto<CategoryDto> 
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro obtenido correctamente.",
                Data = category
            };
        }
        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto dto)
        {
            var categoriesDtos = await ReadCategoriesFromFileAsync();

            var categoryDto = new CategoryDto 
            {
                 Id = Guid.NewGuid(),
                 Name = dto.Name,
                 Description = dto.Description,
            };

            categoriesDtos.Add(categoryDto);

            var categories = categoriesDtos.Select(x => new CategoryEntity 
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();

            await WriteCategoriesToFileAsync(categories);
            
            return new ResponseDto<CategoryDto> 
            {
                StatusCode = 201,
                Status = true,
                Message = "Registro creado correctamente.",
            };
        }
        public async Task<ResponseDto<CategoryDto>> EditAsync(CategoryEditDto dto, Guid id)
        {
            var categoriesDto = await ReadCategoriesFromFileAsync();

            var existingCategory = categoriesDto
                .FirstOrDefault(category =>  category.Id == id);
            if (existingCategory is null) 
            {
                return new ResponseDto<CategoryDto> 
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El registro {id} no fue encontrado"
                };
            }
            
            for (int i = 0; i < categoriesDto.Count; i++)
            {
                if (categoriesDto[i].Id == id) 
                {
                    categoriesDto[i].Name = dto.Name;
                    categoriesDto[i].Description = dto.Description;
                }
            }

            var categories = categoriesDto.Select(x => new CategoryEntity
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();

            await WriteCategoriesToFileAsync(categories);
            return new ResponseDto<CategoryDto> 
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro editado correctamente"
            };
        }
        public async Task<ResponseDto<CategoryDto>> DeleteAsync(Guid id)
        {
            var categoriesDto = await ReadCategoriesFromFileAsync();
            var categoryToDelete = categoriesDto.FirstOrDefault(x => x.Id == id);

            if (categoryToDelete is null) 
            {
                return new ResponseDto<CategoryDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El registro {id} no fue encontrado"
                };
            }

            categoriesDto.Remove(categoryToDelete);

            var categories = categoriesDto.Select(x => new CategoryEntity
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();

            await WriteCategoriesToFileAsync(categories);

            return new ResponseDto<CategoryDto> 
            {
                StatusCode = 200,
                Status = true,
                Message = "Registro borrado correctamente."
            };
        }

        private async Task<List<CategoryDto>> ReadCategoriesFromFileAsync() 
        {
            if (!File.Exists(_JSON_FILE)) 
            {
                return new List<CategoryDto>();
            }

            var json = await File.ReadAllTextAsync(_JSON_FILE);

            var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(json);

            var dtos = categories.Select(x => new CategoryDto 
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();

            return dtos;
        }

        private async Task WriteCategoriesToFileAsync(List<CategoryEntity> categories) 
        {
            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            if (File.Exists(_JSON_FILE))
            {
                await File.WriteAllTextAsync(_JSON_FILE, json);
            }

        }

    }
}

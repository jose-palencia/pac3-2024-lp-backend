using AutoMapper;
using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Dtos.Posts;

namespace BlogUNAH.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForCategories();
            MapsForPosts();
        }

        private void MapsForCategories()
        {
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryCreateDto, CategoryEntity>();
            CreateMap<CategoryEditDto, CategoryEntity>();
        }

        private void MapsForPosts() 
        {
            CreateMap<PostEntity, PostDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags
                .Select(pt => pt.Tag.Name)
                .ToList()));
            CreateMap<PostCreateDto, PostEntity>();
            CreateMap<PostEditDto, PostEntity>();
        }
    }
}

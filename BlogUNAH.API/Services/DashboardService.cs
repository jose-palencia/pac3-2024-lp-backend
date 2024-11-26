using AutoMapper;
using BlogUNAH.API.Database;
using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Dtos.Dashboard;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogUNAH.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly BlogUNAHContext _context;
        private readonly IMapper _mapper;

        public DashboardService(
            BlogUNAHContext context,
            IMapper mapper
            )
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ResponseDto<DashboardDto>> GetDashboardAsync()
        {
            var posts = await _context.Posts.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();

            var categories = await _context.Categories.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();

            var tags = await _context.Tags.
                OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync();

            var dashboardDto = new DashboardDto
            {
                PostsCount = await _context.Posts.CountAsync(),
                TagsCount = await _context.Tags.CountAsync(),
                UsersCount = await _context.Users.CountAsync(),
                CommentsCount = Random.Shared.Next( 0, 10000),
                Posts = _mapper.Map<List<DashboardPostDto>>(posts),
                Categories = _mapper.Map<List<DashboardCategoryDto>>(categories),
                Tags = _mapper.Map<List<DashboardTagDto>>(tags)
            };

            return new ResponseDto<DashboardDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Datos obtenidos correctamente",
                Data = dashboardDto
            };
        }
    }
}

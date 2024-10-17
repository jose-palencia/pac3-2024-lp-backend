using AutoMapper;
using BlogUNAH.API.Constants;
using BlogUNAH.API.Database;
using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Dtos.Posts;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogUNAH.API.Services
{
    public class PostsService : IPostsService
    {
        private readonly BlogUNAHContext _context;
        private readonly IAuditService _auditService;
        private readonly ILogger<PostsService> _logger;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;

        public PostsService(BlogUNAHContext context,
            IAuditService auditService,
            ILogger<PostsService> logger,
            IMapper mapper,
            IConfiguration configuration)
        {
            this._context = context;
            this._auditService = auditService;
            this._logger = logger;
            this._mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }

        public async Task<ResponseDto<PaginationDto<List<PostDto>>>> GetListAsync(
            string searchTerm = "", int page = 1) 
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var postEntityQuery = _context.Posts
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Where(x => x.PublicationDate <= DateTime.Now);

            if (!string.IsNullOrEmpty(searchTerm)) 
            {
                postEntityQuery = postEntityQuery
                    .Where(x => (x.Title + " " + x.Category.Name + " " + x.Content)
                    .ToLower().Contains(searchTerm.ToLower()));
            }

            int totalPosts = await postEntityQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalPosts / PAGE_SIZE);

            var postsEntity = await postEntityQuery
                .OrderByDescending(x => x.PublicationDate)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var postsDto = _mapper.Map<List<PostDto>>(postsEntity);

            return new ResponseDto<PaginationDto<List<PostDto>>> 
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORDS_FOUND,
                Data = new PaginationDto<List<PostDto>> 
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalPosts,
                    TotalPages = totalPages,
                    Items = postsDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages,
                }
            };

        }

        public async Task<ResponseDto<PostDto>> GetByIdAsync(Guid id) 
        {
            var postEntity = await _context.Posts
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(postEntity is null) 
            {
                return new ResponseDto<PostDto> 
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"{MessagesConstant.RECORD_NOT_FOUND} {id}"
                };
            }

            var postDto = _mapper.Map<PostDto>(postEntity);

            return new ResponseDto<PostDto> 
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstant.RECORD_FOUND,
                Data = postDto,
            };
        }

        public async Task<ResponseDto<PostDto>> CreateAsync(PostCreateDto dto) 
        {
            using (var transaction  = await _context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    var postEntity = _mapper.Map<PostEntity>(dto);
                    postEntity.AuthorId = _auditService.GetUserId();

                    _context.Posts.Add(postEntity);
                    await _context.SaveChangesAsync();

                    // Buscar tags del dto en la tabla de tags
                    var existingTags = await _context.Tags
                        .Where(t => dto.TagList.Contains(t.Name))
                        .ToListAsync();

                    // Identificar tags que no existen
                    var newTagNames = dto.TagList
                        .Except(existingTags.Select(t => t.Name)).ToList();

                    // Crear nuevos tags
                    var newTags = newTagNames.Select(name => new TagEntity
                    {
                        Name = name,
                    }).ToList();

                    _context.Tags.AddRange(newTags);
                    await _context.SaveChangesAsync();

                    // Combinar tags existentes y nuevos
                    var allTags = existingTags.Concat(newTags).ToList();

                    var postTagsEntity = allTags.Select(t => new PostTagEntity 
                    {
                        PostId = postEntity.Id,
                        TagId = t.Id,
                    }).ToList();

                    _context.PostsTags.AddRange(postTagsEntity);
                    await _context.SaveChangesAsync();

                    //throw new Exception("Error para probar el Rollback.");
                    await transaction.CommitAsync();

                    return new ResponseDto<PostDto> 
                    {
                        StatusCode = 201,
                        Status = true,
                        Message = MessagesConstant.CREATE_SUCCESS,
                    };
                }
                catch (Exception e) 
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.CREATE_ERROR);
                    return new ResponseDto<PostDto> 
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.CREATE_ERROR
                    };
                }
            }
        }

        public async Task<ResponseDto<PostDto>> EditAsync(PostEditDto dto, Guid id) 
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try 
                {
                    var postEntity = await _context.Posts.FindAsync(id);

                    if (postEntity is null) 
                    {
                        return new ResponseDto<PostDto> 
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"{MessagesConstant.RECORD_NOT_FOUND} {id}"
                        };
                    }

                    _mapper.Map(dto, postEntity);
                    //postEntity.AuthorId = _authService.GetUserId(); //TODO: Remover cuanto este el frontend con nueva logica

                    _context.Posts.Update(postEntity);
                    await _context.SaveChangesAsync();

                    // Eliminar tags anteriores
                    var existingPostTags = await _context.PostsTags
                        .Where(t => t.PostId == id).ToListAsync();

                    _context.RemoveRange(existingPostTags);
                    await _context.SaveChangesAsync();
                    

                    // Buscar tags del dto en la tabla de tags
                    var existingTags = await _context.Tags
                        .Where(t => dto.TagList.Contains(t.Name))
                        .ToListAsync();

                    // Identificar tags que no existen
                    var newTagsNames = dto.TagList
                        .Except(existingTags.Select(t => t.Name))
                        .ToList();

                    // Crear nuevos tags
                    var newTags = newTagsNames
                        .Select(name => new TagEntity { Name = name }).ToList();
                    _context.Tags.AddRange(newTags);
                    await _context.SaveChangesAsync();

                    // Combinar tags existentes y nuevas
                    var allTags = existingTags.Concat(newTags).ToList();

                    // Agregar tags a la tabla post_tags
                    var postTagsNew = allTags
                        .Select(t => new PostTagEntity 
                        {
                            PostId = postEntity.Id,
                            TagId = t.Id,
                        })
                        .ToList();

                    _context.PostsTags.AddRange(postTagsNew);
                    await _context.SaveChangesAsync();

                    //throw new Exception("Error para validar el rollback.");

                    await transaction.CommitAsync();

                    return new ResponseDto<PostDto> 
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = MessagesConstant.UPDATE_SUCCESS
                    };

                } 
                catch (Exception e) 
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.UPDATE_ERROR);
                    return new ResponseDto<PostDto> 
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.UPDATE_ERROR
                    };
                }
            }
        }

        public async Task<ResponseDto<PostDto>> DeleteAsync(Guid id) 
        {
            using (var transaction = await _context.Database.BeginTransactionAsync()) 
            {
                try 
                {
                    var postEntity = await _context.Posts.FindAsync(id);

                    if (postEntity is null)
                    {
                        return new ResponseDto<PostDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"{MessagesConstant.RECORD_NOT_FOUND} {id}"
                        };
                    }

                    _context.Posts.Remove(postEntity);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new ResponseDto<PostDto> 
                    {
                        StatusCode = 200,
                        Status = true,
                        Message = MessagesConstant.DELETE_SUCCESS
                    };
                } 
                catch (Exception e) 
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, MessagesConstant.DELETE_ERROR);
                    return new ResponseDto<PostDto> 
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = MessagesConstant.DELETE_ERROR
                    };
                }
            }
        }
    }
}

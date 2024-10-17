using BlogUNAH.API.Dtos.Auth;
using BlogUNAH.API.Dtos.Common;

namespace BlogUNAH.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
    }
}

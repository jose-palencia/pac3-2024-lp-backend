using BlogUNAH.API.Dtos.Auth;
using BlogUNAH.API.Dtos.Common;
using System.Security.Claims;

namespace BlogUNAH.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto);
        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto);
        ClaimsPrincipal GetTokenPrincipal(string token);
    }
}

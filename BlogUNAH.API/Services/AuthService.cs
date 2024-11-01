using BlogUNAH.API.Constants;
using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Auth;
using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogUNAH.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager, 
            IConfiguration configuration
            )
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager
                .PasswordSignInAsync(dto.Email, 
                                     dto.Password, 
                                     isPersistent: false, 
                                     lockoutOnFailure: false);

            if(result.Succeeded) 
            {
                // Generación del token
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                // ClaimList creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                };

                var userRoles = await _userManager.GetRolesAsync(userEntity);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return new ResponseDto<LoginResponseDto> 
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Inicio de sesion satisfactorio",
                    Data = new LoginResponseDto 
                    {
                        FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo,
                    }
                };

            }

            return new ResponseDto<LoginResponseDto> 
            {
                Status = false,
                StatusCode = 401,
                Message = "Fallo el inicio de sesión"
            };


        }

        public async Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto) 
        {
            var user = new UserEntity 
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Email,
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded) 
            {
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                await _userManager.AddToRoleAsync(userEntity, RolesConstant.USER);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                    new Claim(ClaimTypes.Role, RolesConstant.USER)
                };

                var jwtToken = GetToken(authClaims);

                return new ResponseDto<LoginResponseDto> 
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Registro de usuario realizado satisfactoriamente.",
                    Data = new LoginResponseDto 
                    {
                        FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo,
                    }
                };
            }

            return new ResponseDto<LoginResponseDto> 
            {
                StatusCode = 400,
                Status = false,
                Message = "Error al registrar el usuario"
            };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:Expires"] ?? "15")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, 
                    SecurityAlgorithms.HmacSha256)
            );
        }
    }
}

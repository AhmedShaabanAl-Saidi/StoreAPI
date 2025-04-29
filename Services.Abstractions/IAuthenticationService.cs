using Shared.IdentityDtos;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
    }
}

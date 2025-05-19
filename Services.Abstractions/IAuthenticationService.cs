using Shared.IdentityDtos;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<UserResultDto> GetUserByEmailAsync(string email);
        Task<bool> IsEmailExist(string email);
        Task<IdentityAddressDto> GetUserAddressAsync(string email);
        Task<IdentityAddressDto> UpdateUserAddressAsync(string email, IdentityAddressDto addressDto);
    }
}

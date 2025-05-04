using Shared.IdentityDtos;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<UserResultDto> GetUserByEmailAsync(string email);
        Task<bool> IsEmailExist(string email);
        Task<AddressDto> GetUserAddressAsync(string email);
        Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto addressDto);
    }
}

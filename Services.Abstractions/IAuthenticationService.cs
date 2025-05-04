using Shared.IdentityDtos;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<UserResultDto> GetUserByEmail(string email);
        Task<bool> IsEmailExist(string email);
        Task<AddressDto> GetUserAddress(string email);
        Task<AddressDto> UpdateUserAddress(string email, AddressDto addressDto);
    }
}

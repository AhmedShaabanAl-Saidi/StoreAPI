using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared.IdentityDtos;

namespace Services
{
    public class AuthenticationService(UserManager<User> userManager, IMapper mapper)
        : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                throw new UnAuthorizedException($"Email : {loginDto.Email} Not found");

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
                throw new UnAuthorizedException("Invalid Password");

            return new UserResultDto
            (
                user.DisplayName,
                user.Email!,
                "Token"
            );

        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }

            return new UserResultDto
            (
                user.DisplayName,
                user.Email!,
                "Token"
            );
        }
    }
}

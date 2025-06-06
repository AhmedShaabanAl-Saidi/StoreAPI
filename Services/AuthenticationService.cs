﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.IdentityDtos;

namespace Services
{
    public class AuthenticationService(UserManager<User> userManager, IMapper mapper,IOptions<JwtOptions> options)
        : IAuthenticationService
    {
        public async Task<IdentityAddressDto> GetUserAddressAsync(string email)
        {
            var user = await userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
                throw new UserNotFoundException(email);

            return mapper.Map<IdentityAddressDto>(user.Address);
        }

        public async Task<UserResultDto> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                throw new UserNotFoundException(email);

            return new UserResultDto(
                user.DisplayName,
                user.Email,
                null // No token is needed here
            );
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return user != null;
        }

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
                await CreateTokenAsync(user)    
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
                await CreateTokenAsync(user)
            );
        }

        public async Task<IdentityAddressDto> UpdateUserAddressAsync(string email, IdentityAddressDto addressDto)
        {
            var user = await userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
                throw new UserNotFoundException(email);

            var mappedAddress = mapper.Map<Address>(addressDto);

            user.Address = mappedAddress;

            await userManager.UpdateAsync(user);

            return addressDto;
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = options.Value;

            // Claims
            var clams = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                clams.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                claims: clams,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                audience:jwtOptions.Audience, 
                issuer: jwtOptions.Issuer
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

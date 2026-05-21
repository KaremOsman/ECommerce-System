using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.DataTransferObject.IdentityModules;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {
        public async Task<UserDto> LogInAsync(LogInDto logInDto)
        {
            // 1. Find the user by email
            var user = await _userManager.FindByEmailAsync(logInDto.Email);
            if (user is null)
                throw new UserNotFoundException(logInDto.Email);
            // 2. Check the password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, logInDto.Password);
            if (!isPasswordValid)
                throw new UnauthorizedException("Invalid Email or password.");

            return new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // 1. Mapping RegisterDto to ApplicationUser
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
            // 2. Check if the email is already taken
            if (await _userManager.FindByEmailAsync(registerDto.Email) is not null)
                throw new BadRequestException(new List<string>{$"The Email ' {registerDto.Email} ' is already taken."});
            // 3. Create the user
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.Select(e => e.Description).ToList());
            return new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };

        }

        public async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            { 
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
           
            foreach (var role in roles)        
                claims.Add(new Claim(ClaimTypes.Role, role));

            var secretKey = _configuration["JWTOptions:SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

            var signingCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer : _configuration["JWTOptions:Issuer"],
                audience : _configuration["JWTOptions:Audience"],
                claims : claims,
                expires : DateTime.Now.AddHours(1),
                signingCredentials : signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            return user is not null; 
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UserNotFoundException(email);
            var userDto = new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };
            return userDto;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
                throw new UserNotFoundException(email);
            if (user.Address is null)
                throw new AddressNotFoundException(user.UserName!);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
                throw new UserNotFoundException(email);
            if (user.Address is null)
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            else
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
            }

            await _userManager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }
    }
}

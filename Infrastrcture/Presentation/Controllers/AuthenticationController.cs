using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DataTransferObject.IdentityModules;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        // LogIn 
        [HttpPost("LogIn")]
        public async Task<ActionResult<UserDto>> LogIn(LogInDto logInDto)
        {
            var user = await _serviceManager.AuthenticationService.LogInAsync(logInDto);
            return Ok(user);
        }
        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }
        // Check Email
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
            {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }
        // GetCurernt User
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(user);
        }
        // GetCurernt User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(address);
        }
        // Udate Curernt User Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAdress(AddressDto newAdress)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Updatedaddress =await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email!, newAdress);
            return Ok(Updatedaddress);
        }
    }
}

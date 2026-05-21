using Shared.DataTransferObject.IdentityModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        // LogIn ==> (Take : Email, Password & Return : JWT Token, Email, DisplayName);
        Task<UserDto> LogInAsync(LogInDto logInDto);

        // Register ==> (Take : Email, Password, DisplayName, UserName, PhoneNumber & Return : JWT Token, Email, DisplayName);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        // Check Email Existence ==> (Take : Email & Return : bool);
        Task<bool> CheckEmailAsync(string email);

        // Get Current User Info ==> (Take : Email & Return : UserDto);
        Task<UserDto> GetCurrentUserAsync(string email);
        //Get Current User Address ==> (Take : Email & Return : AddressDto);
        Task<AddressDto> GetCurrentUserAddressAsync(string email);
        // Update Current User Address ==> (Take : Email, AddressDto & Return : AddressDto);
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto);
    }
}

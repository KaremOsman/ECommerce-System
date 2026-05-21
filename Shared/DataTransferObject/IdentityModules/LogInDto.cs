using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.IdentityModules
{
    public class LogInDto
    {
        [EmailAddress]
        public string Email { get; set; } = default!;
        [PasswordPropertyText]
        public string Password { get; set; } = default!;
    }
}

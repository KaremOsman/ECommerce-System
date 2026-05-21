using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
        protected string GetUserEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return email!;
        }
    }
}

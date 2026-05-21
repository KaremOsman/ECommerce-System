using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_CommerceApp.Middelwares
{
    public class GlobalErrorHandlingMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddelware> _logger;

        public GlobalErrorHandlingMiddelware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddelware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Request processing logic
                await _next(context);
                // Response processing logic (if needed)
                await HandelNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error has occurred while processing the request.");
                await HandelExceptionAsync(context, ex);
            }
        }

        private static async Task HandelExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            var Response = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message,
                Errors = ex switch
                {
                    BadRequestException badRequestException => badRequestException.Errors,
                    _ => []
                }
            };
            await context.Response.WriteAsJsonAsync(Response);
        }

        private static async Task HandelNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorDetails
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The End Point ' {context.Request.Path} ' is not found."
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System.Text;

namespace Presentation.Attributes
{
    public class CacheAttribute(string cacheGroup, int durationInSec = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var versionKey = $"{cacheGroup}:version";
            var version = await cacheService.GetStringAsync(versionKey) ?? "1";

            string cacheKey = CreateCacheKey(context.HttpContext.Request, cacheGroup, version);

            var cachedValue = await cacheService.GetCacheValueAsync(cacheKey);
            if (cachedValue != null)
            {
                context.Result = new ContentResult
                {
                    Content = cachedValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult result)
            {
                await cacheService.SetCacheValueAsync(
                    cacheKey,
                    result.Value!,
                    TimeSpan.FromSeconds(durationInSec));
            }
        }

        private static string CreateCacheKey(HttpRequest request, string cacheGroup, string version)
        {
            StringBuilder key = new();
            key.Append($"{cacheGroup}:v{version}:{request.Path.ToString().TrimEnd('/')}");

            var filteredQuery = request.Query
                .Where(q => !string.IsNullOrEmpty(q.Value))
                .OrderBy(q => q.Key);

            if (filteredQuery.Any())
            {
                key.Append('?');
                foreach (var (Key, Value) in filteredQuery)
                {
                    key.Append($"{Key.ToLower()}={Value.ToString().ToLower()}&");
                }
            }

            return key.ToString().TrimEnd('&');
        }
    }
}
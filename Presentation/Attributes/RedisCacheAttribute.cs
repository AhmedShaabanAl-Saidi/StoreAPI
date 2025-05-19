using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;

namespace Presentation.Attributes
{
    public class RedisCacheAttribute(int durationInSeconds = 60) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            var Key = GenerateCacheKey(context.HttpContext.Request);

            var value = await cacheService.GetAsync(Key);

            if (value is not null)
            {
                context.Result = new ContentResult
                {
                    Content = value,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var actionExecutedContext = await next.Invoke();

            if (actionExecutedContext.Result is OkObjectResult result)
                await cacheService.SetAsync(Key, result.Value, TimeSpan.FromSeconds(durationInSeconds));

        }

        private static string GenerateCacheKey(HttpRequest httpRequest)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(httpRequest.Path).Append("?");

            foreach (var item in httpRequest.Query.OrderBy(i => i.Key))
                builder.Append($"{item.Key}={item.Value}&");

            return builder.ToString().TrimEnd('&');
        }
    }
}

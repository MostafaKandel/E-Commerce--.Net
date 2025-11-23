using E_Commerce.Service_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    internal class RedisCacheAttribute: ActionFilterAttribute
    {
        private readonly int durationInMin;

        public RedisCacheAttribute(int DurationInMin=5)
        {
            durationInMin = DurationInMin;
        }
        //next: what come after attribute, can be the endpoint or something else
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get the cache service from the DI container
            var CacheService= context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheKey= CreateCacheKey(context.HttpContext.Request);


            // check if Cache Data is exist
            var CacheValue = await CacheService.GetAsync(CacheKey);

            // if exist return it, and skip the controller action execution
            if (CacheValue is not null)
            {
                context.Result= new ContentResult()
                {
                    Content= CacheValue,
                    ContentType= "application/json",
                    StatusCode= StatusCodes.Status200OK
                };
                return;
            }
            // if not exist, execute the controller action
            var ExcutedContext =await next.Invoke();
            if(ExcutedContext.Result is OkObjectResult result)
            {
                // store the result in the cache
                await CacheService.SetAsync(CacheKey, result.Value , TimeSpan.FromMinutes(durationInMin));
            }


        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key= new StringBuilder();
            Key.Append(request.Path);
            foreach (var query in request.Query.OrderBy(x => x.Key))
            {
                 Key.Append($"|{query.Key}-{query.Value}");
            }
            return Key.ToString();
        }
    }
}

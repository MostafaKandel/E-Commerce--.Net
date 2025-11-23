using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Custom_Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> logger) {
            next = Next;
            this.logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "something wrong");
                
                var Problem = new ProblemDetails()
                {
                    Title = "An unexpected error occurred!",
                    
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status =  ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    }
                };
                httpContext.Response.StatusCode = Problem.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(Problem);

            }
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Problem = new ProblemDetails()
                {
                    Title = "The resource you are looking for is not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"The resource at {httpContext.Request.Path} is not found",
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(Problem);
            }
        }


    }
}

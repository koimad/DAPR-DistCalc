using Microsoft.AspNetCore.Builder;
using ServerCore.MiddleWare;

namespace ServerCore.Extensions
{
    public static class ResponseMiddlewareExtension
    {
        public static IApplicationBuilder UseResponseAndExceptionWrapper(this IApplicationBuilder builder, ResponseOptions options = default)
        {
            options ??= new ResponseOptions();
            return builder.UseMiddleware<ResponseMiddleware>(options);
        }
    }
}
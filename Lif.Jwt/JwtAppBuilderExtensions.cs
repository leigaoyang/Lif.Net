using Microsoft.AspNetCore.Builder;

namespace Lif.Jwt
{
    public static class JwtAppBuilderExtensions
    {
        public static IApplicationBuilder UseLifJwt(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
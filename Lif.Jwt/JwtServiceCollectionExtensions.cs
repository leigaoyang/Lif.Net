using Microsoft.Extensions.DependencyInjection;

namespace Lif.Jwt
{
    public static class JwtServiceCollectionExtensions
    {
        public static IServiceCollection AddLifJwt(this IServiceCollection services)
        {
            return services;
        }
    }
}
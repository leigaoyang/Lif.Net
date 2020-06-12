using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Lif.Jwt
{
    public static class LifJwtServiceCollectionExtensions
    {
        public static IServiceCollection AddLifJwt(this IServiceCollection services, Action<LifJwtOptions> configureOptions)
        {
            var lifJwtOptions = new LifJwtOptions();
            configureOptions?.Invoke(lifJwtOptions);
            services.AddHttpContextAccessor();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.RequireHttpsMetadata = false;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = lifJwtOptions.IssuerSigningKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped(lifJwtOptions.UserService);
            return services;
        }
    }
}
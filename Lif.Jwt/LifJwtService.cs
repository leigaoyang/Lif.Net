using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Lif.Jwt
{
    public abstract class LifJwtService<TUser> where TUser : class
    {
        readonly IHttpContextAccessor httpContextAccessor;

        protected LifJwtService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken(DateTime expires, SigningCredentials signingCredentials, TUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GenerateClaims(user)),
                Expires = expires,
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public TUser GetUser()
        {
            return GenerateUser(httpContextAccessor.HttpContext.User.Claims);
        }

        protected abstract TUser GenerateUser(IEnumerable<Claim> claims);

        protected abstract IEnumerable<Claim> GenerateClaims(TUser user);
    }
}
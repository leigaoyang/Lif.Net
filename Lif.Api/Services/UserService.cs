using System;
using System.Collections.Generic;
using System.Security.Claims;
using Lif.Api.Models;
using Lif.Jwt;
using Microsoft.AspNetCore.Http;

namespace Lif.Api.Services
{
    public class UserService : LifJwtService<User>
    {
        public UserService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        protected override User GenerateUser(IEnumerable<Claim> claims)
        {
            var user = new User();
            foreach (var claim in claims)
                switch (claim.Type)
                {
                    case ClaimTypes.Sid:
                        user.Id = Convert.ToInt32(claim.Value);
                        break;
                    case ClaimTypes.NameIdentifier:
                        user.UserName = claim.Value;
                        break;
                }

            return user;
        }

        protected override IEnumerable<Claim> GenerateClaims(User user)
        {
            yield return new Claim(ClaimTypes.Sid, user.Id.ToString());
            yield return new Claim(ClaimTypes.NameIdentifier, user.UserName);
        }
    }
}
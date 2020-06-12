using System;
using Microsoft.IdentityModel.Tokens;

namespace Lif.Jwt
{
    public class LifJwtOptions
    {
        public SecurityKey IssuerSigningKey { get; set; }
        public Type UserService { get; set; }
    }
}
using auction_webapi.Models;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace webapi.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        private static IConfiguration _config;
        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, IConfiguration config)
        {
            _next = next;
            _logger = logger;
            _config = config;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:key"]);
            var handler = new JwtSecurityTokenHandler();
            var b = context.Request.Headers["Authorization"].ToString();
            var tokenSecure = handler.ReadToken(context.Request.Headers["Authorization"]) as SecurityToken;
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(b, validations, out tokenSecure);
            var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            
                User user = new User();

                int a;
                int.TryParse(claims.Claims.FirstOrDefault(x => x.Type == "userId")?.Value ?? "", out a);
                user.UserId = a;
            EnumRoles role;
            Enum.TryParse(claims.Claims.FirstOrDefault(x => x.Type == "Role")?.Value ?? "", out role);
            user.Role = role;
            // var emailClaim = claims.Claims.FirstOrDefault(x => x.Type == "email");
            // if (emailClaim != null)
            // {
            //    user.Email = emailClaim.Value;
            // }
            context.Items["User"] = user;
                await _next(context);
 
            }

        }
}








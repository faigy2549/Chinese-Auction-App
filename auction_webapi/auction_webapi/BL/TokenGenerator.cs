using auction_webapi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace auction_webapi.BL
{
    public class TokenGenerator
    {
        public static string GenerateToken(string username, EnumRoles role, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("bd1a1ccf8095037f361a4d351e7c0de65f0776bfc2f478ea8d312c763bb6caca");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 //new Claim("email", username),
                 new Claim(ClaimTypes.Role, role.ToString()),
                 new Claim("userId",userId.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer= "http://localhost:44342/",
                Audience= "http://localhost:4200/",

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            
    

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

    }
}

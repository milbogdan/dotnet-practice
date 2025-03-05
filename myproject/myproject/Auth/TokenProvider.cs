using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using myproject.Model;
using System.Security.Claims;
using System.Text;

namespace myproject.Auth
{
    public class TokenProvider(IConfiguration configuration)
    {
        public string Create (User user)
        {
            string secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var Credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescripotor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email.ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = Credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescripotor);

            return token;
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interfaces;
using api.Model;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenService : ITokenServices
    {
        private readonly IConfiguration _configration;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _configration = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration["JWT:SigningKey"]));
        }
        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName)
            };

            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha256Signature);
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configration["JWT:Issuer"],
                Audience = _configration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesciptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
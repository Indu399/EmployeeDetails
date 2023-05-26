using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;



namespace EmployeeDetails
{
    public class JwtTokenManager : IJwtTokenManager

    {
        private readonly IConfiguration _configuration;
      

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Authenticate(string userName, string password)
        {
            if (!Data.ApiToken.Employee.Any(x => x.Key.Equals(userName) && x.Value.Equals(password)))
                return null;

            var Key = _configuration.GetValue<string>("JwtConfig:Key");
            var KeyBytes = Encoding.ASCII.GetBytes(Key);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName)
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(KeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}

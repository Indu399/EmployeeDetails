using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeDetails
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(authOptions =>

            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            })
                .AddJwtBearer(jwtOptions =>
                {
                    var Key = Configuration.GetValue<string>("JwtConfig:Key");
                    var KeyBytes = Encoding.ASCII.GetBytes(Key);
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(KeyBytes),
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddSingleton(typeof(IJwtTokenManager),typeof(JwtTokenManager));

        }


    }
}

using IdentityAPI.Interfaces;
using IdentityAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityAPI.Commands
{
    public class JwtGenerationCommand : IJwtGenerationCommand
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly JwtSecurityTokenHandler tokenHandler;

        public JwtGenerationCommand(IConfiguration configuration, IHostingEnvironment hostingEnvironment, JwtSecurityTokenHandler tokenHandler)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
            this.tokenHandler = tokenHandler;
        }
        public string GenerateToken(Usuario model)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = this.tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }

        protected ClaimsIdentity ObterClaims(Usuario model)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("UserId", model.Id.ToString()));
            claims.AddClaim(new Claim("Nome", model.Nome));
            claims.AddClaim(new Claim("TipoAcesso", model.TipoAcesso.ToString()));
            claims.AddClaim(new Claim("Email", model.Email.ToString()));
            return claims;
        }
    }
}

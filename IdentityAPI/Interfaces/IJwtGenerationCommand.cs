using IdentityAPI.Model;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityAPI.Interfaces
{
    public interface IJwtGenerationCommand
    {
        string GenerateToken(Usuario model);
    }
}

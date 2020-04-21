using FootAPI.Repository;
using GeneralLibrary;
using IdentityAPI.Interfaces;
using IdentityAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("generatetoken")]
        public async Task<IActionResult> GenerateToken(
            [FromBody] AuthModel authModel,
            [FromServices] ILoginRepository loginRepository,
            [FromServices] IJwtGenerationCommand jwtGenerationCommand,
            [FromServices] ErrorLoggingRepository errorLoggingRepository
        )
        {
            try
            {
                var user = await loginRepository.Obter(authModel);
                return Ok(new { Token = jwtGenerationCommand.GenerateToken(user), Auth = user });
            }
            catch(Exception ex)
            {
                await errorLoggingRepository.RegistrarErro(ex);
                return BadRequest(ex);
            }
        }
    }
}
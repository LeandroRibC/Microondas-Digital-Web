using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MicroondasDigital.Api.DTOs; // Seus DTOs de login

namespace MicroondasDigital.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config) {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login) {
            // TODO: Valide o usuário e senha (hash SHA256) conforme sua lógica
            // Exemplo fixo:
            if (login.Usuario == "admin" && login.SenhaSha256 == "HASH_DA_SENHA") {
                var token = GerarToken(login.Usuario);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private string GerarToken(string usuario) {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

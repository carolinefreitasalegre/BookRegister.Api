using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult LoginAccount([FromBody] LoginData login)
        {
            if (login.User == "admin" && login.Password == "admin")
            {
                var token = GerarTokenJwt();
                return Ok(new { token });
            }

            return BadRequest("Credenciais inválidas.");
        }


        private string GerarTokenJwt()
        {
            string secretToken = "bf070603 - edc3 - 4fbb-b153-5ea1c590cca3";

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admin"),
                new Claim("nome", "Administrador do sistema")
            };

            var token = new JwtSecurityToken(
                issuer: "register_book",
                audience: "app_book",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credencial
                );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

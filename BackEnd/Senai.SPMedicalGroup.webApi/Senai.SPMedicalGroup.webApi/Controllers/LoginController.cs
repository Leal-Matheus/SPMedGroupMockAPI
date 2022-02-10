using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.SPMedicalGroup.webApi.Domains;
using Senai.SPMedicalGroup.webApi.Repositories;
using Senai.SPMedicalGroup.webApi.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel Login)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.Login(Login.EmailUsuario, Login.SenhaUsuario);
                if (usuarioBuscado != null)
                {
                    var Claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.EmailUsuario),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                    new Claim("role", usuarioBuscado.IdTipoUsuario.ToString())
                };

                    var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SenaiSPMedicalGroupWebApi"));

                    var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

                    var myToken = new JwtSecurityToken(
                            issuer: "SPMedicalGroup.webApi",
                            audience: "SPMedicalGroup.webApi",
                            claims: Claims,
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: Creds
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(myToken)
                    });
                }

                return NotFound("Email ou Senha Inválidos!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.SPMedicalGroup.webApi.Domains;
using Senai.SPMedicalGroup.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        public string JwtRegisteredClaimTypes { get; private set; }

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Listar()
        {
            if (_usuarioRepository.ListarUsuarios() == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não há nenhum usuario cadastrado ainda"
                });
            }

            return Ok(_usuarioRepository.ListarUsuarios());
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Usuario novoUsuario)
        {
            try
            {
                if (novoUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Insira todos os dados"
                    });
                }
                _usuarioRepository.Cadastrar(novoUsuario);
                return StatusCode(201, new
                {
                    Mensagem = "Usuario cadastrado",
                    novoUsuario
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(int id, Usuario usuarioAtualizado)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido"
                    });
                }

                if (_usuarioRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhum usuário com este ID"
                    });
                }
                if (usuarioAtualizado == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe todos os dados"
                    });
                }
                _usuarioRepository.Atualizar(id, usuarioAtualizado);
                return StatusCode(200, new
                {
                    Mensagem = "Usuario atualizado",
                    usuarioAtualizado
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpDelete("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido"
                    });
                }

                if (_usuarioRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhum usuário com este ID"
                    });
                }

                _usuarioRepository.Deletar(id);

                return StatusCode(200, new
                {
                    Mensagem = "Usuario deletado"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpPost("imagem/bd/{idUsuario}")]
        public IActionResult postBD(IFormFile arquivo, int idUsuario)
        {
            try
            {
                if (arquivo == null)
                {
                    return BadRequest(new { mensagem = "É necessario enviar uma foto .png ou .jpg" });
                }
                if (arquivo.Length > 5000)
                {
                    return BadRequest(new { mensagem = "O tamanho máximo da imagem é de 5mb" });
                }

                string extensao = arquivo.FileName.Split('.').Last();

                if (extensao != "png" || extensao != "jpg")
                {
                    return BadRequest(new { mensagem = "Apenas arquivos .png ou .jpg são suportados" });
                }



                _usuarioRepository.SalvarPerfilBD(arquivo, idUsuario);

                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }


        }
        [Authorize(Roles = "1")]
        [HttpGet("imagem/bd/{idUsuario}")]
        public IActionResult getBd(int idUsuario)
        {
            try
            {
                string base64 = _usuarioRepository.ConsultarPerfilBD(idUsuario);
                return Ok(base64);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuariooBuscado = _usuarioRepository.BuscarPorId(id);

                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido!"
                    });
                }

                if (usuariooBuscado == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Nenhum usuário com o ID informado!"
                    });
                }

                return StatusCode(201, new
                {
                    Mensagem = "usuário encontrado",
                    usuariooBuscado
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}

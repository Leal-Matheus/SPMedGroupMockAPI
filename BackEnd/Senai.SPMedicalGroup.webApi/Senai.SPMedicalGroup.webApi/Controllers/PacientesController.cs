using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.SPMedicalGroup.webApi.Domains;
using Senai.SPMedicalGroup.webApi.Interfaces;
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
    public class PacientesController : ControllerBase
    {
        private IPacienteRepository _pacienteRepository { get; set; }

        public PacientesController()
        {
            _pacienteRepository = new PacienteRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<Paciente> lista = _pacienteRepository.Listar();

                if (lista == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhum paciente cadastrado no sistema"
                    });
                }

                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Paciente novoPaciente)
        {
            try
            {
                if (novoPaciente.Cpf == null || novoPaciente.DataNasc > DateTime.Now || novoPaciente.Rg == null || novoPaciente.Telefone == null || novoPaciente.EnderecoPaciente == null || novoPaciente.IdUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados estão inválidos!"
                    });
                }

                _pacienteRepository.Cadastrar(novoPaciente);

                return Ok(new
                {
                    Mensagem = "Paciente cadastrado",
                    novoPaciente
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Paciente PacienteAtualizado)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Insira um ID válido!"
                    });
                }

                if (_pacienteRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não há nenhum paciente com o ID informado!"
                    });
                }

                if (PacienteAtualizado.Cpf == null || PacienteAtualizado.DataNasc > DateTime.Now || PacienteAtualizado.Rg == null || PacienteAtualizado.Telefone == null || PacienteAtualizado.EnderecoPaciente == null || PacienteAtualizado.IdUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados informados são inválidos!"
                    });
                }

                _pacienteRepository.Atualizar(id, PacienteAtualizado);
                return Ok(new
                {
                    Mensagem = "Paciente Alterado",
                    PacienteAtualizado
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "Insira um ID válido!"
                });
            }

            if (_pacienteRepository.BuscarPorId(id) == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não há nenhum paciente com o ID informado!"
                });
            }

            _pacienteRepository.Deletar(id);
            return Ok(new
            {
                Mensagem = "Paciente Excluido",

            });
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "Insira um ID válido!"
                });
            }

            if (_pacienteRepository.BuscarPorId(id) == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não há nenhum paciente com o ID informado!"
                });
            }

            Paciente pacienteEncontrado = _pacienteRepository.BuscarPorId(id);
            return Ok(new
            {
                Mensagem = "Paciente Encontrado",
                pacienteEncontrado
            });
        }
    }
}

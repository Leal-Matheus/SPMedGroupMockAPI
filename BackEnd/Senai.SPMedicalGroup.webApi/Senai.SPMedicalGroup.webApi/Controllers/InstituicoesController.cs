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
    public class InstituicoesController : ControllerBase
    {
        private IInstituicaoRepository _instituicaoRepository { get; set; }

        public InstituicoesController()
        {
            _instituicaoRepository = new InstituicaoRepository();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Instituicao novaClinica)
        {
            try
            {

                if (novaClinica == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os valores inseridos são inválidos!"
                    });
                }
                _instituicaoRepository.CadastrarClinica(novaClinica);

                return StatusCode(201, new
                {
                    Mensagem = "Clinica cadastrada",
                    novaClinica
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<Instituicao> lista = _instituicaoRepository.ListarTodas();

                if (lista == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma instituição cadastrada!"
                    });
                }

                return Ok(new
                {
                    Mensagem = $"Foram encontradas {lista.Count()} clínicas",
                    lista
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, Instituicao ClinicaAtualizada)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Insira um id válido"
                    });
                }

                if (_instituicaoRepository.BuscarClinica(id) == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma clínica com o id informado!"
                    });
                }
                if (ClinicaAtualizada.Cnpj == null || ClinicaAtualizada.Endereco == null || ClinicaAtualizada.NomeFantasia == null || ClinicaAtualizada.RazaoSocial == null || ClinicaAtualizada.Cnpj.Length != 18)
                {
                    return BadRequest(new
                    {
                        Mensagem = "As informações inseridas são inválidas!"
                    });
                }

                _instituicaoRepository.Atualizar(id, ClinicaAtualizada);
                return Ok(new
                {
                    Mensagem = "clinica atualizada",
                    ClinicaAtualizada
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id:int}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Insira um id válido"
                    });
                }

                if (_instituicaoRepository.BuscarClinica(id) == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma clínica com o id informado!"
                    });
                }

                _instituicaoRepository.Deletar(id);
                return Ok(new
                {
                    Mensagem = "Clinica deletada",
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }
    }
}
